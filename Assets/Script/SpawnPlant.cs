using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlant : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject bloem;
    public bool alreadyTake = false;
    public bool mustBeDead = false;
    public GameObject Partent;
    void Awake()
    {
        NetworkManager nm = FindObjectOfType<NetworkManager>();

        if (nm.isHost == true)
        {
            if (mustBeDead)
            {
                var GO = Instantiate(bloem, this.gameObject.transform.position, Quaternion.identity) as GameObject;
                GO.GetComponent<PlantRedo>().BloemState = !mustBeDead;
                NetworkServer.Spawn(GO);
                Destroy(this.gameObject);
            }
            else
            {
                var GO = Instantiate(bloem, this.gameObject.transform.position, Quaternion.identity) as GameObject;
                NetworkServer.Spawn(GO);
                Destroy(this.gameObject);
            }
        }
    }
}
