using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FootSteps : MonoBehaviour
{
    public AudioClip[] footsteps;
    public GameObject audioSouce;
    
    public NetworkAnimator anim;
    public animatorHandler ah;

    private void Step()
    {
        int clip = GetRandomClip();
        audioSouce.GetComponent<AudioSource>().PlayOneShot(footsteps[clip]);
    }


    private int GetRandomClip()
    {
        return UnityEngine.Random.Range(0, footsteps.Length);
    }
}
