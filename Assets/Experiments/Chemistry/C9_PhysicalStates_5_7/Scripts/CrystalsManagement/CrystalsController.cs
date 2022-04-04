using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C9_PhysicalStates_5_7
{
    public class CrystalsController : MonoBehaviour
    {
        [SerializeField] GameObject naphCrystalsObject;
        ParticleSystem naphCrystalsSystem;
        public GameObject white_smoke;
        public GameObject Black_smoke;
        int timeroff_val;

        void Start()
        {
            naphCrystalsSystem = naphCrystalsObject.GetComponent<ParticleSystem>();
            DisplayResult();
        }

        void CheckTimerVal(int num)
        {
            if (num <= 3)
            {
                white_smoke.gameObject.SetActive(true);
                white_smoke.gameObject.GetComponent<ParticleSystem>().Play();
            }
            if (num >= 5 && num <= 10)
            {
                NoofCrystals(num);
                naphCrystalsSystem.gameObject.SetActive(true);
            }
            if (num > 10)
            {
                Black_smoke.gameObject.SetActive(true);
                Black_smoke.gameObject.GetComponent<ParticleSystem>().Play();
            }
        }

        void NoofCrystals(int num)
        {
            switch (num)
            {
                case 5:
                    {
                        naphCrystalsSystem.emission.SetBursts(new[]
                        {
                              new ParticleSystem.Burst(0f, 10),
                        });
                        break;
                    }
                case 6:
                    {
                        naphCrystalsSystem.emission.SetBursts(new[]
                       {
                              new ParticleSystem.Burst(0f, 20),
                        });
                        break;
                    }
                case 7:
                    {
                        naphCrystalsSystem.emission.SetBursts(new[]
                       {
                              new ParticleSystem.Burst(0f, 30),
                        });
                        break;
                    }
                case 8:
                    {
                        naphCrystalsSystem.emission.SetBursts(new[]
                       {
                              new ParticleSystem.Burst(0f, 40),
                        });
                        break;
                    }
                case 9:
                    {
                        naphCrystalsSystem.emission.SetBursts(new[]
                       {
                              new ParticleSystem.Burst(0f, 50),
                        });
                        break;
                    }
                case 10:
                    {
                        naphCrystalsSystem.emission.SetBursts(new[]
                       {
                              new ParticleSystem.Burst(0f, 50),
                        });
                        break;
                    }
            }
        }

        void DisplayResult()
        {
            timeroff_val = PlayerPrefs.GetInt("BurnerOffTime");
            Debug.Log(timeroff_val + "time val");
            CheckTimerVal(timeroff_val);

        }
    }
}