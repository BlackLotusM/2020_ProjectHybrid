using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationHandler : MonoBehaviour
{
    public ShowRemove sr;
    void Start()
    {
        sr.startNotification("tetttt", "mieeep");
    }
}
