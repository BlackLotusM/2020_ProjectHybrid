using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaterPlantQuest : MonoBehaviour
{
    public PlantManager pm;
    public int Needed;
    public TextMeshProUGUI Collected;
    public GameObject EnablePanel;
    public GameObject DisablePanel;

    // Update is called once per frame
    void Update()
    {
        Collected.text = pm.PlantsFixed.ToString();
        if(pm.PlantsFixed >= Needed)
        {
            DisablePanel.SetActive(false);
            EnablePanel.SetActive(true);
        }
    }
}
