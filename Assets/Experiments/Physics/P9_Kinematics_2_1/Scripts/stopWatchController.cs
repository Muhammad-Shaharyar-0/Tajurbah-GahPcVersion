using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace P9_Kinematics_2_1
{
public class stopWatchController : MonoBehaviour {

    static stopWatchController ginstance;
    public static stopWatchController Ginstance
    {
        get
        {
            if (ginstance == null)
            {
                ginstance = FindObjectOfType<stopWatchController>();
            }
            return ginstance;
        }
    }

    public Text StopwatchTimeText;
    public bool start = false;
    public AudioSource TimerSound;
    public float StopwatchTime = 0;

    public Text ActualStopwatchTimeText;
    public bool Actualstart = false;
    private float ActualStopwatchTime = 0;

    private GameObject saveButton;

    public DragDropScript DragDropScriptObject;

    // Use this for initialization
    void Start () {

        ActualStopwatchTimeText.gameObject.SetActive(false);
        saveButton = GameObject.FindGameObjectWithTag("SaveButton");
        saveButton.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

        if (start == true)
        {
            if(TimerSound.isPlaying)
            {

            }
            else
            {
                TimerSound.Play();
            }
            StopwatchTime += 1 * Time.deltaTime;
            StopwatchTimeText.text = StopwatchTime.ToString("F2");
        }
        if (Actualstart == true)
        {
            ActualStopwatchTime += 1 * Time.deltaTime;
            ActualStopwatchTimeText.text = ActualStopwatchTime.ToString("F2");
        }

        if(Input.GetKey(KeyCode.Space))
        {
            StopButton();
        }

    }

    public void StopButton()
    {
        start = false;
        TimerSound.Stop();
        if (GameObject.FindGameObjectWithTag("Ball"))
        {
            ActualStopwatchTimeText.gameObject.SetActive(true);
            saveButton.SetActive(true);
        }
        DragDropScriptObject.StopWatchStartButton.gameObject.SetActive(true);
        DragDropScriptObject.StopWatchStopButton.gameObject.SetActive(false);
    }
    public void ResetButton()
    {
        start = false;
        TimerSound.Stop();
        StopwatchTime = 0;
        StopwatchTimeText.text = 0.ToString();
        ActualStopwatchTime = 0;
        ActualStopwatchTimeText.gameObject.SetActive(false);
        saveButton.SetActive(false);
    }
}
}
