using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class LocalPlayerHide : NetworkBehaviour
{
    public GameObject[] list;

    private void Update()
    {
        if (!isLocalPlayer) return;
            foreach (GameObject o in list)
            {
                o.SetActive(true);
            }
        }
    }
