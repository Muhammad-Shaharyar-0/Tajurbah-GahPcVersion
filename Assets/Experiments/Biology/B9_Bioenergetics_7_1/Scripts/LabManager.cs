using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace B9_Bioenergetics_7_1
{
    public class LabManager : MonoBehaviour
    {
        static LabManager instance;
        public static LabManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<LabManager>();
                }
                return instance;
            }
        }

        public bool appratusSetupCompleted = false;
        GameObject burner;
        GameObject[] hierarchy_obj;
        //    public Guidance_transform[] guide;
        int no = 0;

        public Button testButton;
        public ParticleSystem bubbleParticles;

        // Start is called before the first frame update
        void Start()
        {
            testButton.gameObject.SetActive(false);
        }

        public void AppratusSetupCompleted()
        {
            appratusSetupCompleted = true;
            testButton.gameObject.SetActive(true);
            B9_Bioenergetics_7_1.StopwatchScript.Instance.StartTime();
            bubbleParticles.Play();
        }

        public void BurnerOn()
        {
            SoundsManager.Instance.PlayBurnerSound();
            B9_Bioenergetics_7_1.StopwatchScript.Instance.StartTime();
            //    chinaDishController.Instance.StartVapoursEmission();
        }

        public void BurnerOff()
        {
            SoundsManager.Instance.StopBurnerSound();
            B9_Bioenergetics_7_1.StopwatchScript.Instance.StopTime();
            //  chinaDishController.Instance.StopVapoursEmission();
        }

        public void EnableResetButton()
        {
            B9_Bioenergetics_7_1.StopwatchScript.Instance.MakeReset_Interactable();

        }

        public void DisableResetButton()
        {
            B9_Bioenergetics_7_1.StopwatchScript.Instance.MakeReset_Noninteractable();
        }

        public void Test()
        {
            SoundsManager.Instance.StopClockSound();    
            SceneManager.LoadScene("B9_Bioenergetics_7_1_Test");
        }

        public void RestartExperiment()
        {
            SceneManager.LoadScene("B9_Bioenergetics_7_1_Hydrilla");
        }

        public void EndExperiment()
        {
            Application.Quit();
        }
    }
}
