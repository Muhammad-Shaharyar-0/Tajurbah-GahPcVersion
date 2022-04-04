using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_3
{
    public class DragDropScript : MonoBehaviour
    {

        public string DragLayerName, TouchLayerName;
        //Initialize Variables
        public Transform MassHangerTouched;
        GameObject getTarget;
        // public bool isMouseDragging;
        Vector3 offsetValue;
        Vector3 positionOfScreen;

        // Use this for initialization
        void Start()
        {

        }

        void Update()
        {

            //Mouse Button Press Down
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;

                getTarget = ReturnClickedObject(out hitInfo);

                if (getTarget != null)
                {
                    if (getTarget.GetComponent<Rigidbody>() != null)
                    {
                        getTarget.GetComponent<Rigidbody>().isKinematic = true;
                        getTarget.GetComponent<Rigidbody>().useGravity = false;
                    }
                    MarsiveAttack.Ginstance.isMouseDragging = true;
                    //Converting world position to screen position.
                    positionOfScreen = Camera.main.WorldToScreenPoint(getTarget.transform.position);
                }
            }

            //Mouse Button Up
            if (Input.GetMouseButtonUp(0))
            {
                if (getTarget != null)
                {
                    Debug.Log("target: " + getTarget.name);
                    if (getTarget.GetComponent<Rigidbody>() == null)
                    {
                        Debug.Log("resetting mass of rigidbody to: " + float.Parse(getTarget.tag) / 1000);
                        getTarget.AddComponent<Rigidbody>();
                        getTarget.GetComponent<Rigidbody>().mass = float.Parse(getTarget.tag) / 1000;
                        getTarget.GetComponent<Rigidbody>().angularDrag = 5f;
                        getTarget.GetComponent<Rigidbody>().drag = 5f;
                        getTarget.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                    }
                    // check if dropping on masshanger
                    Transform MassHanger;
                    if (IsTargetDroppingOnMassHanger(out MassHanger))
                    {
                        // if yes, then snap it into the right place
                        MassHanger.GetComponent<SnapTo>().SnapIntoPlace();
                    }
                    getTarget.GetComponent<Rigidbody>().isKinematic = false;
                    getTarget.GetComponent<Rigidbody>().useGravity = true;
                    MarsiveAttack.Ginstance.isMouseDragging = false;
                }
            }

            //Is mouse Moving
            if (MarsiveAttack.Ginstance.isMouseDragging)
            {
                //tracking mouse position.
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z);

                //converting screen position to world position with offset changes.
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace);

                getTarget.transform.position = currentPosition;
                //It will update target gameobject's current postion.   
            }
        }

        bool IsTargetDroppingOnMassHanger(out Transform MassHanger)
        {
            MassHanger = this.gameObject.GetComponent<MarsiveAttack>().InActiveUse;
            if (null == MassHanger)
            {
                return false;
            }
            if (MassHanger.GetComponent<SnapTo>().GetMassToSnapIntoPlace() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Method to Return Clicked Object
        GameObject ReturnClickedObject(out RaycastHit hit)
        {
            GameObject target = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction, Color.red,180,false);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                // Debug.Log("target's layer: " + hit.collider.gameObject.layer);
                // Debug.Log("target's name: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer(DragLayerName))
                {
                    target = hit.collider.gameObject;
                    if (target.transform.parent)
                    {
                        target = target.transform.parent.gameObject;
                    }
                }
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer(TouchLayerName))
                {
                    MassHangerTouched = hit.collider.transform;
                }
            }
            return target;
        }
    }
}