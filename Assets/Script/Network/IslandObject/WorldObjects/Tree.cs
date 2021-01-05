using Mirror;
using UnityEngine;

namespace FirstGearGames.Mirrors.SynchronizingBulkSceneObjects
{

    public class Tree : WorldObject
    {
        #region Public.
        [SerializeField, HideInInspector]
        public TreeObjectData Data = new TreeObjectData();
        #endregion

        #region Serialized.
        [Tooltip("Object to show when WorldObject is available.")]
        [SerializeField]
        private GameObject _visualObject;
        #endregion

        protected override void Awake()
        {
            base.Awake();
        }

        public override void UpdateData(WorldObjectData data)
        {
            Data = (TreeObjectData)data;
            ApplyStateVisuals();
        }

        public override bool ObjectNotDefault()
        {
            bool changed = (Data.TreeState != TreeObjectData.TreeStates.Default ||
                               Data.Instantiated);
            return changed;
        }

        public override WorldObjectData ReturnData()
        {
            return Data;
        }

        private void ApplyStateVisuals()
        {
            gameObject.SetActive(Data.TreeState != TreeObjectData.TreeStates.Removed);
            _visualObject.SetActive(Data.TreeState == TreeObjectData.TreeStates.Default);
        }



    }



}