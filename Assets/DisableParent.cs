using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableParent : MonoBehaviour
{
    public GameObject part;

    public void detroyBoi()
    {
        Destroy(part);
    }
}
