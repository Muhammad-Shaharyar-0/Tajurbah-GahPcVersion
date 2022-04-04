using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B9_Bioenergetics_7_1
{
    public class PlaceholderController : MonoBehaviour
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
                B9_Bioenergetics_7_1.PlaceholderManager.Instance.IncrementAppratusCount();
                otherGameObject.transform.position = this.transform.position;
                B9_Bioenergetics_7_1.dragObjectsController.Instance.DropAppratus();

                otherGameObject.GetComponent<B9_Bioenergetics_7_1.LabController>().Makekinematic();

                if (!otherGameObject.gameObject.CompareTag("FunnelPH"))
                {
                    otherGameObject.GetComponent<B9_Bioenergetics_7_1.LabController>().ChangeLayerToDefault();
                }
                this.gameObject.SetActive(false);

                if (nextObject != null)
                {
                    nextObject.SetActive(true);
                }
            }
        }
    }
}