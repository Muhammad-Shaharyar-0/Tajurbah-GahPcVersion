using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_3
{
    public class DragDropInventory : MonoBehaviour
    {

        //Initialize Variables
        GameObject getTarget, targetToDisappear;
        // bool isMouseDragging;
        Vector3 offsetValue;
        Vector3 positionOfScreen;

        public GameObject[] Apparatus;
        public GameObject[] ApparatusImages;
        private bool ApparatusClicked;
        private int ApparatusIndex;
        // public Text CM;
        public string DragLayerName, TouchLayerName;

        private int cmdist;

        private float prevPosY;
        //[SerializeField] float slowDownFactor;

        // Use this for initialization
        void Start()
        {
            //PlayerPrefs.DeleteAll();
            ApparatusClicked = false;
            prevPosY = 0.0f;
            // if(slowDownFactor==0.0f){
            //     slowDownFactor = 1.0f;
            // }
            // CM.gameObject.SetActive(false);
        }

        void Update()
        {
            // if (GameObject.FindGameObjectWithTag("10"))
            // {
            //     ApparatusImages[0].SetActive(false);
            // }
            // else
            // {
            //     ApparatusImages[0].SetActive(true);
            // }

            // if (GameObject.FindGameObjectWithTag("MeterRod"))
            // {
            //     ApparatusImages[1].SetActive(false);
            // }
            // else
            // {
            //     ApparatusImages[1].SetActive(true);
            // }
            // if (GameObject.FindGameObjectWithTag("Ball"))
            // {
            //     ApparatusImages[2].SetActive(false);
            // }
            // else
            // {
            //     ApparatusImages[2].SetActive(true);
            // }

            // if (GameObject.FindGameObjectWithTag("Ball"))
            // {
            //     // if (ballController.Ginstance.rayHitting == true)
            //     // {
            //     //     CM.gameObject.SetActive(true);
            //     //     int temp = 0;
            //     //     temp = (int)meterRodController.Ginstance.dist;
            //     //     CM.text = temp.ToString() + " cm";
            //     // }
            //     // else
            //     // {
            //     //     CM.gameObject.SetActive(false);
            //     // }
            // }

            //Mouse Button Press Down
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;

                if (ApparatusClicked == true && (!Apparatus[ApparatusIndex].GetComponent<DropProperties>().OnlyOneInstanceAllowed ||
                    (Apparatus[ApparatusIndex].GetComponent<DropProperties>().OnlyOneInstanceAllowed && GameObject.Find(Apparatus[ApparatusIndex].gameObject.name + "(Clone)") == null)))
                {
                    getTarget = Instantiate(Apparatus[ApparatusIndex]);
                    Vector3 v3Temp = getTarget.transform.position;
                    if (-99 != getTarget.GetComponent<DropProperties>().zOnRelease)
                    {
                        v3Temp.z = getTarget.GetComponent<DropProperties>().zOnRelease;
                    }
                    if (-99 != getTarget.GetComponent<DropProperties>().xOnRelease)
                    {
                        v3Temp.x = getTarget.GetComponent<DropProperties>().xOnRelease;
                    }
                    if (-99 != getTarget.GetComponent<DropProperties>().yOnRelease)
                    {
                        v3Temp.y = getTarget.GetComponent<DropProperties>().yOnRelease;
                    }
                    getTarget.transform.position = v3Temp;
                    if (Vector3.zero != getTarget.GetComponent<DropProperties>().rotationOnSpawn)
                    {
                        getTarget.transform.rotation = Quaternion.Euler(getTarget.GetComponent<DropProperties>().rotationOnSpawn);
                    }
                    if (getTarget.GetComponent<DropProperties>().CallDropHandler && null != getTarget.GetComponent<OnDrop>())
                    {
                        getTarget.GetComponent<OnDrop>().HandleDrop();
                    }
                    if (getTarget.GetComponent<DropProperties>().OnlyOneInstanceAllowed && !getTarget.GetComponent<DropProperties>().DisappearsOnRelease &&
                        getTarget.GetComponent<DropProperties>().CanvasClickToDisappear)
                    {
                        targetToDisappear = getTarget;
                    }
                }
                else
                {
                    getTarget = ReturnClickedObject();
                    if (null != getTarget)
                    {
                        Debug.Log("going to look for DropProperties");
                        // trying to ensure we're dealing with the dragged object at the right level
                        getTarget = ReturnDroppedParent(getTarget.transform);
                    }
                    // in case the previously instantiated inventory element is set to disappear,
                    // when the user clicks elsewhere on the canvas, destroy it (if it hasn't already been destroyed).
                    else if (null != targetToDisappear && targetToDisappear.GetComponent<DropProperties>().OnlyOneInstanceAllowed &&
                        !targetToDisappear.GetComponent<DropProperties>().DisappearsOnRelease &&
                        targetToDisappear.GetComponent<DropProperties>().CanvasClickToDisappear)
                    {
                        Destroy(targetToDisappear);
                    }
                }

                if (getTarget != null)
                {
                    if (null != getTarget.GetComponent<Rigidbody>())
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
                    if (null != getTarget.GetComponent<DropProperties>() && getTarget.GetComponent<DropProperties>().DisappearsOnRelease)
                    {
                        Destroy(getTarget);
                    }
                    else
                    {
                        if (null != getTarget.GetComponent<Rigidbody>() && null != getTarget.GetComponent<DropProperties>())
                        {
                            getTarget.GetComponent<Rigidbody>().isKinematic = getTarget.GetComponent<DropProperties>().isKinematicOnRelease;
                            getTarget.GetComponent<Rigidbody>().useGravity = getTarget.GetComponent<DropProperties>().turnGravityOnOnRelease;

                            getTarget.GetComponent<Rigidbody>().constraints = getTarget.GetComponent<DropProperties>().constraintsOnRelease;

                            if (getTarget.GetComponent<DropProperties>().massOnRelease != -1)
                            {
                                getTarget.GetComponent<Rigidbody>().mass = getTarget.GetComponent<DropProperties>().massOnRelease;
                            }
                        }

                        if (null != getTarget.GetComponent<DropProperties>() && getTarget.GetComponent<DropProperties>().SnapsOnRelease)
                        {
                            // check if dropping where it's supposed to snap
                            Transform SnapTo;
                            if (MarsiveAttack.Ginstance.IsTargetDroppingOnExpectedDestination(out SnapTo))
                            {
                                // if yes, then snap it into the right place
                                SnapTo.GetComponent<SnapTo>().SnapIntoPlace();
                            }
                        }
                    }
                    ApparatusClicked = false;
                    MarsiveAttack.Ginstance.isMouseDragging = false;
                }
            }

            //Is mouse Moving
            if (MarsiveAttack.Ginstance.isMouseDragging)
            {
                //tracking mouse position.
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z);

                // Debug.Log("screen space pos: (" + currentScreenSpace.x.ToString("0.###") + ", " +
                //             currentScreenSpace.y.ToString("0.###") + ", " + currentScreenSpace.z.ToString("0.###") + ")");

                //converting screen position to world position with offset changes.
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace);

                // Debug.Log("world space pos: (" + currentPosition.x.ToString("0.###") + ", " +
                //             currentPosition.y.ToString("0.###") + ", " + currentPosition.z.ToString("0.###") + ")");
                //float posX = 0.0f;
                if (null != getTarget.GetComponent<DropProperties>() && null != getTarget.GetComponent<DropProperties>().constrainXBy)
                {
                    currentPosition.x = getTarget.GetComponent<DropProperties>().constrainXBy.position.x;
                    if (-99 != getTarget.GetComponent<DropProperties>().xOnRelease)
                    {
                        currentPosition.x += getTarget.GetComponent<DropProperties>().xOnRelease;
                    }
                    Debug.Log("anchored to x at: " + currentPosition.x.ToString("0.###"));
                }
                else if (null != getTarget.transform.parent &&
                        null != getTarget.transform.parent.GetComponent<DropProperties>() &&
                        null != getTarget.transform.parent.GetComponent<DropProperties>().constrainXBy)
                {
                    currentPosition.x = getTarget.transform.parent.GetComponent<DropProperties>().constrainXBy.position.x;
                    if (-99 != getTarget.transform.parent.GetComponent<DropProperties>().xOnRelease)
                    {
                        currentPosition.x += getTarget.transform.parent.GetComponent<DropProperties>().xOnRelease;
                    }
                    Debug.Log("anchored by parent to x at: " + currentPosition.x.ToString("0.###"));
                }

                if(null != getTarget.GetComponent<DropProperties>().SuperSloMoTarget && checkSuperSlowMoRange(currentPosition.y, 
                                        getTarget.GetComponent<DropProperties>().SuperSloMoTarget.transform.position.y, 
                                        getTarget.GetComponent<DropProperties>().ySuperSlowMoRangeFactor)){
                    Debug.Log("applying super slow-mo mode with factor: " + getTarget.GetComponent<DropProperties>().ySuperSlowMoSlowDownFactor);
                    currentPosition.y = SlowDownY(currentPosition.y, getTarget.GetComponent<DropProperties>().ySuperSlowMoSlowDownFactor);
                }
                else{
                    Debug.Log("slow-mo mode with factor: " + getTarget.GetComponent<DropProperties>().ySlowDownFactor);
                    currentPosition.y = SlowDownY(currentPosition.y, getTarget.GetComponent<DropProperties>().ySlowDownFactor);
                }
                getTarget.transform.position = currentPosition;
                prevPosY = currentPosition.y;
                //It will update target gameobject's current postion.   
            }
        }

        bool checkSuperSlowMoRange(float currentPosY, float SuperSlowMoTargetY, float SuperSlowMoRangeFactor){
            return (currentPosY >= (SuperSlowMoTargetY*(1-SuperSlowMoRangeFactor)) && 
                    currentPosY <= (SuperSlowMoTargetY*(1+SuperSlowMoRangeFactor)));
        }

        float SlowDownY(float newPosY, float slowDownFactor){
            if(prevPosY==0.0f){
                return newPosY;
            }
            return (slowDownFactor*(newPosY-prevPosY))+prevPosY;
        }

        GameObject ReturnDroppedParent(Transform subject)
        {

            GameObject goSubject = subject.gameObject;
            Debug.Log("looking for parent of " + goSubject.name + " with DropProperties component");
            while (null != subject.transform.parent)
            {
                subject = subject.transform.parent;
                if (null != subject.GetComponent<DropProperties>())
                {
                    Debug.Log("found parent with DropProperties, that is, " + subject.gameObject.name);
                    return subject.gameObject;
                }
            }
            Debug.Log("could not find a parent with the DropProperties component");
            return goSubject;
        }

        //Method to Return Clicked Object
        GameObject ReturnClickedObject()
        {
            GameObject target = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            DebugUserTap(ray.origin, ray.direction);
            return CheckIfClickedObjectBelongsToLayer(ray.origin, ray.direction, DragLayerName);
        }

        private GameObject CheckIfClickedObjectBelongsToLayer(Vector3 origin, Vector3 direction, string layerName)
        {
            RaycastHit[] hits = Physics.RaycastAll(origin, direction, 15.0f);
            if (hits.Length > 0)
            {
                // Debug.Log("hitting " + hits.Length);
                foreach (RaycastHit hit in hits)
                {
                    // Debug.Log("ray hit " + hit.collider.gameObject.name + " with tag " + hit.collider.gameObject.tag + " and layer " + hit.collider.gameObject.layer);
                    // Debug.DrawRay(origin, direction, Color.blue);
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer(layerName))
                    {
                        return hit.collider.gameObject;
                    }
                }
            }
            return null;
        }

        private void DebugUserTap(Vector3 origin, Vector3 direction)
        {
            RaycastHit[] hits = Physics.RaycastAll(origin, direction, 5.0f);
            if (hits.Length > 0)
            {
                // Debug.Log("hitting " + hits.Length);
                foreach (RaycastHit hit in hits)
                {
                    Debug.Log("ray hit " + hit.collider.gameObject.name + " with tag " + hit.collider.gameObject.tag + " and layer " + hit.collider.gameObject.layer);
                }
                Debug.DrawRay(origin, direction, Color.blue);
            }
        }

        public void ClickOnApparatus(int index)
        {
            ApparatusIndex = index;
            ApparatusClicked = true;
        }

        void OnCollisionEnter(UnityEngine.Collision other)
        {
            Destroy(other.gameObject);
        }

        public void startButton()
        {
            cmdist = 0;
            if (GameObject.FindGameObjectWithTag("Ball"))
            {
                GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                // cmdist = (int)meterRodController.Ginstance.dist;
                // stopWatchController.Ginstance.Actualstart = true;
            }
            // stopWatchController.Ginstance.start = true;


        }

        // public void saveButton()
        // {

        //     int No = 1;
        //     int flag = 0;
        //     if (PlayerPrefs.HasKey("No"))
        //     {
        //         No = PlayerPrefs.GetInt("No");
        //         No = No + 1;
        //         PlayerPrefs.SetInt("No", No);
        //     }
        //     else
        //     {
        //         PlayerPrefs.SetInt("No", No);
        //     }



        //     for (int i = 1; i <= PlayerPrefs.GetInt("No"); i++)
        //     {
        //         if (PlayerPrefs.HasKey("S[" + i + "]"))
        //         {
        //             if (PlayerPrefs.GetString("S[" + i + "]").ToString() == cmdist.ToString())
        //             {
        //                 if (!PlayerPrefs.HasKey("T2[" + i + "]"))
        //                 {
        //                     PlayerPrefs.SetString("T2[" + i + "]", stopWatchController.Ginstance.StopwatchTime.ToString("F2"));
        //                     No = No - 1;
        //                     flag = 1;
        //                 }
        //             }

        //         }
        //         else
        //         {
        //             if (flag == 0)
        //             {
        //                 PlayerPrefs.SetString("S[" + i + "]", cmdist.ToString());
        //                 PlayerPrefs.SetString("T1[" + i + "]", stopWatchController.Ginstance.StopwatchTime.ToString("F2"));
        //             }
        //         }
        //     }

        //     PlayerPrefs.SetInt("No", No);

        //     stopWatchController.Ginstance.ResetButton();

        // }
    }
}