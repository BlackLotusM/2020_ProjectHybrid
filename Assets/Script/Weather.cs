using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Net;
using Newtonsoft.Json.Linq;

public class Weather : NetworkBehaviour
{
    public PlayerManager pm;
    public Button sadBtn;
    public Button happyBtn;
    public Button madBtn;

    public GameObject sad;
    public GameObject happy;
    public GameObject mad;

    private string getSpawnPos = "http://86.91.184.42/getWeatherNode.php?naam=";

    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        sadBtn.onClick.AddListener(setRain);
        happyBtn.onClick.AddListener(setClear);
        madBtn.onClick.AddListener(setMad);
    }

    IEnumerator getPartPosClear(string download)
    {
        var json22 = new WebClient().DownloadString(download);
        yield return json22;
        dynamic data2 = JObject.Parse(json22);
        Vector3 pos = new Vector3((float)data2.PosX, (float)data2.PosY, (float)data2.PosZ);
        CmdSetClear(pos);
    }

    IEnumerator getPartPosRain(string download)
    {
        var json22 = new WebClient().DownloadString(download);
        yield return json22;
        dynamic data2 = JObject.Parse(json22);
        Vector3 pos = new Vector3((float)data2.PosX, (float)data2.PosY, (float)data2.PosZ);
        CmdSetRain(pos);
    }

    IEnumerator getPartPosMad(string download)
    {
        var json22 = new WebClient().DownloadString(download);
        yield return json22;
        dynamic data2 = JObject.Parse(json22);
        Vector3 pos = new Vector3((float)data2.PosX, (float)data2.PosY, (float)data2.PosZ);
        CmdSetMad(pos);
    }


    [Command]
    void CmdSetRain(Vector3 pos)
    {
        var GO = Instantiate(sad, pos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(GO);
    }

    [Command]
    void CmdSetClear(Vector3 pos)
    {
        var GO = Instantiate(happy, pos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(GO);
    }

    [Command]
    void CmdSetMad(Vector3 pos)
    {
        var GO = Instantiate(mad, pos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(GO);
    }

    void setMad()
    {
        string final = getSpawnPos + pm.playerName;
        StartCoroutine(getPartPosMad(final));
    }
    void setClear()
    {
        string final = getSpawnPos + pm.playerName;
        StartCoroutine(getPartPosClear(final));
    }

    void setRain()
    {
        string final = getSpawnPos + pm.playerName;
        StartCoroutine(getPartPosRain(final));
    }
}
