using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(4 * Time.deltaTime, 22 * Time.deltaTime, 10 * Time.deltaTime); //rotates 50 degrees per second around z axis
    }
}
