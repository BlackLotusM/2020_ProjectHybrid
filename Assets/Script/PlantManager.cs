using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : NetworkBehaviour
{
    public int PlantsFixed;
    public void bishwerk(GameObject pr)
    {
        SetStates(pr);
    }

    [Command]
    public void SetStates(GameObject pr)
    {
        bool BloemState = !pr.GetComponent<PlantRedo>().BloemState;
        if(BloemState != false)
        {
            RpcSetState(BloemState, pr);
        }
    }

    [ClientRpc]
    private void RpcSetState(bool state, GameObject pr)
    {
        pr.GetComponent<PlantRedo>().BloemState = state;
    }

    private void Awake()
    {
        if (isServer)
        {
            Debug.Log("ree");
        }
    }
}
