using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B9_Bioenergetics_7_1
{
    public class PlaceholderManager : MonoBehaviour
    {
        static PlaceholderManager instance; public static PlaceholderManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlaceholderManager>();
                }
                return instance;
            }
        }

        [Header("Appratus Count")]
        public int appratusCount = 0;

        int no = 0;

        public void IncrementAppratusCount()
        {
            no++;

            CheckAppratusSetupCompleted();
        }

        void CheckAppratusSetupCompleted()
        {
            if (no == appratusCount)
            {
                B9_Bioenergetics_7_1.LabManager.Instance.AppratusSetupCompleted();
            }
        }
    }
}