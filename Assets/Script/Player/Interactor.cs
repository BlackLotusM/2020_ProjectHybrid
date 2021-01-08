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
            //if (base.isServer && !base.isClient)
            //    return;
            //if (!base.hasAuthority)
            //    return;

            if (Input.GetKeyDown(KeyCode.Mouse1))
                TryInteract();
        }

        private void TryInteract()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 0.6f);
            for (int i = 0; i < hits.Length; i++)
            {
                if(hits[i].tag == "Bloem")
                {
                    hits[i].gameObject.GetComponent<PlantRedo>().SetState(this.gameObject.GetComponent<PlantManager>());
                }
            }
        }
    }
}