using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace P9_Kinematics_2_1
{
public class graphController : MonoBehaviour {

    public GameObject[]  horizontalLabels;
    public GameObject[] verticalLabels;
    int No = 0;
    float[] yCordinates;
    float[] xCordinates;
    double yoffset=0;
    double xoffset=0;

    // Use this for initialization
    void Start () {
		
	}

    void OnEnable()
    {
        yoffset = 0;
        xoffset = 0;

        yCordinates = new float[PlayerPrefs.GetInt("No")];
        xCordinates = new float[PlayerPrefs.GetInt("No")];

        for (int i = 0; i < PlayerPrefs.GetInt("No"); i++)
        {
            No = i + 1;
            yCordinates[i] = float.Parse(PlayerPrefs.GetString("T^2[" + No + "]"));
            xCordinates[i] = float.Parse(PlayerPrefs.GetString("2S[" + No + "]"));

        }

        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                yoffset = System.Math.Round(Mathf.Min(yCordinates), 1);
                xoffset = System.Math.Round(Mathf.Min(xCordinates), 0);
            }
            else
            {
                yoffset = yoffset + 0.2;
                xoffset = xoffset + (Mathf.Max(xCordinates) - Mathf.Min(xCordinates)) / 8;
            }

            yoffset = System.Math.Round(yoffset, 1);
            xoffset = System.Math.Round(xoffset, 0);

    
            horizontalLabels[i].GetComponent<TextMeshProUGUI>().text = yoffset.ToString();
           
            verticalLabels[i].GetComponent<TextMeshProUGUI>().text = xoffset.ToString();
        }
    }

}
}