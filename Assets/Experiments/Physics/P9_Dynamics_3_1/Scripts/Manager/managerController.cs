using System;
using System.Collections;
using System.Collections.Generic;
using Tajurbah_Gah;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace P9_Dynamics_3_1
{
    public class managerController : MonoBehaviour
    {
        static managerController instance;
        public static managerController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<managerController>();
                }
                return instance;
            }
        }


        [Header("Select Experiment Screen")]
        public GameObject selectexperiment;

        [Header("Phases")]
        public GameObject phase1;
        public GameObject phase2;

        private GameObject phase1Object = null;
        private GameObject phase2Object = null;

        string eData1;
        string eData2;

        [Header("Data Table")]
        public GameObject DataTable;

        // Start is called before the first frame update
        void Start()
        {
            //PlayerPrefs.DeleteAll();
        }

        public void ChoosenExperiment(string data1, string data2)
        {
            eData1 = data1;
            eData2 = data2;

            StartExperiment();
        }
        public string GetEData1()
        {
            return eData1;
        }
        public string GetEData2()
        {
            return eData2;
        }

        void StartExperiment()
        {
            InitializePhase1();
        }

        public void InitializePhase1()
        {
            StepsLabelController.Instance.UpdateStep(0,false,null);
            if (phase1Object != null)
            {
                Destroy(phase1Object);
            }
            phase1Object = Instantiate(phase1, transform.position, Quaternion.identity);
            phase1Object.SetActive(true);
        }

        public void Phase1Completed()
        {
            Destroy(phase1Object);
            InitializePhase2();
            FindObjectOfType<numpadController>().CloseNumPad();
        }

        public void Phase2Completed()
        {
            Destroy(phase2Object);
            DataTable.SetActive(true);
            FindObjectOfType<numpadController>().CloseNumPad();
            StepsLabelController.Instance.UpdateStep(4, false, null);
        }

        public void InitializePhase2()
        {

            StepsLabelController.Instance.UpdateStep(2, false, null);
            if (phase2Object != null)
            {
                Destroy(phase2Object);
            }
            phase2Object = Instantiate(phase2, transform.position, Quaternion.identity);
            phase2Object.SetActive(true);
        }

        public void ResetPhase2()
        {
            InitializePhase2();
        }

        public string ReturnTypeOfExperiment()
        {
            return eData1 + eData2;
        }

        public void ExperimentCompleted()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}