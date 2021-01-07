using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using System.Net;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class Chat : NetworkBehaviour
{
    [Header("Chat Related")]
    [SerializeField] private TextMeshProUGUI chatText = null;
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private GameObject canvas = null;
    [SerializeField] private PlayerManager pm = null;

    private static event Action<string> OnMessage;
    private bool active = false;
    [SerializeField]private bool ismobile = false;
    [SerializeField] private GameObject canvasUI = null;
    private string highscoreURL = "http://86.91.184.42/updateLastPosition.php?naam='";
    private NetworkManager nm;


    private IEnumerator PostScores()
    {
        if (!ismobile)
        {
            GameObject hand = GameObject.Find(pm.name);
            float x = hand.GetComponent<Transform>().position.x;
            float y = hand.GetComponent<Transform>().position.y;
            float z = hand.GetComponent<Transform>().position.z;

            string final = highscoreURL + pm.name + "'&Pos_X=" + x + "&Pos_Y=" + y + "&Pos_Z=" + z;
            var hs_get = new UnityWebRequest(final);
            hs_get.downloadHandler = new DownloadHandlerBuffer();
            yield return hs_get.SendWebRequest();

            if (hs_get.error != null)
            {
                print("There was an error getting the high score: " + hs_get.error);
            }
            else
            {
                CmdSendMessage("Left the game");

                if (isServer)
                {
                    nm.StopHost();
                }
                else
                {
                    nm.StopClient();
                }
                SceneManager.LoadScene("Menu");
            }
        }
        else
        {
            if (isServer)
                {
                    nm.StopHost();
                }
                else
                {
                    nm.StopClient();
                }
                SceneManager.LoadScene("Menu");
        }
    }
    public void StopClientServer()
    {
        StartCoroutine(PostScores());
    }

    // Called when the a client is connected to the server
    public override void OnStartAuthority()
    {
        OnMessage += HandleNewMessage;
    }

    // Called when a client has exited the server
    [ClientCallback]
    private void OnDestroy()
    {
        if (!hasAuthority) { return; }
        OnMessage -= HandleNewMessage;
    }

    private void Start()
    {
        if (!isLocalPlayer) return;
        canvasUI.SetActive(true);
        nm = FindObjectOfType<NetworkManager>();
        if (isClient)
        {
            CmdSendMessage("Joined the game");
        }
        else
        {
            RpcHandleMessage("Joined the game");
        }
    }

    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isLocalPlayer)
            {
                active = !active;
                canvas.SetActive(active);
            }
        }
    }

    [Client]
    public void Send()
    {
        if (string.IsNullOrWhiteSpace(inputField.text)) { return; }
        CmdSendMessage(inputField.text);
        inputField.text = string.Empty;
    }

    [Command]
    private void CmdSendMessage(string message)
    {
        // Validate message
        RpcHandleMessage($"[{pm.playerName}]: {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }
}
