using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace C9_Solutions_6_11
{
    public class placeHolderManager : MonoBehaviour
    {
        static C9_Solutions_6_11.placeHolderManager pmanager_instance;
        public static C9_Solutions_6_11.placeHolderManager PManager_Instance
        {
            get
            {
                if (C9_Solutions_6_11.placeHolderManager.pmanager_instance == null)
                {
                    C9_Solutions_6_11.placeHolderManager.pmanager_instance = FindObjectOfType<C9_Solutions_6_11.placeHolderManager>();
                }
                return C9_Solutions_6_11.placeHolderManager.pmanager_instance;
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
                C9_Solutions_6_11.LabManager.LabManager_Instance.AppratusCompleted();
            }
        }
    }
}