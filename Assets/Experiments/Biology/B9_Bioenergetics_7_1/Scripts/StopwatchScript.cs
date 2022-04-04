using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace B9_Bioenergetics_7_1
{
    public class StopwatchScript : MonoBehaviour
    {
        
        static StopwatchScript instance;
        public static StopwatchScript Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<StopwatchScript>();
                }
                return instance;
            }
        }

        //[Header("Time Text")]
        public Text watch_text;
        public ParticleSystem bubbles;
        public GameObject testPanel;
        public Text txt;

        int sec = 0;
        float hours;
        
        // Start is called before the first frame update
        void Start()
        {
            watch_text.text = sec.ToString();
        }

        public void StartTime()
        {
            SoundsManager.Instance.PlayClockSound();
            StartCoroutine("HoursRoutine");
        }

        IEnumerator HoursRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(5f);
                sec++;
                watch_text.text = sec.ToString();

                if (sec >= 24)
                {
                    testPanel.SetActive(true);
                    txt.GetComponent<Text>().gameObject.SetActive(false);
                    
                    sec = 24;
                    watch_text.text = sec.ToString();
                }
                PlayerPrefs.SetFloat("OxygenTimer", sec);
            }
        }    

        public void StartBubbles()
        {
            StartCoroutine("StartBubblesAnimation");
        }

        IEnumerator StartBubblesAnimation()
        {
            yield return new WaitForSeconds(30);
            bubbles.Play();
        }

        public void StopTime()
        {
            SoundsManager.Instance.StopClockSound();
            StopCoroutine("HoursRoutine");
        }

        public void ResetWatch()
        {
            SoundsManager.Instance.StopClockSound();
            sec = 0;
            watch_text.text = "00";
        }

        public void MakeReset_Interactable()
        {
            this.transform.Find("ResetButton").GetComponent<Button>().interactable = true;
        }

        public void MakeReset_Noninteractable()
        {
            this.transform.Find("ResetButton").GetComponent<Button>().interactable = false;
        }

        public void EnableTimeFastButton()
        {
            this.transform.Find("TimeCollapse").Find("TimeFast").GetComponent<Button>().interactable = true;
        }

        public void DisableTimeButtons()
        {

            this.transform.Find("TimeCollapse").Find("TimeFast").GetComponent<Button>().interactable = false;
            this.transform.Find("TimeCollapse").Find("TimeSlow").GetComponent<Button>().interactable = false;
        }     
    }
}