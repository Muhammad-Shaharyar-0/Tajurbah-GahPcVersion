using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace B9_Bioenergetics_7_1
{
    public class TimeManagement : MonoBehaviour
    {
        static TimeManagement time_instance;
        public static TimeManagement Time_instance
        {
            get
            {
                if (time_instance == null)
                {
                    time_instance = FindObjectOfType<TimeManagement>();
                }
                return time_instance;
            }
        }

        Image time_image;
        Button time_slow;
        Button time_fast;
        int time_value = 1;
        int enable_slowflag = 0;
        string time_string;
        int time_no;
        // Start is called before the first frame update
        void Start()
        {
            time_image = GameObject.Find("TimeSpeed").GetComponent<Image>();
            time_slow = GameObject.Find("TimeSlow").GetComponent<Button>();
            time_fast = GameObject.Find("TimeFast").GetComponent<Button>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void ChangeTimescale(int time_val)
        {
            Debug.Log(time_val);
            switch (time_val)
            {
                case 1:
                    Time.timeScale = time_val;
                    break;
                case 2:
                    Time.timeScale = time_val * 40;
                    break;
                case 3:
                    Time.timeScale = time_val * 30;
                    break;
                case 4:
                    Time.timeScale = time_val * 25;
                    break;
            }


            ////time_string = time_image.GetComponentInChildren<Text>().text.ToString();
            //int time_no = int.Parse(time_image.GetComponentInChildren<Text>().text.ToString());
            //Debug.Log("time no ki value ha" + time_no);
            //if(int.TryParse(time_string,out time_no))
            //{
            //    Debug.Log("image ka no: " + time_no);
            //}


        }

        public void SetDefault_Timescale()
        {
            time_value = 1;
            Time.timeScale = time_value;
            time_image.GetComponentInChildren<Text>().text = time_value + "x";
        }

        public void IncreaseTimevalue()
        {
            if (enable_slowflag == 0)
            {
                time_slow.interactable = true;
                enable_slowflag = 1;
            }
            time_value += 1;
            if (time_value > 1)
            {
                time_slow.interactable = true;
            }
            if (time_value >= 4)
            {
                time_fast.interactable = false;
            }
            else if (time_value != 4)
            {
                time_fast.interactable = true;
            }
            Debug.Log("in fast " + time_value);
            ChangeTimescale(time_value);
            time_image.GetComponentInChildren<Text>().text = time_value + "x";
        }

        public void DecreaseTimevalue()
        {
            Debug.Log("in decraes");
            time_value -= 1;
            //Debug.Log(time_value);
            if (time_value == 1)
            {
                time_slow.interactable = false;
            }
            else
            {
                if (time_value < 4)
                {
                    time_fast.interactable = true;
                }
                if (!time_slow.interactable)
                {
                    time_slow.interactable = true;
                }
                Debug.Log("slow time= " + time_value);

            }
            ChangeTimescale(time_value);
            time_image.GetComponentInChildren<Text>().text = time_value + "x";

        }



    }
}