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
    [SyncVar]
    public bool finished = false;
    public GameObject IsDone;
    public GameObject IsCheck;

    private void Start()
    {
        button.onClick.AddListener(CmdUpdateO);
    }

    private void Update()
    {
        if (finished)
        {
            IsDone.SetActive(true);
            IsCheck.SetActive(false);
        }
        GameObject[] list = GameObject.FindGameObjectsWithTag("MiniGame");
        foreach (GameObject r in list)
        {
            if (Vector3.Distance(this.gameObject.transform.position, r.transform.position) < 10)
            {
                MiniGameObj = r.gameObject;
            }else if (r == MiniGameObj)
            {
                MiniGameObj = null;
            }
        }
    }

    [Command]
    public void CmdUpdateO()
    {
        MiniGameObj.GetComponent<MiniGame>().MiniGameActive = true;
        RpcSyncVarWithClients(stat);
    }

    [ClientRpc]
    void RpcSyncVarWithClients(bool varToSync)
    {
        MiniGameObj.GetComponent<MiniGame>().MiniGameActive = true;
    }

}
