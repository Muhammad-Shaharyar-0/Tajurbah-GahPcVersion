using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class moveZoomCameraMag : moveZoomCamera
    {
        [SerializeField] bool AnchoredToMetreRod;
        // Start is called before the first frame update
        void Start()
        {

        }

        void FixedUpdate()
        {
            if (null != base.ToFollow())
            {
                Vector3 tmpPos = this.transform.position;
                tmpPos.y = base.ToFollow().position.y + base.YCorrection();
                if (AnchoredToMetreRod && null != base.MetreRod())
                {
                    tmpPos.x = base.MetreRod().transform.position.x;
                }
                else
                {
                    tmpPos.x = base.ToFollow().position.x;
                }
                tmpPos.z = base.ToFollow().position.z + base.ZDist();
                this.transform.position = tmpPos;
            }
        }
    }
}