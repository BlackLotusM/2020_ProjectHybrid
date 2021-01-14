using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class animatorHandler : NetworkBehaviour
{
    public FootSteps foot;
    public GameObject Sound;
    public GameObject prefab;
    public GameObject point;

    internal void setSound(int clip)
    {
        CmdSpawnSound(clip);
        
    }

    [Command]
    void CmdSpawnSound(int i)
    {
        var GO = Instantiate(prefab, point.transform.position, Quaternion.identity) as GameObject;
        GO.gameObject.GetComponent<AudioSource>().clip = foot.footsteps[i];
        NetworkServer.Spawn(GO);
    }
}
