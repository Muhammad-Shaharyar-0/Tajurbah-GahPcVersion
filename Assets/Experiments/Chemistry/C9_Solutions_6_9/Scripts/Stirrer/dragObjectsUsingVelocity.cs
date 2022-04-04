using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C9_Solutions_6_7
{
    public class dragObjectsUsingVelocity : MonoBehaviour
    {
        public float distanceFromCamera;
        Rigidbody r;

        Vector3 lastPos;


        private void Start()
        {
            distanceFromCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
            r = GetComponent<Rigidbody>();
        }



        private void Update()
        {
            if(Input.GetMouseButton(0))
            {
                Vector3 pos = Input.mousePosition;
                pos.z = distanceFromCamera;
                pos = Camera.main.ScreenToWorldPoint(pos);
                r.velocity = (pos - transform.position) * 20;
                
            }
            if(Input.GetMouseButtonUp(0))
            {
                r.velocity = Vector3.zero;
            }
        }
    }
}





