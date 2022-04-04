using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C9_Solutions_6_7
{
    public class SpoonController : MonoBehaviour
    {
        Animator anim;

        private void Start()
        {
            anim=GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("SpoonPH"))
            {
                anim.Play("SpoonRotateAnim");
              
            }
        }

        void SpawnParticlesEvent()
        {
            LabManager.LabManager_Instance.EnableCopperSulphateParticles();
        }

        void DestroyThisObject()
        {
            Destroy(this.gameObject);
        }
    }
}