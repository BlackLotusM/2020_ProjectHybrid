using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class PlayerData : NetworkBehaviour
{
    public GameObject player;
    [SerializeField]
    private Image islandImage;
    [SerializeField]
    public string stringPlayer;
    [SerializeField]
    private TextMeshProUGUI playerName;
    [SerializeField]
    private Button visitBtn;
    [SerializeField]
    public PlayerManager PM;
    private string getIsland = "http://86.91.184.42/GetIsland.php?naam=";

    private void Update()
    {
        this.gameObject.name = PM.playerName;
        playerName.text = PM.playerName;
        stringPlayer = PM.playerName;
    }

    void Start()
    {
        Button press = visitBtn.GetComponent<Button>();
        press.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        string final = getIsland + stringPlayer;
        StartCoroutine(getIslandData(final));
    }

    IEnumerator getIslandData(string download)
    {
        var json22 = new WebClient().DownloadString(download);
        yield return json22;
        dynamic data2 = JObject.Parse(json22);
        player.transform.position = new Vector3((float)data2.Center_X, (float)data2.Center_Y, (float)data2.Center_Z);
    }
}
