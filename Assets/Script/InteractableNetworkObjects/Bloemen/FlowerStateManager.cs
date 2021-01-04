using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class FlowerStateManager : NetworkBehaviour
{
    [SyncVar]
    public bool state;

    public NetworkManager nm;
   
    void Awake()
    {
         nm = FindObjectOfType<NetworkManager>();
    }
    void Start()
    {
        if (isServer) {
            int s = Random.Range(0, 2);
            if (s == 1)
            {
                state = true;
            }
            else
            {
                state = false;
            }
        } 
    }

    void Update()
    {
        if (!isServer)
        {
            nm.CmdSendName(this.gameObject, state);
        }
        else
        {
            nm.RpcSendName(this.gameObject, state);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int s = Random.Range(0, 2);
            if (s == 1)
            {
                state = true;
            }
            else
            {
                state = false;
            }
        }
    }
}
