using System.Collections;
using UnityEditor;
using UnityEngine;
using TMPro;

public class ShowRemove : MonoBehaviour
{
    //public GameObject Parent;
    public GameObject Prefab;
    public GameObject Parent;

    public void startNotification(string title, string content)
    {
        StartCoroutine(Show(title, content));
    }

    IEnumerator Show(string title, string content)
    {
        GameObject t = Instantiate(Prefab);
        t.transform.SetParent(Parent.transform);
        t.transform.localScale = new Vector3(1,1,1);
        Transform tt = t.transform.Find("BackGround");
        Transform t2 = tt.transform.Find("Title");
        Transform t3 = tt.transform.Find("Content");
        t2.gameObject.GetComponent<TextMeshProUGUI>().text = title;
        t3.gameObject.GetComponent<TextMeshProUGUI>().text = content;

        yield return new WaitForSeconds(0.1f);
    }
}