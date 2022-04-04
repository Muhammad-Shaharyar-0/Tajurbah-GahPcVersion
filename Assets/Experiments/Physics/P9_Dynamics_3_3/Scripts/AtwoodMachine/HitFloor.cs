using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class HitFloor : MonoBehaviour
    {
        [SerializeField] bool checkActualFloor;
        [SerializeField] float FloorLevel, yCorrection, margin;
        [SerializeField] Transform me, PseudoFloor, otherMassHanger, ActualFloor, MetreRod;
        [SerializeField] StackInfo myStack, otherStack;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (checkActualFloor)
            {
                if (MarsiveAttack.Ginstance.state == MarsiveState.stabilised &&
                    ActualFloor.position.y < otherMassHanger.position.y)
                {
                    ActualFloor.Translate(0f, otherMassHanger.position.y + yCorrection - ActualFloor.position.y, 0f);

                    Debug.Log("about to adjust MR");
                    MarsiveAttack.Ginstance.FinalAdjustmentToMetreRod();
                    Debug.Log("after adjusting");
                }
            }
            else
            {
                if (MarsiveAttack.Ginstance.state == MarsiveState.stabilised &&
                    PseudoFloor.position.y < otherMassHanger.position.y)
                {
                    PseudoFloor.Translate(0f, otherMassHanger.position.y + yCorrection - PseudoFloor.position.y, 0f);
                    // not sure whether to adjust metre rod in this case??
                }
            }
            if (MarsiveAttack.Ginstance.state == MarsiveState.grounded &&
                (
                    (Mathf.Abs(this.transform.position.y - otherMassHanger.position.y) < this.margin)
                    ||
                    (myStack.GetTotalMass() == 0 && otherStack.GetTotalMass() == 0)
                    ||
                    (myStack.GetTotalMass() < otherStack.GetTotalMass())
                )
                )
            {
                MarsiveAttack.Ginstance.state = MarsiveState.crashed;
            }
        }

        void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (checkActualFloor)
            {
                if (other.gameObject.tag == "Floor")
                {
                    MarsiveAttack.Ginstance.state = MarsiveState.grounded;
                    Debug.Log("grounded by collision");
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!checkActualFloor)
            {
                if (other.gameObject.tag == "Floor")
                {
                    MarsiveAttack.Ginstance.state = MarsiveState.grounded;
                    Debug.Log("grounded by trigger");
                }
            }
        }
    }
}