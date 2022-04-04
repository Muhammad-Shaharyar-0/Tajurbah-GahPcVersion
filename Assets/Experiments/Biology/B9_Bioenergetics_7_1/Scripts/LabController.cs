using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B9_Bioenergetics_7_1
{
    public class LabController : MonoBehaviour
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
    }
}