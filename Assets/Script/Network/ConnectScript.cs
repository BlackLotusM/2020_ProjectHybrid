using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Net;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class ConnectScript : MonoBehaviour
{
    public InputField ip;
    [SerializeField]
    public NetworkManager manager;
    [SerializeField]
    private TMP_InputField PlayerName = null;
    [SerializeField]
    private bool isHost = false;

    private string highscoreURL = "http://86.91.184.42/?naam=";

    private IEnumerator PostScores()
    {
        manager = null;
        manager = FindObjectOfType<NetworkManager>();
        manager.networkAddress = ip.text;
        
        manager.DisplayName = PlayerName.text;

        string final = highscoreURL+ PlayerName.text;
        var hs_get = new UnityWebRequest(final);
        hs_get.downloadHandler = new DownloadHandlerBuffer();

        
        yield return hs_get.SendWebRequest();

        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            if (isHost)
            {
                manager.isHost = true;
                StartServer();
            }
            else
            {
                manager.isHost = false;
                StartButtons();
            }
        }
    }

    public void connect()
    {
        StartCoroutine(PostScores());
    }

    public void StopButtons()
    {
        manager.StopClient();
    }

    public void StartButtons()
    {
        if (!NetworkClient.active)
        {
            manager.StartClient();
        }
        else { manager.StopClient(); }
    }

    public void StartServer()
    {
        if (!NetworkClient.active)
        {
            manager.StartHost();
        }
        else { manager.StopHost(); }
    }
}
 
