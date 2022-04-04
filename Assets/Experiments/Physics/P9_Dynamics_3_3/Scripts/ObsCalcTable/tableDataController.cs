using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_3
{
    public class tableDataController : MonoBehaviour
    {

        public GameObject NoPrefab;
        [SerializeField] string fldName;

        public void collectData(string changedText)
        {
            string No = NoPrefab.GetComponent<Text>().text;
            string name = gameObject.name.ToString();
            PlayerPrefs.SetString(name + "[" + No + "]", changedText);
        }

        public void collectDataForField(string changedText)
        {
            string No = NoPrefab.GetComponent<Text>().text;
            PlayerPrefs.SetString(fldName + "[" + No + "]", changedText);
        }
    }
}