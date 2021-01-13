using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class animatorHandler : NetworkBehaviour
{
    public FootSteps foot;
    internal void setSound(int clip, GameObject audiosource)
    {
        CmdSetSound(clip, audiosource);
    }

    [Command]
    public void CmdSetSound(int ac, GameObject audioObject)
    {
        audioObject.GetComponent<AudioSource>().PlayOneShot(foot.footsteps[ac]);
        RpcUpdate(ac, audioObject);
    }

    [ClientRpc]
    public void RpcUpdate(int ac, GameObject audioObject)
    {
        audioObject.GetComponent<AudioSource>().PlayOneShot(foot.footsteps[ac]);
    }
}
