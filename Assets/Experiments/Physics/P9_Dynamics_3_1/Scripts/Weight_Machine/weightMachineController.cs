using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_1
{
    public class weightMachineController : MonoBehaviour
    {
        public Text DigitalText;

        /*float weightOfObjects;

        // Start is called before the first frame update
        void Start()
        {
            weightOfObjects = 0;
        }

        private void OnCollisionEnter(UnityEngine.Collision other)
        {

            if (other.gameObject.GetComponent<Rigidbody>())
            {
                weightOfObjects = weightOfObjects + other.gameObject.GetComponent<Rigidbody>().mass;
            }

            DigitalText.text = weightOfObjects.ToString() + " kg";
        }

        private void OnCollisionExit(UnityEngine.Collision other)
        {
            if (other.gameObject.GetComponent<Rigidbody>())
            {
                weightOfObjects = weightOfObjects - other.gameObject.GetComponent<Rigidbody>().mass;

                if (weightOfObjects < 0)
                {
                    weightOfObjects = 0;
                }
            }

            DigitalText.text = weightOfObjects.ToString() + " kg";
        }


        */














        float forceToMass;

        public float combinedForce;
        public float calculatedMass;

        public int registeredRigidbodies;

        Dictionary<Rigidbody, float> impulsePerRigidBody = new Dictionary<Rigidbody, float>();

        float currentDeltaTime;
        float lastDeltaTime;

        private void Awake()
        {
            forceToMass = 1f / Physics.gravity.magnitude;
        }

        void UpdateWeight()
        {
            registeredRigidbodies = impulsePerRigidBody.Count;
            combinedForce = 0;

            foreach (var force in impulsePerRigidBody.Values)
            {
                combinedForce += force;
            }

            calculatedMass = (float)(combinedForce * forceToMass);
            if (calculatedMass == Mathf.Infinity || calculatedMass < 0)
            {
                if (calculatedMass < 0)
                {
                    calculatedMass = Mathf.Abs(calculatedMass);
                }
                else
                {
                    calculatedMass = 0;
                }
            }
        }

        private void FixedUpdate()
        {
            lastDeltaTime = currentDeltaTime;
            currentDeltaTime = Time.deltaTime;
            DigitalText.text = (Mathf.Round(calculatedMass * 100) / 100).ToString() + " kg";
        }

        private void OnCollisionStay(UnityEngine.Collision collision)
        {
            if (collision.rigidbody != null)
            {
                if (impulsePerRigidBody.ContainsKey(collision.rigidbody))
                    impulsePerRigidBody[collision.rigidbody] = collision.impulse.y / lastDeltaTime;
                else
                    impulsePerRigidBody.Add(collision.rigidbody, collision.impulse.y / lastDeltaTime);

                UpdateWeight();
            }
        }
        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (collision.rigidbody != null)
            {
                if (impulsePerRigidBody.ContainsKey(collision.rigidbody))
                    impulsePerRigidBody[collision.rigidbody] = collision.impulse.y / lastDeltaTime;
                else
                    impulsePerRigidBody.Add(collision.rigidbody, collision.impulse.y / lastDeltaTime);

                UpdateWeight();
            }
        }
        private void OnCollisionExit(UnityEngine.Collision collision)
        {
            if (collision.rigidbody != null)
            {
                impulsePerRigidBody.Remove(collision.rigidbody);
                UpdateWeight();
            }
        }
    }
}