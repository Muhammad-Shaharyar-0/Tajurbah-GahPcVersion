using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_1
{
    public class setWeightController : MonoBehaviour
    {
        public string KeyName;
        Rigidbody RB;

        string typeOfExperiment;

        private void OnEnable()
        {
            typeOfExperiment = managerController.Instance.ReturnTypeOfExperiment();
            RB = GetComponent<Rigidbody>();
            RB.mass = float.Parse(PlayerPrefs.GetString(typeOfExperiment + KeyName));
        }
    }
}
