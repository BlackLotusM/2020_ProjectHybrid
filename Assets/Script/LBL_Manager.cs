using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LBL_Manager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager pm;
    [SerializeField]
    private TextMeshProUGUI playerName;
    [SerializeField]
    private TextMeshProUGUI date;

    private void Update()
    {
        date.text = System.DateTime.Today.ToString("dd/MM/yyyy");
        playerName.text = pm.playerName;
    }
}
