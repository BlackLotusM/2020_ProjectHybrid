using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Net;
using UnityEngine.UI;
using TMPro;

public class ConnectScript : MonoBehaviour
{
    public InputField ip;
    [SerializeField]
    private GameObject managerObj;
    private NetworkManager manager;
    [SerializeField]
    private TextMeshProUGUI PlayerName;
    [SerializeField]
    private bool isHost = false;
    private void Awake()
    {
        manager = FindObjectOfType<NetworkManager>();
    }
    public void connect()
    {
        manager.networkAddress = ip.text;
        manager.DisplayName = PlayerName.text;
        Debug.Log(manager.networkAddress);
        
        if (isHost)
        {
            StartServer();
        }
        else
        {
            StartButtons();
        }
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
 
