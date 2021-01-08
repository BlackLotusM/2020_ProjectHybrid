using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra : MonoBehaviour
{
    public GameObject ph;
    public GameObject ps;
    void Update()
    {
        int r = Random.Range(0, 1000);
        if(r > 993)
        {
            ph.GetComponent<MeshRenderer>().enabled = false;
            ps.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            ph.GetComponent<MeshRenderer>().enabled = true;
            ps.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
