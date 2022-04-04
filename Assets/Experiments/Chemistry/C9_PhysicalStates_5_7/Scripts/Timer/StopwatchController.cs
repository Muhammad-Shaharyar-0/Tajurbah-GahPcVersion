using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace C9_PhysicalStates_5_7
{
    public class StopwatchController : MonoBehaviour
    {
        public Action CallBack;
        int MinutesForCallBack = -1;


        static C9_PhysicalStates_5_7.StopwatchController stopwatch_instance;
        public static C9_PhysicalStates_5_7.StopwatchController Stopwatch_Instance
        {
            get
            {
                if (stopwatch_instance == null)
                {
                    stopwatch_instance = FindObjectOfType<C9_PhysicalStates_5_7.StopwatchController>();
                }
                return stopwatch_instance;
            }
        }

        [Header("Time Text")]
        public Text watch_text;

        public int mins;
        float sec;
        float time;
        int flag = 0;

        // Start is called before the first frame update
        void Start()
        {
            time = 0;
        }

        public void StartTime()
        {
            SoundsManager.Instance.PlayClockSound();

            StartCoroutine("StartTimeRoutine");
        }

        IEnumerator StartTimeRoutine()
        {
            while (true)
            {
                CalculateTime();
                yield return null;
            }
        }

        void CalculateTime()
        {
            time += Time.deltaTime;
            sec = (int)(time % 60);
            mins = (int)((time / 60) % 60);
            watch_text.text = mins.ToString("00") + ":" + sec.ToString("00");

            if(mins==MinutesForCallBack)
            {
                CallBack?.Invoke();
                CallBack = null;
            }
        }

        public void CallBackActionOnTime(int Minutes, Action CallBack)
        {
            MinutesForCallBack = Minutes;
            this.CallBack= CallBack;
        }


        public void StopTime()
        {
            SoundsManager.Instance.StopClockSound();

            StopCoroutine("StartTimeRoutine");
        }

        public void ResetWatch()
        {
            SoundsManager.Instance.StopClockSound();

            time = 0;
            watch_text.text = "00:00:00";
            C9_PhysicalStates_5_7.TimeManagement.Time_instance.SetDefault_Timescale();
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
            this.transform.Find("TimeCollapse").Find("TimerFast").GetComponent<Button>().interactable = true;
        }

        public void DisableTimeButtons()
        {

            this.transform.Find("TimeCollapse").Find("TimerFast").GetComponent<Button>().interactable = false;
            this.transform.Find("TimeCollapse").Find("TimerSlow").GetComponent<Button>().interactable = false;
        }
    }
}
