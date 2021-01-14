using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlob : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            transform.Translate(-0.01f, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "north")
        {
            this.gameObject.transform.eulerAngles = new Vector3(
                this.gameObject.transform.eulerAngles.x,
                this.gameObject.transform.eulerAngles.y + Random.Range(80,220),
                this.gameObject.transform.eulerAngles.z
            );
        }
        else if (collision.gameObject.tag == "south")
        {
            this.gameObject.transform.eulerAngles = new Vector3(
                this.gameObject.transform.eulerAngles.x,
                this.gameObject.transform.eulerAngles.y + Random.Range(80, 220),
                this.gameObject.transform.eulerAngles.z
            );
        }
        else if (collision.gameObject.tag == "west")
        {
            this.gameObject.transform.eulerAngles = new Vector3(
                this.gameObject.transform.eulerAngles.x,
                this.gameObject.transform.eulerAngles.y + Random.Range(80, 220),
                this.gameObject.transform.eulerAngles.z
            );
        }
        else if (collision.gameObject.tag == "east")
        {
            this.gameObject.transform.eulerAngles = new Vector3(
                this.gameObject.transform.eulerAngles.x,
                this.gameObject.transform.eulerAngles.y + Random.Range(80, 220),
                this.gameObject.transform.eulerAngles.z
            );
        }
    }
}
