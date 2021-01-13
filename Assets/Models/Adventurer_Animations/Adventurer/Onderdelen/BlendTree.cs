using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BlendTree : MonoBehaviour
{
    public float smoothBlend = 0.1f;
    public Animator anim;
    public NetworkAnimator an;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void Move(float x, float y)
    {
        if (y == 0)
        {
            an.SetTrigger("Idle");
            an.ResetTrigger("Back");
            an.ResetTrigger("Walk");
        }
        else if(y > 0.2)
        {
            an.SetTrigger("Walk");
            an.ResetTrigger("Back");
            an.ResetTrigger("Idle");
        }
        else if (y < -0.2)
        {
            an.SetTrigger("Back");
            an.ResetTrigger("Walk");
            an.ResetTrigger("Idle");
        }
    }
}