using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_1
{
    public class randomWeightController : MonoBehaviour
    {

        public float minValue;
        public float maxValue;

        Rigidbody RB;

        // Start is called before the first frame update
        void Start()
        {
            RB = GetComponent<Rigidbody>();
            RB.mass = Mathf.Round(Random.Range(minValue, maxValue) * 10) / 10;
        }
    }
}
