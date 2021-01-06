﻿using UnityEngine;
using TMPro;
using Mirror;
using System.Net;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using FirstGearGames.Mirrors.SynchronizingBulkSceneObjects;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerObj
{
    public int ID = 0;
    public string Naam = "";
}

public class PlayerManager : NetworkBehaviour
{
    private NetworkManager NetManager;
    public TextMeshProUGUI playerNameText;
    private Material playerMaterialClone;
    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;
    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;
    public GameObject bloem;
   
    [SerializeField]
    private GameObject CameraMountPoint = null;
    public bool isMobile;

    private string highscoreURL = "http://86.91.184.42/getLastPosition.php?naam=";
    private string checkIsland = "http://86.91.184.42/CheckForIsland.php?naam=";
    private string addIsland = "http://86.91.184.42/addIsland.php?naam=";
    private string getIsland = "http://86.91.184.42/selectIslands.php";

    [Header("Island")]
    [SerializeField]private GameObject[] Obstacles = new GameObject[0];
    public int islandTypeInt = 0;

    public float obstacleCheckRadius = 3f;
    public int maxSpawnAttemptsPerObstacle = 10;
    public float maxX1, maxY1 = 0;
    public float maxX2, maxY2 = 0;
    [SerializeField]
    private Button SelectIsland;
    [SerializeField]
    private GameObject selectIslandGroup;
    GameObject Obstacle;
    public GameObject art;
    public List<string> targetVelocity;
    public List<string> setList;

    private void Awake()
    {
        NetManager = FindObjectOfType<NetworkManager>();
        playerName = NetManager.DisplayName;
    }

    void OnNameChanged(string _Old, string _New)
    {
            playerNameText.text = playerName;

    }

    void OnColorChanged(Color _Old, Color _New)
    {
        if (!isMobile)
        {
            playerNameText.color = _New;
            playerMaterialClone = new Material(GetComponent<Renderer>().material);
            playerMaterialClone.color = _New;
            GetComponent<Renderer>().material = playerMaterialClone;
        }
    }

    
    public override void OnStartLocalPlayer()
    {
        playerName = NetManager.DisplayName;
        gameObject.name = playerName;
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        CmdSetupPlayer(playerName, color);
        if (isServer){InitIslands();}
        StartCoroutine(InitializePlayer(gameObject));
        StartCoroutine(UpdateList());
    }


    private void InitIslands()
    {
        var json22 = new WebClient().DownloadString(getIsland);
        if (json22 != "")
        {
            dynamic jsonObj = JsonConvert.DeserializeObject<JArray>(json22);
            foreach (var obj in jsonObj)
            {
                Vector3 pos = new Vector3((float)obj.x, (float)obj.y, (float)obj.z);
                WorldObjectTypes value = (WorldObjectTypes)obj.IslandType;
                WorldObject wo = WorldObjectManager.Instance.InstantiateWorldObject(value, pos, Quaternion.identity);
                IslandObjectData data = (IslandObjectData)wo.ReturnData();
                data.SetTreeState(IslandObjectData.TreeStates.Default);
                wo.UpdateData(data);
                WorldObjectManager.Instance.InitializeWorldObject(wo, value);
            }
        };
    }
    [Command]
    private void SpawnIsland(Vector3 position2, int island)
    {
        WorldObjectTypes value = (WorldObjectTypes)island;
        WorldObject wo = WorldObjectManager.Instance.InstantiateWorldObject(value, position2, Quaternion.identity);
        IslandObjectData data = (IslandObjectData)wo.ReturnData();
        data.SetTreeState(IslandObjectData.TreeStates.Default);
        wo.UpdateData(data);
        wo.name = name + " Island";
        WorldObjectManager.Instance.InitializeWorldObject(wo, value);
    }

