using Mirror;
using UnityEngine;

namespace FirstGearGames.Mirrors.InteractingSceneObjects
{

    public class Interactor : NetworkBehaviour
    {

        private void Update()
        {
            ClientUpdate();
        }

        private void ClientUpdate()
        {
            if (base.isServer && !base.isClient)
                return;
            if (!base.hasAuthority)
                return;

            if (Input.GetKeyDown(KeyCode.Mouse1))
                TryInteract();
        }

        private void TryInteract()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 0.2f);
            for (int i = 0; i < hits.Length; i++)
            {
                INetworkUsable[] usables = hits[i].GetComponents<INetworkUsable>();
                foreach (INetworkUsable usable in usables)
                {
                    if (usable != null)
                    {
                        if (!base.isServer)
                            usable.Use();
                        CmdUse(usable.GetNetworkIdentity(), usable.GetId());
                    }
                }
            }
        }

        [Command]
        private void CmdUse(NetworkIdentity netIdent, int id)
        {
            INetworkUsable[] usables = netIdent.gameObject.GetComponents<INetworkUsable>();
            for (int i = 0; i < usables.Length; i++)
            {
                if (usables[i].GetId() == id)
                    usables[i].Use();
            }
        }
    }
}