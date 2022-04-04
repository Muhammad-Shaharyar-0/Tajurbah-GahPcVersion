using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C9_Solutions_6_7
{
    public class dragObjectsController : MonoBehaviour
    {
        static C9_Solutions_6_7.dragObjectsController instance;
        public static C9_Solutions_6_7.dragObjectsController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<C9_Solutions_6_7.dragObjectsController>();
                }
                return instance;
            }
        }

        [Header("Define Layer")]
        public string LayerName = "Dragable";

        [Header("Any Parent")]
        public bool makeParent = false;
        public GameObject Parent;

        GameObject getTarget;
        bool isMouseDragging;
        Vector3 offsetValue;
        Vector3 positionOfScreen;

        [Header("Define Apparatus")]
        public GameObject[] Apparatus;
        public GameObject[] ApparatusImages;
        private bool ApparatusClicked;
        private int ApparatusIndex;

        // Use this for initialization
        void Start()
        {
            ApparatusClicked = false;
        }

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

                getTarget.transform.position = currentPosition;
                //It will update target gameobject's current postion.   
            }
        }

        //Method to Return Clicked Object
        GameObject ReturnClickedObject(out RaycastHit hit)
        {
            GameObject target = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer(LayerName))
                {
                    target = hit.collider.gameObject;

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

        public void ClickOnApparatus(int index)
        {
            ApparatusIndex = index;
            ApparatusClicked = true;
        }

        void MouseButtonUpAction()
        {
            if (getTarget != null)
            {
                Destroy(getTarget.GetComponent<Outline>());

                getTarget.GetComponent<Rigidbody>().isKinematic = false;
                getTarget.GetComponent<Rigidbody>().useGravity = true;
                ApparatusClicked = false;
                isMouseDragging = false;
            }
        }

        void MouseButtonDownAction()
        {
            RaycastHit hitInfo;

            if (ApparatusClicked == true)
            {
                getTarget = Instantiate(Apparatus[ApparatusIndex]);

                if (makeParent)
                {
                    getTarget.transform.parent = Parent.transform;
                }
            }
            else
            {
                getTarget = ReturnClickedObject(out hitInfo);
            }

            if (getTarget != null)
            {
                Outline outline = getTarget.AddComponent<Outline>();
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = Color.yellow;
                outline.OutlineWidth = 5f;

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