using Mirror;
using UnityEngine;

namespace FirstGearGames.Mirrors.InteractingSceneObjects
{

    [RequireComponent(typeof(UsableIdAssigner))]
    public class SlidingDoor : NetworkBehaviour, INetworkUsable
    {

        #region Serialized.
        [SerializeField]
        private GameObject _plantHappy = null;
        [SerializeField]
        private GameObject _plantSad = null;
        #endregion

        #region Private.
        [SerializeField]
        private bool _open = false;

        [SyncVar]
        private int _id;
        #endregion

        public override void OnStartServer()
        {
            if (isClient && isServer)
            {
                int t = Random.Range(0, 3);
                if (t == 1)
                {
                    _open = true;
                }
                else
                {
                    _open = false;
                }

                RpcUpdate(_plantHappy, _plantSad, _open);
            }
        }
        private void Update()
        {
            MoveDoor();
        }

        private void MoveDoor()
        {
            if (isServer)
            {
                RpcUpdate(_plantHappy, _plantSad, _open);
            }
        }

        public void Use()
        {
            if (base.isServer)
            {
                _open = !_open;
                RpcUpdate(_plantHappy, _plantSad, _open);
            }
        }

        [ClientRpc]
        void RpcUpdate(GameObject p1, GameObject p2, bool _open)
        {
            p1.GetComponent<MeshRenderer>().enabled = _open;
            p2.GetComponent<MeshRenderer>().enabled = !_open;
        }
        public NetworkIdentity GetNetworkIdentity()
        {
            return base.netIdentity;
        }

        public void SetId(int value)
        {
            _id = value;
        }

        public int GetId()
        {
            return _id;
        }
    }
}