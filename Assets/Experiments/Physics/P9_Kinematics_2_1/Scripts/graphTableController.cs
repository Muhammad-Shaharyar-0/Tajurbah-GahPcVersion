using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Kinematics_2_1
{
public class graphTableController : MonoBehaviour {

    public GameObject GraphData;
    int No;

    void OnEnable()
    {
        while (this.transform.childCount > 0)
        {
            Transform c = this.transform.GetChild(0);
            c.SetParent(null);
            Destroy(c.gameObject);
        }

        for (int i = 0; i < PlayerPrefs.GetInt("No"); i++)
        {
            No = i + 1;
            GameObject go = Instantiate(GraphData);
            go.transform.SetParent(this.transform, false);
            go.transform.Find("No").GetComponent<Text>().text = No.ToString();
            if (PlayerPrefs.HasKey("2S[" + No + "]"))
            {
                go.transform.Find("2S").GetComponent<InputField>().text = PlayerPrefs.GetString("2S[" + No + "]");
            }
            if (PlayerPrefs.HasKey("T^2[" + No + "]"))
            {
                go.transform.Find("T^2").GetComponent<InputField>().text = PlayerPrefs.GetString("T^2[" + No + "]");
            }
        }
    }
}
}