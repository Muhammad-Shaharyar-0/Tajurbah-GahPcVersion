using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C9_PhysicalStates_5_7
{
    public class chinaDishController : MonoBehaviour
    {

        static C9_PhysicalStates_5_7.chinaDishController chinadish_instance;
        public static C9_PhysicalStates_5_7.chinaDishController Chinadish_Instance
        {
            get
            {
                if (chinadish_instance == null)
                {
                    chinadish_instance = FindObjectOfType<C9_PhysicalStates_5_7.chinaDishController>();
                }
                return chinadish_instance;
            }
        }

        [Header("Vapours System")]
        public ParticleSystem vapoursParticle;

        private void Start()
        {
            vapoursParticle.Stop();
        }

        public void StartVapoursEmission()
        {
            vapoursParticle.Play();
        }

        public void StopVapoursEmission()
        {
            vapoursParticle.Stop();
        }
    }
}
