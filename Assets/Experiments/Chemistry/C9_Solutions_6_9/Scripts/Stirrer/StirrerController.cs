using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C9_Solutions_6_7
{
    public class StirrerController : MonoBehaviour
    {
        int count = 0;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("StirrerPH"))
            {
                GetComponent<dragObjectsUsingVelocity>().enabled = true;
                GetComponent<objectController>().MakeNonkinematic();
            }
        }

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (other.gameObject.CompareTag("BeakerPH"))
            {
                count++;
                if (count >= 12)
                {
                    LabManager.LabManager_Instance.nextButton.interactable = true;
                }
                else if (other.gameObject.GetComponent<beakerController>())
                {
                    other.gameObject.GetComponent<beakerController>().MixingOfCopperSulphate();
                }
            }
        }

        void DestroyThisObject()
        {
            Destroy(this.gameObject);
        }
    }
}
