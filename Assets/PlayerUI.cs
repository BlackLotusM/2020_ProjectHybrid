using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerUI : NetworkBehaviour
{
    public bool active = false;
    [SerializeField]
    private GameObject[] OtherPopUps;
   
    public void ToggleCanvas(GameObject Canvas)
    {
        foreach (GameObject t in OtherPopUps)
        {
            t.SetActive(false);
        }

        if(Canvas.activeSelf == true)
        {
            active = true;
            active = !active;
            Canvas.SetActive(active);
        }
        else
        {
            active = false;
            active = !active;
            Canvas.SetActive(active);
        }
    }

    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }
}
