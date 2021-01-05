using FirstGearGames.Mirrors.SynchronizingBulkSceneObjects;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlanter : MonoBehaviour
{
        /// <summary>
        /// Prefab to spawn for new WorldObjects.
        /// </summary>
        [Tooltip("Prefab to spawn for new WorldObjects.")]
        [SerializeField]
        private WorldObject _treePrefab;

        private void Update()
        {
            if (NetworkServer.active)
            {
                CheckPlantTreeObject();
            }
        }

    /// <summary>
    /// Plant all the WorldObjects.
    /// </summary>
    private void CheckPlantTreeObject()
    {
        //Right click plants
        if (!Input.GetKeyDown(KeyCode.Mouse1))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            WorldObject wo = WorldObjectManager.Instance.InstantiateWorldObject(WorldObjectTypes.ZonneBloem, hit.point, Quaternion.identity);
            TreeObjectData data = (TreeObjectData)wo.ReturnData();
            data.SetTreeState(TreeObjectData.TreeStates.Default);
            wo.UpdateData(data);
            WorldObjectManager.Instance.InitializeWorldObject(wo, WorldObjectTypes.ZonneBloem);
        }
    }
}
