using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_1
{
    public class tableInputDataController : MonoBehaviour
    {

        public GameObject NoPrefab;

        string typeOfExperiment;
        private void Start()
        {
            typeOfExperiment = managerController.Instance.ReturnTypeOfExperiment();
        }

        public void collectData(string changedText)
        {
            string No = NoPrefab.GetComponent<Text>().text;
            string name = gameObject.name.ToString();
            PlayerPrefs.SetString(typeOfExperiment + name + "[" + No + "]", changedText);
        }
    }
}