using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace C9_Solutions_6_7
{
    public class LabManager : MonoBehaviour
    {
        static C9_Solutions_6_7.LabManager labmanager_instance;
        public static C9_Solutions_6_7.LabManager LabManager_Instance
        {
            get
            {
                if (C9_Solutions_6_7.LabManager.labmanager_instance == null)
                {
                    C9_Solutions_6_7.LabManager.labmanager_instance = FindObjectOfType<C9_Solutions_6_7.LabManager>();
                }
                return C9_Solutions_6_7.LabManager.labmanager_instance;
            }
        }

        public bool watch_crystals=false;
        public GameObject watch_crystalsbtn;
        public bool appratusSetupCompleted = false;
        int no = 0;

        public Button OnButton;
        public GameObject otherBeakerPlaceholder;
        public GameObject copperSulphateParticles;
        public GameObject spoon;

        public Button nextButton;
        // Start is called before the first frame update
        void Start()
        {
            nextButton.interactable = false;
        }

        private void Update()
        {
            if(watch_crystals)
            {
                watch_crystalsbtn.gameObject.SetActive(true);
                watch_crystals = false;
            }
            //if(appratusSetupCompleted)
            //{
            //    C9_Solutions_6_7.BurnerManager.burner_instance.EnableOnButton();
            //    appratusSetupCompleted = false;
            //}
        }
        public void AppratusCompleted()
        {
            appratusSetupCompleted = true;
            C9_Solutions_6_7.Burner.Instance.EnableOnBtn();
           
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

        public void NextPhase2()
        {
            SceneManager.LoadScene("Copper2");
        }
        public void NextPhase3()
        {
            SceneManager.LoadScene("Copper3");
        }
        public void NextPhase4()
        {
            SceneManager.LoadScene("Copper4");
        }

        public void EndExperiment()
        {
            Application.Quit();
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

        public void EnableOtherBeakerPlaceholder()
        {
            otherBeakerPlaceholder.SetActive(true);
        }

        public void EnableCopperSulphateParticles()
        {
            copperSulphateParticles.SetActive(true);
        }
    }
}

