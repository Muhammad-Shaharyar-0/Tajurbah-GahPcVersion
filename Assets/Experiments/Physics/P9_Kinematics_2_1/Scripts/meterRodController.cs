using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Kinematics_2_1
{

public class meterRodController : MonoBehaviour {

    static meterRodController ginstance;
    public static meterRodController Ginstance
    {
        get
        {
            if (ginstance == null)
            {
                ginstance = FindObjectOfType<meterRodController>();
            }
            return ginstance;
        }
    }

    public Text AngleText;
    public Transform MeterRodS;
    public Transform MeterRodE;
    public float dist;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (GameObject.FindGameObjectWithTag("Ball"))
        {
            if (ballController.Ginstance.rayHitting == true)
            {
                dist = Vector3.Distance(MeterRodS.position, MeterRodE.position);
                dist = dist * 100;
            }
        }

        float angle = Mathf.Abs(transform.eulerAngles.z-360);
        if(angle>=360)
        {
            angle = 0;
        }
        AngleText.text = angle.ToString("f0");

        PlayerPrefs.SetString("Angle", angle.ToString("f0"));


    }
}
}