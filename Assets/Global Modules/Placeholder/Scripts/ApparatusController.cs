using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tajurbah_Gah
{ 
    public class ApparatusController : MonoBehaviour
    {
        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Makekinematic()
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        public void ChangeLayerToDefault()
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            ChangeChildLayer("Default");
        }

        void ChangeChildLayer(string layer)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                this.gameObject.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer(layer);
            }
        }

        public void DropApparatus()
        {
            DragObjectsController.Instance.DropAppratus();
        }
    }
}