using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_1
{
    public class saveWeightsDataController : MonoBehaviour
    {
        public Button nextButton1;

        string weightOfBlock = null;
        string weightOfHanger = null;

        public Button nextButton2;

        string weightOnBlock = null;
        string weightOnHanger = null;

        public Button resetButton;

        string typeOfExperiment;


        private void Start()
        {
            typeOfExperiment = managerController.Instance.ReturnTypeOfExperiment();

            if (nextButton1 != null)
            {
                nextButton1.onClick.AddListener(TriggerManagerForPhase2);
            }

            if (nextButton2 != null)
            {
                nextButton2.onClick.AddListener(TriggerManagerForTable);
            }

            if (resetButton != null)
            {
                resetButton.onClick.AddListener(TriggerManagerForReset);
            }
        }

        public void WeightOfBlock(string Data)
        {
            weightOfBlock = Data;

            CheckDataAndEnablenextButton1();
        }

        public void WeightOfHanger(string Data)
        {
            weightOfHanger = Data;

            CheckDataAndEnablenextButton1();
        }

        public void WeightOnBlock(string Data)
        {
            weightOnBlock = Data;

            CheckDataAndEnablenextButton2();
        }

        public void WeightOnHanger(string Data)
        {
            weightOnHanger = Data;

            CheckDataAndEnablenextButton2();
        }

        public void CheckDataAndEnablenextButton1()
        {
            if (!string.IsNullOrEmpty(weightOfBlock) && !string.IsNullOrEmpty(weightOfHanger))
            {
                PlayerPrefs.SetString(typeOfExperiment + "W1", weightOfBlock);
                PlayerPrefs.SetString(typeOfExperiment + "W3", weightOfHanger);

                if(nextButton1.interactable!=true)
                {         
                    Tajurbah_Gah.StepsLabelController.Instance.UpdateStep(1,false,()=> { nextButton1.interactable = true; });
                }
            }
            else
            {
                nextButton1.interactable = false;
            }
        }

        public void CheckDataAndEnablenextButton2()
        {
            if (!string.IsNullOrEmpty(weightOnBlock) && !string.IsNullOrEmpty(weightOnHanger))
            {
                PlayerPrefs.SetString(typeOfExperiment + "W2", weightOnBlock);
                PlayerPrefs.SetString(typeOfExperiment + "W4", weightOnHanger);

                nextButton2.interactable = true;
            }
            else
            {
                nextButton2.interactable = false;
            }
        }

        void TriggerManagerForPhase2()
        {
            managerController.Instance.Phase1Completed();
        }
        void TriggerManagerForTable()
        {
            SaveObservations();
            managerController.Instance.Phase2Completed();
        }
        void TriggerManagerForReset()
        {
            managerController.Instance.ResetPhase2();
        }

        void SaveObservations()
        {
            int No = 1;
            if (PlayerPrefs.HasKey(typeOfExperiment + "No"))
            {
                No = PlayerPrefs.GetInt(typeOfExperiment + "No");
                No = No + 1;
                PlayerPrefs.SetInt(typeOfExperiment + "No", No);
            }
            else
            {
                PlayerPrefs.SetInt(typeOfExperiment + "No", No);
            }

            PlayerPrefs.SetString(typeOfExperiment + "W1[" + No + "]", PlayerPrefs.GetString(typeOfExperiment + "W1"));
            PlayerPrefs.SetString(typeOfExperiment + "W2[" + No + "]", PlayerPrefs.GetString(typeOfExperiment + "W2"));
            PlayerPrefs.SetString(typeOfExperiment + "W3[" + No + "]", PlayerPrefs.GetString(typeOfExperiment + "W3"));
            PlayerPrefs.SetString(typeOfExperiment + "W4[" + No + "]", PlayerPrefs.GetString(typeOfExperiment + "W4"));
        }

    }
}
