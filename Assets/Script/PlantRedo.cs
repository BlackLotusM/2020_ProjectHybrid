using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PlantRedo : NetworkBehaviour
{
    [SyncVar(hook = nameof(Bish))]
    public bool BloemState;

    [SerializeField]
    private GameObject sadBloem;
    [SerializeField]
    private GameObject happyBloem;


    void Bish(bool _old, bool newV)
    {
        BloemState = newV;
    }
    private void Start()
    {
        
    }
    private void Awake()
    {
            int i = Random.Range(0, 22);
            if (i > 7)
            {
            BloemState = true;
                
            }
            else
            {
                BloemState = false;
            }
    }

    public void SetState(PlantManager pm)
    {
        pm.bishwerk(this.gameObject);
    }

    void Update()
    {
        if (BloemState)
        {
            sadBloem.SetActive(false);
            happyBloem.SetActive(true);
            

        }
        else
        {
            sadBloem.SetActive(true);
            happyBloem.SetActive(false);
        }
    }
}
