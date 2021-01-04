using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using System.Net;
using Newtonsoft.Json.Linq;

public class Script_UI : MonoBehaviour
{
    private NetworkManager nm;
    private string highscoreURL = "http://86.91.184.42/getCoin.php?naam=";
    private string final;
    private void Start()
    {
        nm = FindObjectOfType<NetworkManager>();
        final = highscoreURL + nm.DisplayName;
        StartCoroutine(WaitAndPrint());
    }
    IEnumerator WaitAndPrint()
    {
        var json = new WebClient().DownloadString(final);
        dynamic data = JObject.Parse(json);
        this.gameObject.GetComponent<TextMeshProUGUI>().text = data.Amount;
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(WaitAndPrint());
    }
}
