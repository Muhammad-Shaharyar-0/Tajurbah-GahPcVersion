using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Tajurbah_Gah;
using P9_Kinematics_2_1;

namespace C9_PhysicalStates_5_7
{
    public class LabManager : MonoBehaviour
    {
        static C9_PhysicalStates_5_7.LabManager labmanager_instance;
        public static C9_PhysicalStates_5_7.LabManager LabManager_Instance
        {
            get
            {
                if (labmanager_instance == null)
                {
                    labmanager_instance = FindObjectOfType<C9_PhysicalStates_5_7.LabManager>();
                }
                return labmanager_instance;
            }
        }

        public bool watch_crystals=false;
        public GameObject watch_crystalsbtn;
        public bool appratusSetupCompleted = false;
        GameObject burner;
        GameObject[] hierarchy_obj;
        public Guidance_transform[] guide;
        int no = 0;


        // Start is called before the first frame update
        void Start()
        {
            StepsLabelController.Instance.UpdateStep(0, false, null);

            StopwatchController.Stopwatch_Instance.CallBackActionOnTime(5, () => { StepsLabelController.Instance.UpdateStep(3, false, null); });
        }

        private void Update()
        {
            if(watch_crystals)
            {
                watch_crystalsbtn.gameObject.SetActive(true);
                watch_crystals = false;
            }
        }
        public void AppratusSetupCompleted()
        {
            StepsLabelController.Instance.UpdateStep(1, false, null);
            appratusSetupCompleted = true;
            C9_PhysicalStates_5_7.BurnerController.Burner_Instance.EnableBurnerOnButton();
        }

        public void BurnerOn()
        {
            StepsLabelController.Instance.UpdateStep(2, false, null);
            DisableResetButton();
            C9_PhysicalStates_5_7.StopwatchController.Stopwatch_Instance.StartTime();
            C9_PhysicalStates_5_7.chinaDishController.Chinadish_Instance.StartVapoursEmission();
        }

        public void BurnerOff()
        {
            StepsLabelController.Instance.UpdateStep(4, false, null);
            EnableResetButton();
            C9_PhysicalStates_5_7.StopwatchController.Stopwatch_Instance.StopTime();
            C9_PhysicalStates_5_7.chinaDishController.Chinadish_Instance.StopVapoursEmission();
            watch_crystalsbtn.gameObject.SetActive(true);

        }

        public void EnableResetButton()
        {
            C9_PhysicalStates_5_7.StopwatchController.Stopwatch_Instance.MakeReset_Interactable();

        }

        public void DisableResetButton()
        {
            C9_PhysicalStates_5_7.StopwatchController.Stopwatch_Instance.MakeReset_Noninteractable();
        }

        public void StartVapoursRoutine()
        {
            //VapoursController.Vapour_instance.vapours_particle.gameObject.SetActive(true);
            //VapoursController.Vapour_instance.Start_Evaporation();
        }

        public void StopVapoursRoutine()
        {
            // VapoursController.Vapour_instance.Stop_Evaporation();
            //VapoursController.Vapour_instance.vapours_particle.gameObject.SetActive(false);
        }

        public void ChangeWatchFlag()
        {
            // StopwatchController.In.SetFlag();
        }

        public void RestartExperiment()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void EndExperiment()
        {
            Application.Quit();
        }

        public void ShowCrystals()
        {
            SceneManager.LoadScene("C9_PhysicalStates_5_7_Results");
        }
    }
}

