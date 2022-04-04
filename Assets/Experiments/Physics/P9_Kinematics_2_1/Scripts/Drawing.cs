using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Kinematics_2_1
{

public class Drawing : MonoBehaviour
{

    //Initialize Variables
    public GameObject getTarget;
    Vector3 positionOfScreen;
    bool isOnGraph = false;

    void Update()
    {
        //Mouse Button Press Down
        if (Input.GetMouseButton(0))
        {
            if (isOnGraph)
            {
                //Converting world position to screen position.
                positionOfScreen = Camera.main.WorldToScreenPoint(getTarget.transform.position);

                //tracking mouse position.
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z);

                GameObject go = Instantiate(getTarget);
                go.transform.SetParent(this.gameObject.transform);
                go.transform.position = currentScreenSpace; 
            }
        }
    }

    public void IsGraph()
    {
        isOnGraph = !isOnGraph;
    }

    public void ResetButton()
    {
        GameObject[] dots = GameObject.FindGameObjectsWithTag("Graph");
        for (int i = 0; i < dots.Length; i++)
        {
            Destroy(dots[i].gameObject);
        }
    } 
}
}