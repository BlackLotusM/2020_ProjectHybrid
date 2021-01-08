using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;
public class StartMiniGameSCRIPT : NetworkBehaviour
{
    
    public GameObject MiniGameObj;
    public Button button;
    [SyncVar]
    public bool stat = false;

    private void Start()
    {
        button.onClick.AddListener(CmdUpdateO);
    }

    [Command]
    public void CmdUpdateO()
    {
        stat = !stat;
        RpcSyncVarWithClients(stat);
    }

    [ClientRpc]
    void RpcSyncVarWithClients(bool varToSync)
    {
        stat = varToSync;
    }

}
