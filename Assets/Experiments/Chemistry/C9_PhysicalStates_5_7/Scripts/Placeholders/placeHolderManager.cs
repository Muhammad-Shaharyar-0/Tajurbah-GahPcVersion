using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace C9_PhysicalStates_5_7
{
    public class placeHolderManager : MonoBehaviour
    {
        static placeHolderManager pmanager_instance;
        public static placeHolderManager PManager_Instance
        {
            get
            {
                if (pmanager_instance == null)
                {
                    pmanager_instance = FindObjectOfType<C9_PhysicalStates_5_7.placeHolderManager>();
                }
                return pmanager_instance;
            }
        }

        [Header("Appratus Count")]
        public int appratusCount = 0;

        int no = 0;

        public void IncrementAppratusCount()
        {
            no++;

            CheckAppratusSetupCompleted(no);
        }

        void CheckAppratusSetupCompleted(int num)
        {
            if (num == appratusCount)
            {
                C9_PhysicalStates_5_7.LabManager.LabManager_Instance.AppratusSetupCompleted();
            }
        }
    }
}