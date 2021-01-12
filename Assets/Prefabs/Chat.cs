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
using FirstGearGames.Mirrors.SynchronizingBulkSceneObjects;

public class Chat : NetworkBehaviour
{
    [Header("Chat Related")]
    [SerializeField] private TextMeshProUGUI chatText = null;
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private TextMeshProUGUI MobilechatText = null;
    [SerializeField] private TMP_InputField MobileinputField = null;
    [SerializeField] private PlayerManager pm = null;
    private static event Action<string> OnMessage;

    [SerializeField]private bool ismobile = false;
    [SerializeField] private GameObject canvasUI = null;
    [SerializeField] private GameObject noti = null;
    private string highscoreURL = "http://86.91.184.42/updateLastPosition.php?naam='";
    private NetworkManager nm;
    public GameObject MobileUI;


    public void StopClientServer()
    {
        StartCoroutine(PostScores());
    }

    public override void OnStartAuthority()
    {
        OnMessage += HandleNewMessage;
    }

    private void Start()
    {
        if (!isLocalPlayer) return;
        if (!ismobile)
        {
            MobileUI.SetActive(false);
            canvasUI.SetActive(true);
            noti.SetActive(true);
        }
        else
        {
            chatText = MobilechatText;
            inputField = MobileinputField;

            MobileUI.SetActive(true);
            canvasUI.SetActive(false);
            noti.SetActive(false);
        }
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

    private IEnumerator PostScores()
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
                WorldObjectManager nm2 = FindObjectOfType<WorldObjectManager>();
                nm2._dirty.Clear();
                nm.StopHost();
                nm.StopServer();
            }
            else
            {
                nm.StopClient();
            }
            SceneManager.LoadScene("Menu");
        }
    }

    [ClientCallback]
    private void OnDestroy()
    {
        if (!hasAuthority) { return; }
        OnMessage -= HandleNewMessage;
    }

    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    [Client]
    public void Send()
    {
        if (string.IsNullOrWhiteSpace(inputField.text)) { return; }
        CmdSendMessage(inputField.text);
        inputField.text = string.Empty;
    }

    [Client]
    public void SendCustom(string message)
    {
        CmdServerMessage(message);
    }

    [Command]
    private void CmdSendMessage(string message)
    {
        RpcHandleMessage($"[{pm.playerName}]: {message}");
    }

    [Command]
    private void CmdServerMessage(string message)
    {
        RpcHandleMessage($"Weather Station: {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }
}