    public void setIslandType(int type)
    {
        islandTypeInt = type;
    }
    private IEnumerator InitializePlayer(GameObject Target)
    {
        string final2 = checkIsland + playerName;
        var json2 = new WebClient().DownloadString(final2);
        yield return json2;
        dynamic data2 = JObject.Parse(json2);

        if (data2.HasIsland == 0)
        {
            selectIslandGroup.SetActive(true);
            var waitForButton = new WaitForUIButtons(SelectIsland);
            yield return waitForButton.Reset();
            if (waitForButton.PressedButton)
            {
                selectIslandGroup.SetActive(false);
            }

            Obstacle = Obstacles[Random.Range(0, Obstacles.Length)];
            Vector3 position2 = Vector3.zero;
            bool validPosition = false;
            int spawnAttempts = 0;

            while (!validPosition)
            {
                spawnAttempts++;
                position2 = new Vector3(Random.Range(maxX1, maxX2), 0, Random.Range(maxY1, maxY2));
                validPosition = true;
                Collider[] colliders = Physics.OverlapSphere(position2, obstacleCheckRadius);
                foreach (Collider col in colliders)
                {
                    if (col.tag == "Obstacle")
                    {
                        validPosition = false;
                    }
                }
                if (spawnAttempts > 7)
                {
                    maxX1 -= 200;
                    maxX2 += 200;
                    maxY1 += 200;
                    maxY2 -= 200;
                }
            }

            if (validPosition)
            {
                string final = addIsland + name + "&Center_X=0" + "&Center_Y=0" + "&Center_Z=0" + "&IslandPosX=" + position2.x + "&IslandPosY=" + position2.y + "&IslandPosZ=" + position2.z + "&IslandType=" + islandTypeInt;
                StartCoroutine(SetIsland(final));

                SpawnIsland(position2, islandTypeInt);
                var x = position2.x;
                var y = position2.y;
                var z = position2.z;

                Target.transform.position = new Vector3(x, y, z);
                setCamPos();
            }
        }
        else
        {
            selectIslandGroup.SetActive(false);
            string posString = highscoreURL + name;
            var json = new WebClient().DownloadString(posString);
            dynamic data = JObject.Parse(json);
            float x = data.Pos_X;
            float y = data.Pos_Y;
            float z = data.Pos_Z;
            Target.transform.position = new Vector3(x, y, z);
            setCamPos();
        }
    }

    private IEnumerator SetIsland(string final)
    {
        var hs_get = new UnityWebRequest(final);
        hs_get.downloadHandler = new DownloadHandlerBuffer();
        yield return hs_get.SendWebRequest();
    }

    void setCamPos()
    {
        Transform cameraTransform = Camera.main.gameObject.transform;  //Find main camera which is part of the scene instead of the prefab
        cameraTransform.parent = CameraMountPoint.transform;  //Make the camera a child of the mount point
        cameraTransform.position = CameraMountPoint.transform.position;  //Set position/rotation same as the mount point
        cameraTransform.rotation = CameraMountPoint.transform.rotation;
        if (isMobile)
        {
            cameraTransform.gameObject.SetActive(false);
        }
    }

    [Command]
    public void CmdSetupPlayer(string _name, Color _col)
    {
        playerName = _name;
        playerColor = _col;
    }

    [Command]
    public void CmdSpawn()
    {
        var GO = Instantiate(bloem, this.gameObject.transform.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(GO);
    }

    IEnumerator UpdateList()
    {
        targetVelocity = new List<string>();
        setList = new List<string>();
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            targetVelocity.Add(p.GetComponent<PlayerManager>().playerName);
        }

        setList = targetVelocity;
        
        Debug.Log(setList.Count);
        yield return new WaitForSeconds(2f);
        targetVelocity.Clear();

        StartCoroutine(UpdateList());
    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdSpawn();
        }

        if (isMobile)
        {
            gameObject.transform.position = new Vector3(0, -20, 0);
        }
        else
        {
            float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
            float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

            transform.Rotate(0, moveX, 0);
            transform.Translate(0, 0, moveZ);
        }
    }
}
