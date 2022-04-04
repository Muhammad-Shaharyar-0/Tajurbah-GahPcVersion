using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace C9_PhysicalStates_5_7
{
    public class BurnerController : MonoBehaviour
    {
        static C9_PhysicalStates_5_7.BurnerController burner_instance;
        public static C9_PhysicalStates_5_7.BurnerController Burner_Instance
        {
            get
            {
                if (burner_instance == null)
                {
                    burner_instance = FindObjectOfType<C9_PhysicalStates_5_7.BurnerController>();
                }
                return burner_instance;
            }
        }

        [Header("Burner On Button")]
        public Button burnerOnButton;

        [Header("Burner Off Button")]
        public Button burnerOffButton;

        GameObject[] hierarchy_obj;
        GameObject burner;

        // Start is called before the first frame update
        void Start()
        {
            burnerOnButton.onClick.AddListener(DisableCrystalsButton);
            burnerOnButton.onClick.AddListener(BurnerIsOn);
            burnerOnButton.onClick.AddListener(EnableTimeFastBtn);
            burnerOnButton.onClick.AddListener(DeleteTimerValue);

            burnerOffButton.onClick.AddListener(C9_PhysicalStates_5_7.StopwatchController.Stopwatch_Instance.ResetWatch);
            burnerOffButton.onClick.AddListener(EnableCrystalsButton);
            burnerOffButton.onClick.AddListener(BurnerIsOff);
            burnerOffButton.onClick.AddListener(DisableTimeBtns);
            burnerOffButton.onClick.AddListener(SaveTimerValue);
        }

        public void EnableBurnerOnButton()
        {
            burnerOnButton.interactable = true;
        }

        void BurnerIsOn()
        {
            SoundsManager.Instance.PlayBurnerSound();
            C9_PhysicalStates_5_7.LabManager.LabManager_Instance.BurnerOn();
        }
        void BurnerIsOff()
        {
            SoundsManager.Instance.StopBurnerSound();
            C9_PhysicalStates_5_7.LabManager.LabManager_Instance.BurnerOff();
        }

        void EnableTimeFastBtn()
        {
            C9_PhysicalStates_5_7.StopwatchController.Stopwatch_Instance.EnableTimeFastButton();
        }
        void DisableTimeBtns()
        {
            C9_PhysicalStates_5_7.StopwatchController.Stopwatch_Instance.DisableTimeButtons();
        }

        void DeleteTimerValue()
        {
            PlayerPrefs.DeleteKey("BurnerOffTime");
        }

        void SaveTimerValue()
        {
            PlayerPrefs.SetInt("BurnerOffTime", C9_PhysicalStates_5_7.StopwatchController.Stopwatch_Instance.mins);
        }

        void EnableCrystalsButton()
        {
            C9_PhysicalStates_5_7.LabManager.LabManager_Instance.watch_crystals = true;
        }
        void DisableCrystalsButton()
        {
            C9_PhysicalStates_5_7.LabManager.LabManager_Instance.watch_crystals = false;
        }
    }
}