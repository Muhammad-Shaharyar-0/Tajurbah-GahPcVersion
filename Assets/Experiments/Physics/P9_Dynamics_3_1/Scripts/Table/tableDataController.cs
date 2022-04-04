using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_1
{
    public class tableDataController : MonoBehaviour
    {
        public GameObject Data;
        int No;

        string typeOfExperiment;
        private void Start()
        {
            typeOfExperiment = managerController.Instance.ReturnTypeOfExperiment();
        }

        void OnEnable()
        {
            StartCoroutine("FillTableDataRoutine");
        }

        IEnumerator FillTableDataRoutine()
        {
            yield return null;

            transform.GetComponentInParent<tableManagerController>().CheckTableCompletion();

            while (this.transform.childCount > 0)
            {
                Transform c = this.transform.GetChild(0);
                c.SetParent(null);
                Destroy(c.gameObject);
            }

            for (int i = 0; i < PlayerPrefs.GetInt((typeOfExperiment + "No")); i++)
            {
                No = i + 1;
                GameObject go = Instantiate(Data);
                go.name = "Observation " + i;
                go.transform.SetParent(this.transform, false);
                go.transform.Find("No").GetComponent<Text>().text = No.ToString();
                go.transform.Find("W1").GetComponent<Text>().text = PlayerPrefs.GetString(typeOfExperiment + "W1[" + No + "]");
                go.transform.Find("W2").GetComponent<Text>().text = PlayerPrefs.GetString(typeOfExperiment + "W2[" + No + "]");
                go.transform.Find("W3").GetComponent<Text>().text = PlayerPrefs.GetString(typeOfExperiment + "W3[" + No + "]");
                go.transform.Find("W4").GetComponent<Text>().text = PlayerPrefs.GetString(typeOfExperiment + "W4[" + No + "]");

                if (PlayerPrefs.HasKey(typeOfExperiment + "R[" + No + "]"))
                {
                    go.transform.Find("R").GetComponent<InputField>().text = PlayerPrefs.GetString(typeOfExperiment + "R[" + No + "]");
                }
                if (PlayerPrefs.HasKey(typeOfExperiment + "fs[" + No + "]"))
                {
                    go.transform.Find("fs").GetComponent<InputField>().text = PlayerPrefs.GetString(typeOfExperiment + "fs[" + No + "]");
                }
                if (PlayerPrefs.HasKey(typeOfExperiment + "US[" + No + "]"))
                {
                    go.transform.Find("US").GetComponent<InputField>().text = PlayerPrefs.GetString(typeOfExperiment + "US[" + No + "]");
                }
            }
        }

        public void BackButton()
        {
            managerController.Instance.ResetPhase2();
        }
    }
}