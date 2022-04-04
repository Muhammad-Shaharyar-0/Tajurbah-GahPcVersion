using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class DropProperties : MonoBehaviour
    {
        public bool isKinematicOnRelease, turnGravityOnOnRelease;
        public RigidbodyConstraints constraintsOnRelease;
        public float massOnRelease, zOnRelease, xOnRelease, yOnRelease, ySlowDownFactor, ySuperSlowMoRangeFactor, ySuperSlowMoSlowDownFactor;
        public bool SnapsOnRelease, OnlyOneInstanceAllowed, RemainsDraggableOnRelease, CallDropHandler, DisappearsOnRelease, CanvasClickToDisappear;
        public Vector3 rotationOnSpawn;
        public Transform constrainXBy, SuperSloMoTarget;

        // // Start is called before the first frame update
        void Start()
        {
            if(ySlowDownFactor==0.0f){
                ySlowDownFactor = 1.0f;
            }
            if(ySuperSlowMoSlowDownFactor==0.0f){
                ySuperSlowMoSlowDownFactor=1.0f;
            }
            if(ySuperSlowMoRangeFactor==null || ySuperSlowMoRangeFactor==0.0f){
                ySuperSlowMoRangeFactor=0.0f;
            }
            if(SuperSloMoTarget==null){
                ySuperSlowMoRangeFactor=0.0f;
                ySuperSlowMoSlowDownFactor=1.0f;
            }

        }

        // // Update is called once per frame
        // void Update()
        // {

        // }
    }
}