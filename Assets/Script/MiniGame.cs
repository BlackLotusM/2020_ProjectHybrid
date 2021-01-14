using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;

public class MiniGame : NetworkBehaviour
{
    public GameObject ResetPoint;
    public GameObject PlayerFinal;
    public float radius;
    public GameObject wayPoint1;
    public TextMeshProUGUI Sum;
    public float sumAround;
    public List<GameObject> players = new List<GameObject>();
    public float sum;
    public float sumUp;
    public float iS;
    private float newy;

    [SerializeField]
    public bool MiniGameActive = false;
    [SyncVar]
    public bool MiniGameDone = false;
    public int coins = 100;

    public float SumArray(List<GameObject> toBeSummed)
    {
        float sum = 0;
        foreach (GameObject item in toBeSummed)
        {
            sum += item.GetComponent<micPickup>().dbVal;
        }
        return sum;
    }

    private void Start()
    {
        StartCoroutine(update());
        StartCoroutine(moveUp(MiniGameActive));
    }

    IEnumerator sumLoop()
    {
        if (!MiniGameDone)
        {
            foreach (GameObject p in players)
            {
                if (iS >= players.Count)
                {
                    iS = 0;
                    sum = 0;
                }
                else
                {
                    sum = sum + p.GetComponent<micPickup>().dbVal;
                    sumUp = sum / players.Count;
                    iS++;
                }
            }

            if (players.Count == 0)
            {
                yield return new WaitForSeconds(2);
                StartCoroutine(sumLoop());
            }
            else
            {
                for (int i = 0; i < players.Count;)
                {
                    i++;

                    sum = sum + players[i].GetComponent<micPickup>().dbVal;
                    iS = i;
                    if (i >= players.Count)
                    {
                        sum = sum / players.Count;
                        Sum.text = Convert.ToString(sum);
                        sum = 0;
                        i = 0;
                    }
                }
            }
        }
    }

    IEnumerator moveUp(bool ac)
    {
        if (!MiniGameDone)
        {
            if (ac)
            {
                sumAround = SumArray(players) / players.Count - 2;
                Sum.text = Convert.ToString(sumAround);
                Vector3 target = wayPoint1.transform.position;

                if (float.IsNaN(sumAround))
                {
                    newy = 0;
                }
                else
                {
                    if (sumAround < 0)
                    {
                        newy = -0.3f;
                    }
                    else if (sumAround > 4)
                    {
                        newy = 4;
                    }
                    else
                    {
                        newy = sumAround;
                    }
                }

                this.gameObject.transform.position = Vector3.MoveTowards(transform.position, target, 0.06f);
                this.gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(this.transform.position.x, newy, this.gameObject.transform.position.z), 0.1f);
            }
            else
            {
                this.gameObject.transform.position = ResetPoint.transform.position;
            }

            yield return new WaitForSeconds(0.01f);
            if(this.gameObject.transform.position.y <= 0)
            {
                MiniGameActive = false;
            }
            StartCoroutine(moveUp(MiniGameActive));
            if (Vector3.Distance(new Vector3(this.gameObject.transform.position.x, 0 , this.gameObject.transform.position.z), new Vector3(wayPoint1.transform.position.x, 0, wayPoint1.transform.position.z)) < 1)
            {
                foreach(GameObject p in players)
                {
                    p.GetComponent<StartMiniGameSCRIPT>().finished = true;
                }
                MiniGameDone = true;
            }
            else
            {
                
            }
        }
    }

    IEnumerator update()
    {
        if (!MiniGameDone)
        {
            GameObject[] list = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject r in list)
            {
                if (Vector3.Distance(this.gameObject.transform.position, r.transform.position) < radius)
                {
                    if (!players.Contains(r))
                    {
                        players.Add(r);
                    }
                    if (r == players.First())
                    {
                        if (!MiniGameActive)
                        {
                            r.GetComponent<micPickup>().t = true;
                            r.GetComponent<micPickup>().t2 = false;
                        }
                        else
                        {
                            r.GetComponent<micPickup>().t = false;
                            r.GetComponent<micPickup>().t2 = false;
                        }
                    }
                    else
                    {
                        if (!MiniGameActive)
                        {
                            r.GetComponent<micPickup>().t = false;
                            r.GetComponent<micPickup>().t2 = true;
                        }
                        else
                        {
                            r.GetComponent<micPickup>().t = false;
                            r.GetComponent<micPickup>().t2 = false;
                        }
                    }
                }
                else
                {
                    if (players.Count > 0)
                    {
                        r.GetComponent<micPickup>().t = false;
                        r.GetComponent<micPickup>().t2 = false;
                        players.Remove(r);
                    }
                }
            }
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(update());
        }
    }
}
