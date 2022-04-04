using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Kinematics_2_1
{

public class ballController : MonoBehaviour
{

    static ballController ginstance;
    public static ballController Ginstance
    {
        get
        {
            if (ginstance == null)
            {
                ginstance = FindObjectOfType<ballController>();
            }
            return ginstance;
        }
    }

    public bool rayHitting;
    private RaycastHit hit;
    private GameObject meterrodend;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        meterrodend = GameObject.FindGameObjectWithTag("MeterRodEnd");

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100))
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.blue);

            if (hit.collider.gameObject.tag == "MeterRod")
            {
                meterrodend.transform.position = hit.point;
                rayHitting = true;
            }
            else
            {
                rayHitting = false;
            }
        }

    }

    void OnCollisionExit(UnityEngine.Collision other)
    {
        if (other.gameObject.tag == "MeterRod" && transform.GetComponent<Rigidbody>())
        {
            stopWatchController.Ginstance.Actualstart = false;
        }
    }
}
}