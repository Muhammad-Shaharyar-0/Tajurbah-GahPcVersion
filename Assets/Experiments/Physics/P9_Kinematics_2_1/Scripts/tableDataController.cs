using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tableDataController : MonoBehaviour {

    public GameObject NoPrefab;

    public void collectData(string changedText)
    {
        string No = NoPrefab.GetComponent<Text>().text;
        string name = gameObject.name.ToString();
        PlayerPrefs.SetString(name + "[" + No + "]", changedText);
    }
}
