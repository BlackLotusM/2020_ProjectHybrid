using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class resetManager : NetworkBehaviour
{
    public GameObject manager;
    public GameObject connect1;
    public GameObject connect2;
    private void Start()
    {

        NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();
        
        
        if (nm != null)
        {
            nm.StopAllCoroutines();
            nm.StopClient();
            nm.StopServer();
            nm.StopHost();
            Destroy(nm.gameObject);
        }
        GameObject SetManager =  Instantiate(manager);
        connect1.GetComponent<ConnectScript>().manager = SetManager.GetComponent<NetworkManager>();
        connect2.GetComponent<ConnectScript>().manager = SetManager.GetComponent<NetworkManager>();
    }

    private void Update()
    {
        NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();
        if (nm == null)
        {
            GameObject SetManager = Instantiate(manager);
            connect1.GetComponent<ConnectScript>().manager = SetManager.GetComponent<NetworkManager>();
            connect2.GetComponent<ConnectScript>().manager = SetManager.GetComponent<NetworkManager>();
        }
    }
}
