using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class meterod : MonoBehaviour
    {

        // Use this for initialization
        Rigidbody myrig;
        public GameObject dot;
        Transform myTransform;
        void Start()
        {
            myrig = gameObject.GetComponent<Rigidbody>();
            myrig.constraints = RigidbodyConstraints.FreezeRotationZ;

        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnCollisionStay(UnityEngine.Collision collision)
        {
            if (MarsiveAttack.Ginstance.IsSWRunning())
            {
                myrig.constraints = RigidbodyConstraints.None;
            }
        }


        void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (MarsiveAttack.Ginstance.IsSWRunning())
            {
                print("Points colliding: " + other.contacts.Length);
                print("First point that collided: " + other.contacts[0].point);
                GameObject Point = Instantiate(dot, other.contacts[0].point, Quaternion.identity);
                Point.transform.parent = this.transform;
            }
        }
    }
}