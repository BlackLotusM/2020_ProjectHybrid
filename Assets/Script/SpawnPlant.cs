using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlant : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject bloem;
    void Start()
    {
        if (isServer)
        {
                var GO = Instantiate(bloem, this.gameObject.transform.position, Quaternion.identity) as GameObject;
                NetworkServer.Spawn(GO);
                Destroy(this.gameObject);
        }
    }
}
