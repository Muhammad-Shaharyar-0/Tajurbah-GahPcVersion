using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tajurbah_Gah
{
    public class DragObjectsController : MonoBehaviour
    {
        static DragObjectsController instance;
        public static DragObjectsController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<DragObjectsController>();
                }
                return instance;
            }
        }

        [Header("Define Layer")]
        public string LayerName = "Dragable";

        GameObject getTarget;
        bool isMouseDragging;
        Vector3 offsetValue;
        Vector3 positionOfScreen;

        Outline outline;

        void Update()
        {
            //Mouse Button Press Down
            if (Input.GetMouseButtonDown(0))
            {
                MouseButtonDownAction();
            }

            //Mouse Button Up
            if (Input.GetMouseButtonUp(0))
            {
                MouseButtonUpAction();
            }

            //Is mouse Moving
            if (isMouseDragging)
            {
                //tracking mouse position.
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z);

                //converting screen position to world position with offset changes.
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace);

                getTarget.transform.position = new Vector3(currentPosition.x + offsetValue.x, currentPosition.y + offsetValue.y, currentPosition.z);
                //It will update target gameobject's current postion.   
            }
        }

        //Method to Return Clicked Object
        GameObject ReturnClickedObject(out RaycastHit hit)
        {
            GameObject target = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer(LayerName))
                {
                    target = hit.collider.gameObject;

                    offsetValue = target.transform.position - hit.point;

                    while (target.transform.parent != null)
                    {
                        if (target.GetComponent<Tajurbah_Gah.ParentCheckController>())
                        {
                            break;
                        }
                        else
                        {
                            target = target.transform.parent.gameObject;
                        }
                    }
                }
            }
            return target;
        }

        void MouseButtonUpAction()
        {
            if (getTarget != null)
            {
                outline.enabled = false;

                getTarget.GetComponent<Rigidbody>().isKinematic = false;
                getTarget.GetComponent<Rigidbody>().useGravity = true;
                isMouseDragging = false;
            }
        }

        void MouseButtonDownAction()
        {
            RaycastHit hitInfo;

                getTarget = ReturnClickedObject(out hitInfo);

            if (getTarget != null)
            {
                outline = getTarget.GetComponent<Outline>();
                outline.enabled = true;

                getTarget.GetComponent<Rigidbody>().isKinematic = true;
                getTarget.GetComponent<Rigidbody>().useGravity = false;
                isMouseDragging = true;
                //Converting world position to screen position.
                positionOfScreen = Camera.main.WorldToScreenPoint(getTarget.transform.position);
            }
        }
        public void DropAppratus()
        {
            isMouseDragging = false;
            MouseButtonUpAction();
            getTarget = null;
        }
    }
}