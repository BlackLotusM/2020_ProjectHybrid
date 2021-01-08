using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Net;
using Newtonsoft.Json.Linq;

public class MakePlayerList : NetworkBehaviour
{
    [SerializeField]
    public GameObject Prefab;
    public GameObject parent;
    [SerializeField]
    private GameObject PlayerPrefab;
    [SerializeField]
    private int onlineCount;

    [SerializeField]
    private int listCount = 0;

    private void Start()
    {
        parent = this.gameObject;
        StartCoroutine(updateList());
    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length != listCount)
        {
            StartCoroutine(updateList());
            listCount = players.Length;
        }
    }

    IEnumerator updateList()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (Transform child in parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (GameObject p in players)
        {
            GameObject obj = Instantiate(Prefab);
            obj.GetComponent<PlayerData>().PM = p.GetComponent<PlayerManager>();
            obj.GetComponent<PlayerData>().player = PlayerPrefab;
            obj.transform.SetParent(parent.transform);
            obj.transform.localScale = new Vector3(1, 1, 1);
        }
        yield return new WaitForSeconds(1f);
    }

    private string getIsland = "http://86.91.184.42/GetIsland.php?naam=";

    public void GoHome(PlayerManager pm)
    {
        string final = getIsland + pm.playerName;
        StartCoroutine(getIslandDataBTN(final));
    }

    IEnumerator getIslandDataBTN(string download)
    {
        var json22 = new WebClient().DownloadString(download);
        yield return json22;
        dynamic data2 = JObject.Parse(json22);
        PlayerPrefab.transform.position = new Vector3((float)data2.Center_X, (float)data2.Center_Y, (float)data2.Center_Z);
    }
}
