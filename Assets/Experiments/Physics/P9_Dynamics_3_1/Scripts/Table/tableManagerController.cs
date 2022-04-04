using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_1
{
    public class tableManagerController : MonoBehaviour
    {
        public Button backButton;
        public GameObject averageUSPanel;
        public GameObject confirmButton;

        string typeOfExperiment;
        private void Start()
        {
            typeOfExperiment = managerController.Instance.ReturnTypeOfExperiment();

            averageUSPanel.SetActive(false);
            confirmButton.SetActive(false);
        }

        public void CheckTableCompletion()
        {
            if (PlayerPrefs.GetInt(typeOfExperiment + "No") >= 3)
            {
                backButton.interactable = false;
                averageUSPanel.SetActive(true);
            }
        }

        public void CollectData(string changedText)
        {
            PlayerPrefs.SetString(typeOfExperiment + "AverageUS", changedText);

            if (!confirmButton.activeSelf)
            {
                Tajurbah_Gah.StepsLabelController.Instance.UpdateStep(5, false, null);
                confirmButton.SetActive(true);
            }
        }
        public void ConfirmUSValue()
        {
            managerController.Instance.ExperimentCompleted();
        }
    }

}