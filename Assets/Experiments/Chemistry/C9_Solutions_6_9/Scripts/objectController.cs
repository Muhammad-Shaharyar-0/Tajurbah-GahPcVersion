using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C9_Solutions_6_7
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

        public void MakeNonkinematic()
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        public void ChangeLayerToDefault()
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            ChangeChildLayer("Default");
        }

        public void ChangeLayerToDragable()
        {
            this.gameObject.layer = LayerMask.NameToLayer("Dragable");
            ChangeChildLayer("Dragable");
        }

        void ChangeChildLayer(string layer)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                this.gameObject.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer(layer);
            }
        }

        public void EnableMeshCollider()
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshCollider>().enabled = true;
            
        }
    }
}