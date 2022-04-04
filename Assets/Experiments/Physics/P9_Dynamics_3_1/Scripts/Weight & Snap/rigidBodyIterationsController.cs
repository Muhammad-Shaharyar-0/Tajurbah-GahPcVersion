using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_1
{
    public class rigidBodyIterationsController : MonoBehaviour
    {
        public int solverValue = 30;
        Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.solverIterations = solverValue;
            rb.solverVelocityIterations = solverValue;
        }

    }
}
