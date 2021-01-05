
using UnityEngine;

namespace FirstGearGames.Mirrors.SynchronizingBulkSceneObjects
{

    #region Types.
    /* I made the data which must be in sync it's own class so that I
     * could write a serializer for it and pass that over the network. */
    [System.Serializable]
    public class IslandObjectData : WorldObjectData
    {
        #region Types.
        public enum TreeStates
        {
            Default = 0,
            Chopped = 1,
            Removed = 2
        }
        #endregion
        public TreeStates TreeState { get; private set; } = TreeStates.Default;
        public void SetTreeState(TreeStates value) { TreeState = value; }
    }

    #endregion
}