using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class enableSound : NetworkBehaviour
{
    public GameObject sound;
    private void Update()
    {
        if (!isLocalPlayer) return;
        sound.SetActive(true);
    }
}
