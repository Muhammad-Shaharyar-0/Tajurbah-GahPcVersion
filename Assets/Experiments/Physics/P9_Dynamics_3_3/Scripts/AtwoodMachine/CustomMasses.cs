using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_3
{
    public class CustomMasses : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] InputField txtM1, txtM2;
        [SerializeField] GameObject m1, m2;
        //[SerializeField] RigidBody m1, m2;
        void Start()
        {
            txtM1.text = txtM2.text = 100.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            m1.GetComponent<Rigidbody>().mass = float.Parse(txtM1.text) / 1000;
            m2.GetComponent<Rigidbody>().mass = float.Parse(txtM2.text) / 1000;
        }
    }
}
