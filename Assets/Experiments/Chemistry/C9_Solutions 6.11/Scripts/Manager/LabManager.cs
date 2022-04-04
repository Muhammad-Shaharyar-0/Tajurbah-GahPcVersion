using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace C9_Solutions_6_11
{
    public class LabManager : MonoBehaviour
    {
        static C9_Solutions_6_11.LabManager labmanager_instance;
        public static C9_Solutions_6_11.LabManager LabManager_Instance
        {
            get
            {
                if (C9_Solutions_6_11.LabManager.labmanager_instance == null)
                {
                    C9_Solutions_6_11.LabManager.labmanager_instance = FindObjectOfType<C9_Solutions_6_11.LabManager>();
                }
                return C9_Solutions_6_11.LabManager.labmanager_instance;
            }
        }

        public bool appratusSetupCompleted = false;
        int stepsCompleted = 0;

        public Button OnButton;

        [SerializeField] List<GameObject> Definiations;
        [SerializeField] List<GameObject> Helps;
        [SerializeField] GameObject Steps;
        [SerializeField] GameObject Inventory;
        [SerializeField] GameObject PlaceHolders;
        [SerializeField] GameObject Joystick;
        int activeDefinationIndex;

        bool PracticleCompleted = false;
        public Button nextButton;
        public Button ZoomButton;
        public Button ResetButton;
        // Start is called before the first frame update
        void Start()
        {
            Definiations[0].SetActive(true);
            Steps.SetActive(false);
            Inventory.SetActive(false);
            PlaceHolders.SetActive(false);
            activeDefinationIndex = 0;
            PracticleCompleted = false;
            ZoomButton.interactable = false;
            ResetButton.interactable = false;
            Joystick.GetComponent<Image>().enabled = false;
        }

        private void Update()
        {
            if (appratusSetupCompleted == true)
            {
                if(Tajurbah_Gah.StepsLabelController.Instance!=null)
                {
                    if (stepsCompleted == Tajurbah_Gah.StepsLabelController.Instance.GetNoofSteps())
                    {
                        PracticleCompleted = true;
                        nextButton.interactable = true;

                    }
                }
                
            }
        }
        public void AppratusCompleted()
        {
            appratusSetupCompleted = true;

        }
        public int GetStepsComppleted()
        {
            return stepsCompleted;
        }
        public void StepCompleted()
        {
            Tajurbah_Gah.StepsLabelController.Instance.UpdateStep(stepsCompleted, true);
            stepsCompleted++;
        }
        public void Next()
        {
            if (activeDefinationIndex < Definiations.Count - 1  && Definiations.Count > 1)
            {
                Definiations[activeDefinationIndex].SetActive(false);
                activeDefinationIndex++;
                Definiations[activeDefinationIndex].SetActive(true);
            }
            else if (PracticleCompleted != true)
            {

                Definiations[activeDefinationIndex].SetActive(false);
                Steps.SetActive(true);
                Inventory.SetActive(true);
                PlaceHolders.SetActive(true);
                nextButton.interactable = false;
                ZoomButton.interactable = true;
                ResetButton.interactable = true;
                Joystick.GetComponent<Image>().enabled = true;
            }
            else if(PracticleCompleted == true)
            {
                NextScene();
            }
        }
        //public void BurnerOn()
        //{
        //    EnableExperimentLights();
        //    DisableResetButton();
        //    C9_PhysicalStates_5_7.StopwatchController.Stopwatch_Instance.StartTime();
        //    C9_PhysicalStates_5_7.chinaDishController.Chinadish_Instance.StartVapoursEmission();
        //}

        //public void BurnerOff()
        //{
        //    DisableExperimentLights();
        //    EnableResetButton();
        //    C9_PhysicalStates_5_7.StopwatchController.Stopwatch_Instance.StopTime();
        //    C9_PhysicalStates_5_7.chinaDishController.Chinadish_Instance.StopVapoursEmission();

        //}

        //public void EnableResetButton()
        //{
        //    C9_PhysicalStates_5_7.StopwatchController.Stopwatch_Instance.MakeReset_Interactable();

        //}

        //public void DisableResetButton()
        //{
        //    C9_PhysicalStates_5_7.StopwatchController.Stopwatch_Instance.MakeReset_Noninteractable();
        //}

        //public void StartVapoursRoutine()
        //{
        //    //VapoursController.Vapour_instance.vapours_particle.gameObject.SetActive(true);
        //    //VapoursController.Vapour_instance.Start_Evaporation();
        //}

        //public void StopVapoursRoutine()
        //{
        //    // VapoursController.Vapour_instance.Stop_Evaporation();
        //    //VapoursController.Vapour_instance.vapours_particle.gameObject.SetActive(false);
        //}

        //public void ChangeWatchFlag()
        //{
        //    // StopwatchController.In.SetFlag();
        //}

        public void RestartExperiment()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        public void EndExperiment()
        {
            Application.Quit();
        }
        public void NextScene()
        {
            int currenctScence = SceneManager.GetActiveScene().buildIndex;
            currenctScence++;

            if (currenctScence < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(currenctScence);
            }
            else
            {
                SceneManager.LoadScene(1);
            }

        }
        //void EnableExperimentLights()
        //{
        //    light_1.gameObject.SetActive(false);
        //    light_2.gameObject.SetActive(false);
        //    light_3.gameObject.SetActive(false);
        //}

        //void DisableExperimentLights()
        //{
        //    light_1.gameObject.SetActive(true);
        //    light_2.gameObject.SetActive(true);
        //    light_3.gameObject.SetActive(true);
        //}


    }
}

