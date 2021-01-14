using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startSound : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
        StartCoroutine(destroyY());
    }

    IEnumerator destroyY()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
