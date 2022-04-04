using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class moveZoomCamera : MonoBehaviour
    {
        [SerializeField] Transform toFollow, metreRod;
        [SerializeField] float yCorrection, zDist;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {

            Vector3 tmpPos = this.transform.position;
            tmpPos.y = toFollow.position.y + yCorrection;
            tmpPos.x = metreRod.position.x;
            tmpPos.z = metreRod.position.z + zDist;
            this.transform.position = tmpPos;
        }

        protected float YCorrection()
        {
            return yCorrection;
        }

        protected float ZDist()
        {
            return zDist;
        }

        protected Transform ToFollow()
        {
            return toFollow;
        }

        protected Transform MetreRod()
        {
            return metreRod;
        }

        public void SetLeader(Transform leader)
        {
            toFollow = leader;
        }
    }
}