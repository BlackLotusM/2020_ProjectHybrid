using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using FirstGearGames.Mirrors.SynchronizingBulkSceneObjects;

public class TreeSpawner : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject TreePrefab;
    [SerializeField]
    private WorldObjectTypes _objectType;
    private void Awake()
    {
        Vector3 pos = this.gameObject.transform.position;
        Quaternion rot = this.gameObject.transform.rotation;
        WorldObject wo = WorldObjectManager.Instance.InstantiateWorldObject(_objectType, pos, rot);
        TreeObjectData data = (TreeObjectData)wo.ReturnData();
        data.SetTreeState(TreeObjectData.TreeStates.Default);
        wo.UpdateData(data);
        WorldObjectManager.Instance.InitializeWorldObject(wo, _objectType);
        Destroy(this.gameObject);
    }
}
