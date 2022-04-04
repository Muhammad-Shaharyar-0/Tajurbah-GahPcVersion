using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C9_PhysicalStates_5_7
{
    public class placeHolderController : MonoBehaviour
    {
        [Header("Next Object")]
        public GameObject nextObject;
        public bool isVisible = false;


        int flag = 0;

        private void Start()
        {
            if (!isVisible)
            {
                this.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject otherGameObject = other.gameObject;
            while (otherGameObject.transform.parent != null)
            {
                if (otherGameObject.GetComponent<Tajurbah_Gah.ParentCheckController>())
                {
                    break;
                }
                else
                {
                    otherGameObject = otherGameObject.transform.parent.gameObject;
                }
            }

            if (otherGameObject.transform.tag == this.transform.tag && flag == 0)
            {
                SoundsManager.Instance.PlaySnapClickSound();

                flag = 1;
                C9_PhysicalStates_5_7.placeHolderManager.PManager_Instance.IncrementAppratusCount();
                otherGameObject.transform.position = this.transform.position;
                otherGameObject.transform.rotation = this.transform.rotation;
                otherGameObject.GetComponent<Rigidbody>().freezeRotation = true;
                C9_PhysicalStates_5_7.dragObjectsController.Instance.DropAppratus();
                otherGameObject.GetComponent<C9_PhysicalStates_5_7.objectController>().Makekinematic();
                
                //if (!otherGameObject.gameObject.CompareTag("FunnelPH")) // to make funnel dragable so user can observe crystals
                //{
                    otherGameObject.GetComponent<C9_PhysicalStates_5_7.objectController>().ChangeLayerToDefault();
                //}
                this.gameObject.SetActive(false);

                if (nextObject != null)
                {
                    nextObject.SetActive(true);
                }
            }
        }
    }
}