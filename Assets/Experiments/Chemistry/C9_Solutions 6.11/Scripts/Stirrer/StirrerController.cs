using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C9_Solutions_6_11
{
    public class StirrerController : MonoBehaviour
    {
        int count = 0;
       
     
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("StirrerPH"))
            {
               // GetComponent<dragObjectsUsingVelocity>().enabled = true;
               // GetComponent<objectController>().MakeNonkinematic();
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                GetComponent<Animator>().enabled = true;
            }
        }
        void DestroyThisObject()
        {
            Destroy(this.gameObject);
        }
    }
}
