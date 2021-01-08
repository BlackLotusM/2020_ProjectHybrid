using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : NetworkBehaviour
{    
    public void bishwerk(GameObject pr)
    {

        SetStates(pr);
    }

    [Command]
    public void SetStates(GameObject pr)
    {
        bool BloemState = !pr.GetComponent<PlantRedo>().BloemState;
        RpcSetState(BloemState, pr);
    }

    [ClientRpc]
    private void RpcSetState(bool state, GameObject pr)
    {
        pr.GetComponent<PlantRedo>().BloemState = state;
    }

}
