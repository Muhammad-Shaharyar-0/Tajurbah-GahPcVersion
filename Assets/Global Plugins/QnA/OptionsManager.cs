using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class OptionsManager : MonoBehaviour
{
    [SerializeField] GameObject[] OptsUI;
    public Opts optionSet;
    bool populated = false;
    public bool isMultipleResponse;
    [SerializeField] ToggleGroup optionGroup;

    private void ResetOptsUI(){
        for(int i=0; i<OptsUI.Length; i++){
            OptsUI[i].GetComponent<Text>().text = "--";
            OptsUI[i].transform.parent.gameObject.GetComponent<Toggle>().group = null;
            OptsUI[i].transform.parent.gameObject.GetComponent<Toggle>().isOn = false;
            OptsUI[i].transform.parent.gameObject.SetActive(false);
        }
    }
    public void Populate(Answer curResponse){
        Debug.Log("options count: " + optionSet.count().ToString());
        Debug.Log("optsUI count: " + OptsUI.Length);
        if(null == optionSet || null == OptsUI || OptsUI.Length == 0){
            return;
        }
        ResetOptsUI();
        for(int i=0; i<optionSet.count(); i++){
            if(null != OptsUI[i]){
                OptsUI[i].GetComponent<Text>().text = optionSet.options[i];
                OptsUI[i].transform.parent.gameObject.SetActive(true);
                OptsUI[i].transform.parent.gameObject.GetComponent<Toggle>().group = (isMultipleResponse ? null : optionGroup);
                if(isMultipleResponse){
                    OptsUI[i].transform.parent.gameObject.GetComponent<Toggle>().group = null;
                    if(null != curResponse.ans_multiple_responses){
                        OptsUI[i].transform.parent.gameObject.GetComponent<Toggle>().isOn = curResponse.ans_multiple_responses[i];
                    }
                    else{
                        OptsUI[i].transform.parent.gameObject.GetComponent<Toggle>().isOn = false;
                    }
                }
                else{
                    OptsUI[i].transform.parent.gameObject.GetComponent<Toggle>().group = optionGroup;
                    OptsUI[i].transform.parent.gameObject.GetComponent<Toggle>().isOn = (curResponse.ans_option_index==i);
                }
                populated = true;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        populated = false;
        //Populate();
    }

    // Update is called once per frame
    void Update()
    {
        // if(!populated){
        //     Populate();
        // }
    }

    public int Response(){
        for(int i=0; i<optionSet.count(); i++){
            if(null != OptsUI[i]){
                if(OptsUI[i].transform.parent.gameObject.GetComponent<Toggle>().isOn){
                    return (i);
                }
            }
        }
        return -1;
    }

    public bool[] Responses(){
        bool[] response = new bool[optionSet.count()];
        for(int i=0; i<optionSet.count(); i++){
            if(null != OptsUI[i]){
                if(OptsUI[i].transform.parent.gameObject.GetComponent<Toggle>().isOn){
                    response[i] = true;
                }
                else{
                    response[i] = false;
                }
            }
            else{
                response[i] = false;
            }
        }
        return response;
    }
}
