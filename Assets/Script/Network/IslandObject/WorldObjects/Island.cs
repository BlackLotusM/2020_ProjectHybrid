using Mirror;
using UnityEngine;

namespace FirstGearGames.Mirrors.SynchronizingBulkSceneObjects
{

    public class Island : WorldObject
    {
        #region Public.
        [SerializeField, HideInInspector]
        public IslandObjectData Data = new IslandObjectData();
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
            Data = (IslandObjectData)data;
            ApplyStateVisuals();
        }

        public override bool ObjectNotDefault()
        {
            bool changed = (Data.TreeState != IslandObjectData.TreeStates.Default ||
                               Data.Instantiated);
            return changed;
        }

        public override WorldObjectData ReturnData()
        {
            return Data;
        }

        private void ApplyStateVisuals()
        {
            gameObject.SetActive(Data.TreeState != IslandObjectData.TreeStates.Removed);
            _visualObject.SetActive(Data.TreeState == IslandObjectData.TreeStates.Default);
        }
    }
}