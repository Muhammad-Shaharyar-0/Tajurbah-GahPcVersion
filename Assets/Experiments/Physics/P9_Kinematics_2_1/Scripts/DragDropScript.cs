using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace P9_Kinematics_2_1
{

public class DragDropScript : MonoBehaviour
{

    //Initialize Variables
    GameObject getTarget;
    bool isMouseDragging;
    Vector3 offsetValue;
    Vector3 positionOfScreen;

    public GameObject[] Apparatus;
    public GameObject[] ApparatusImages;
    private bool ApparatusClicked;
    private int ApparatusIndex;
    public Text CM;

    private int cmdist;

    public GameObject AnglePanel;
    public Text AnglePanelText;
    public GameObject StopWatchStartButton;
    public GameObject StopWatchStopButton;
    [SerializeField] Button btnQuit;

    // Use this for initialization
    void Start()
    {
        // PlayerPrefs.DeleteAll();
        ApparatusClicked = false;
        CM.gameObject.SetActive(false);
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Cube1"))
        {
            ApparatusImages[0].SetActive(false);
        }
        else
        {
            ApparatusImages[0].SetActive(true);
        }

        if (GameObject.FindGameObjectWithTag("MeterRod"))
        {
            ApparatusImages[1].SetActive(false);
        }
        else
        {
            ApparatusImages[1].SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Ball"))
        {
            ApparatusImages[2].SetActive(false);
        }
        else
        {
            ApparatusImages[2].SetActive(true);
        }

        if (GameObject.FindGameObjectWithTag("Ball"))
        {
            if (ballController.Ginstance.rayHitting == true)
            {
                CM.gameObject.SetActive(true);
                int temp = 0;
                temp = (int)meterRodController.Ginstance.dist;
                CM.text = temp.ToString() + " cm";
            }
            else
            {
                CM.gameObject.SetActive(false);
            }
        }

        //Mouse Button Press Down
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;

            if (ApparatusClicked == true)
            {
                getTarget = Instantiate(Apparatus[ApparatusIndex]);
            }
            else
            {
                getTarget = ReturnClickedObject(out hitInfo);
            }

            if (getTarget != null)
            {
                getTarget.GetComponent<Rigidbody>().isKinematic = true;
                getTarget.GetComponent<Rigidbody>().useGravity = false;
                isMouseDragging = true;
                //Converting world position to screen position.
                positionOfScreen = Camera.main.WorldToScreenPoint(getTarget.transform.position);
            }
        }

        //Mouse Button Up
        if (Input.GetMouseButtonUp(0))
        {
            if (getTarget != null)
            {
                    getTarget.GetComponent<Rigidbody>().isKinematic = false;
                    getTarget.GetComponent<Rigidbody>().useGravity = true;
                if (getTarget.gameObject.tag == "Ball")
                {
                    getTarget.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
                }

                if (getTarget.tag == "Cube1" || getTarget.tag == "MeterRod")
                {
                    getTarget.GetComponent<Rigidbody>().mass = 5f;
                }
                ApparatusClicked = false;
                isMouseDragging = false;
            }
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

        if(PlayerPrefs.HasKey("No") && PlayerPrefs.GetInt("No") >= 5){
            btnQuit.interactable = true;
        }
    }

    //Method to Return Clicked Object
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Dragable"))
            {
                target = hit.collider.gameObject;
            }
        }
        return target;
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
        if (PlayerPrefs.HasKey("No"))
        {
            Debug.Log("Inif");
            if (PlayerPrefs.GetString("Angle") == PlayerPrefs.GetString("FinalAngle"))
            {
                Debug.Log("Inifif");
                cmdist = 0;
                if (GameObject.FindGameObjectWithTag("Ball"))
                {
                    GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    cmdist = (int)meterRodController.Ginstance.dist;
                    stopWatchController.Ginstance.Actualstart = true;
                }
                stopWatchController.Ginstance.start = true;
                StopWatchStartButton.gameObject.SetActive(false);
                StopWatchStopButton.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Inifelse");
                StopWatchStartButton.gameObject.SetActive(true);
                StopWatchStopButton.gameObject.SetActive(false);
                stopWatchController.Ginstance.ResetButton();
                ShowAnglePanel();
            }
        }
        else
        {
            Debug.Log("Inelse");
            cmdist = 0;
            if (GameObject.FindGameObjectWithTag("Ball"))
            {
                GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                cmdist = (int)meterRodController.Ginstance.dist;
                stopWatchController.Ginstance.Actualstart = true;
            }
            stopWatchController.Ginstance.start = true;
            StopWatchStartButton.gameObject.SetActive(false);
            StopWatchStopButton.gameObject.SetActive(true);

        }
    }

    public void saveButton()
    {

        int No = 1;
        int flag = 0;
        if (PlayerPrefs.HasKey("No"))
        {
            No = PlayerPrefs.GetInt("No");
            No = No + 1;
            PlayerPrefs.SetInt("No", No);
        }
        else
        {
            PlayerPrefs.SetInt("No", No);
            PlayerPrefs.SetString("FinalAngle", PlayerPrefs.GetString("Angle"));
        }



        for (int i = 1; i <= PlayerPrefs.GetInt("No"); i++)
        {
            if (PlayerPrefs.HasKey("S[" + i + "]"))
            {
                if (PlayerPrefs.GetString("S[" + i + "]").ToString() == cmdist.ToString())
                {
                    if (!PlayerPrefs.HasKey("T2[" + i + "]"))
                    {
                        PlayerPrefs.SetString("T2[" + i + "]", stopWatchController.Ginstance.StopwatchTime.ToString("F2"));
                        No = No - 1;
                        flag = 1;
                    }
                }

            }
            else
            {
                if (flag == 0)
                {
                    PlayerPrefs.SetString("S[" + i + "]", cmdist.ToString());
                    PlayerPrefs.SetString("T1[" + i + "]", stopWatchController.Ginstance.StopwatchTime.ToString("F2"));
                }
            }
        }

        PlayerPrefs.SetInt("No", No);

        stopWatchController.Ginstance.ResetButton();

    }

    public void ShowAnglePanel()
    {
        AnglePanel.gameObject.SetActive(true);
        AnglePanelText.text = "Angle of the Meter-Rod should remain same for all calculations. Change it to "+ PlayerPrefs.GetString("FinalAngle")+ "°";
    }

    public void Quit(){
        if(btnQuit.interactable){
            PlayerPrefs.SetString("quiz_mode", "post");
            SceneManager.LoadScene("quiz");
        }
    }
}
}