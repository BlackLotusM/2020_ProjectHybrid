using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    public GameObject Options;
    public GameObject Friends;

    public bool OptionsBool;
    public bool FriendsBool;

    public void doIt()
    {
        Options.SetActive(OptionsBool);
        Friends.SetActive(FriendsBool);
    }
}
