using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tableController : MonoBehaviour {

    public GameObject Data;
    int No;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }
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
            GameObject go = Instantiate(Data);
            go.transform.SetParent(this.transform, false);
            go.transform.Find("No").GetComponent<Text>().text = No.ToString();
            go.transform.Find("S").GetComponent<Text>().text = PlayerPrefs.GetString("S[" + No + "]");
            go.transform.Find("T1").GetComponent<Text>().text = PlayerPrefs.GetString("T1[" + No + "]");
            if (PlayerPrefs.HasKey("T2[" + No + "]"))
            {
                go.transform.Find("T2").GetComponent<Text>().text = PlayerPrefs.GetString("T2[" + No + "]");
            }
            if (PlayerPrefs.HasKey("T[" + No + "]"))
            {
                go.transform.Find("T").GetComponent<InputField>().text =PlayerPrefs.GetString("T[" + No + "]");
            }
            if (PlayerPrefs.HasKey("2S[" + No + "]"))
            {
                go.transform.Find("2S").GetComponent<InputField>().text = PlayerPrefs.GetString("2S[" + No + "]");
            }
            if (PlayerPrefs.HasKey("T^2[" + No + "]"))
            {
                go.transform.Find("T^2").GetComponent<InputField>().text = PlayerPrefs.GetString("T^2[" + No + "]");
            }
            if (PlayerPrefs.HasKey("a[" + No + "]"))
            {
                go.transform.Find("a").GetComponent<InputField>().text = PlayerPrefs.GetString("a[" + No + "]");
            }
            if (PlayerPrefs.HasKey("g[" + No + "]"))
            {
                go.transform.Find("g").GetComponent<InputField>().text = PlayerPrefs.GetString("g[" + No + "]");
            }
        }
    }

}
