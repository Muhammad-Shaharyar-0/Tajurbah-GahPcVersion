using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_1
{
    public class phase2Controller : MonoBehaviour
    {
        static phase2Controller instance;
        public static phase2Controller Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<phase2Controller>();
                }
                return instance;
            }
        }

        [Header("Objects")]
        public GameObject biggerBlock;
        public GameObject smallBlock;
        public GameObject hanger;

        [Header("PhysicsMaterialsForBlock")]
        public PhysicMaterial woodPM;
        public PhysicMaterial glassPM;
        public PhysicMaterial rubberPM;

        [Header("MaterialsForBlock")]
        public Material woodM;
        public Material glassM;
        public Material rubberM;

        [Header("Save Button")]
        public GameObject saveButton;

        [Header("Wrong Value Canvas")]
        public GameObject wrongValuePanel;
        public GameObject obiRope;

        // Start is called before the first frame update
        void Start()
        {
            InitializePhase2(managerController.Instance.GetEData1(), managerController.Instance.GetEData2());

            Time.timeScale = 0;
        }

        void InitializePhase2(string data1, string data2)
        {
            if (data1 == "w" && data2 == "w")
            {
                biggerBlock.GetComponent<MeshRenderer>().material = woodM;
                biggerBlock.GetComponent<BoxCollider>().material = woodPM;
            }
            if (data1 == "w" && data2 == "g")
            {
                biggerBlock.GetComponent<MeshRenderer>().material = woodM;
                biggerBlock.GetComponent<BoxCollider>().material = woodPM;

                smallBlock.GetComponent<MeshRenderer>().material = glassM;
                smallBlock.GetComponent<BoxCollider>().material = glassPM;
            }
            if (data1 == "g" && data2 == "r")
            {
                biggerBlock.GetComponent<MeshRenderer>().material = rubberM;
                biggerBlock.GetComponent<BoxCollider>().material = rubberPM;

                smallBlock.GetComponent<MeshRenderer>().material = glassM;
                smallBlock.GetComponent<BoxCollider>().material = glassPM;
            }
        }

        public void StartPhase2()
        {
            Time.timeScale = 1;
        }

        public void CheckPhase2Completion()
        {
            if (hanger.GetComponent<getTotalWeightController>().GetTotalWeight() <= 0)
            {

                Destroy(obiRope);
                wrongValuePanel.SetActive(true);
            }
            else
            {
                Tajurbah_Gah.StepsLabelController.Instance.UpdateStep(3,false,()=> { saveButton.SetActive(true); });
            }
        }

        public void GoBackToPhase1()
        {
            managerController.Instance.InitializePhase1();
            Destroy(this.gameObject);
        }
    }
}