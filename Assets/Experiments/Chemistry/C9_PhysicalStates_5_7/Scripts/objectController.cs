using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C9_PhysicalStates_5_7
{
    public class objectController : MonoBehaviour
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