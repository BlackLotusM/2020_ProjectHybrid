using Mirror;
using UnityEngine;

namespace FirstGearGames.Mirrors.InteractingSceneObjects
{

    public class Interactor : NetworkBehaviour
    {
        public AudioSource Audio;
        public AudioClip clip;
        private void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }
            else
            {
                ClientUpdate();
            }
        }

        private void ClientUpdate()
        {
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
                    PlantRedo PR = hits[i].gameObject.GetComponent<PlantRedo>();
                    if (PR.BloemState == false)
                    {
                        Audio.PlayOneShot(clip);
                        PR.SetState(this.gameObject.GetComponent<PlantManager>());
                        this.gameObject.GetComponent<PlantManager>().PlantsFixed++;
                    }
                }
            }
        }
    }
}