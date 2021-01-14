using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Net;
using Newtonsoft.Json.Linq;

public class SpawnBlob : NetworkBehaviour
{
    private string getSpawnPos = "http://86.91.184.42/getWeatherNode.php?naam=";
    public PlayerManager pm;
    public GameObject prefab;

    public void SpawnBlobje()
    {
        string final = getSpawnPos + pm.playerName;
        StartCoroutine(getPartPosClear(final));
    }

    IEnumerator getPartPosClear(string download)
    {
        var json22 = new WebClient().DownloadString(download);
        yield return json22;
        dynamic data2 = JObject.Parse(json22);
        Vector3 pos = new Vector3((float)data2.PosX, (float)data2.PosY, (float)data2.PosZ);
        CmdSpawn(pos);
    }

    [Command]
    public void CmdSpawn(Vector3 pos)
    {
        var GO2 = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(GO2);
    }
}
