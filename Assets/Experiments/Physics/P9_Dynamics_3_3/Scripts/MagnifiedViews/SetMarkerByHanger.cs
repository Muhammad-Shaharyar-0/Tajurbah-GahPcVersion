using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class SetMarkerByHanger : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Image fromLeft, fromRight;
        [SerializeField] Transform myHanger;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (myHanger.position.x < 0)
            {
                //Debug.Log("MassHanger1");
                fromLeft.enabled = true;
                fromRight.enabled = false;
            }
            else
            {
                //Debug.Log("MassHanger2");
                fromRight.enabled = true;
                fromLeft.enabled = false;
            }
        }
    }
}
