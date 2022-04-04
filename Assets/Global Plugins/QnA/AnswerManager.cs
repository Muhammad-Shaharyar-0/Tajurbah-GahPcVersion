using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    public bool populated;
    public anstype type;
    public Opts choices;
    public Answer userResponse;
    [SerializeField] GameObject pnlOptions, pnlText, pnlNumeric, pnlBoolean;
    // Start is called before the first frame update
    void Start()
    {
        populated = false;
        Populate();
    }

    // Update is called once per frame
    void Update()
    {
        if(!populated){
            Populate();
        }        
    }
    public void ActivatePanel(GameObject pnlToActivate){
        pnlBoolean.SetActive(false);
        pnlText.SetActive(false);
        pnlNumeric.SetActive(false);
        pnlOptions.SetActive(false);
        pnlToActivate.SetActive(true);
    }

    public void Populate(){

        switch(type){
            case anstype.numeric:
                HandleNumeric();
                break;
            case anstype.mcq:
                HandleOptions(false);
                break;
            case anstype.mrq:
                HandleOptions(true);
                break;
            case anstype.boolean:
                HandleBoolean();
                break;
            case anstype.short_text:
                HandleText(false);
                break;
            case anstype.long_text:
                HandleText(true);
                break;
            default:
                populated = false;
                return;
        }
        populated = true;
    }

    private void HandleOptions(bool isMultipleResponse = false){
        Debug.Log("about to populate options");
        Debug.Log(choices.GetOpts());
        ActivatePanel(pnlOptions);
        pnlOptions.GetComponent<OptionsManager>().optionSet = choices;
        pnlOptions.GetComponent<OptionsManager>().isMultipleResponse = isMultipleResponse;
        pnlOptions.GetComponent<OptionsManager>().Populate(userResponse);
    }
    
    private void HandleNumeric(){
        ActivatePanel(pnlNumeric);
        pnlNumeric.GetComponent<AnswerManagerNumeric>().Populate(userResponse.ans_numeric);
    }
    private void HandleText(bool isLongText = false){
        ActivatePanel(pnlText);
        pnlText.GetComponent<AnswerManagerText>().Populate(isLongText ? userResponse.ans_long_text : userResponse.ans_short_text);
    }
    private void HandleBoolean(){
        ActivatePanel(pnlBoolean);
        pnlBoolean.GetComponent<AnswerManagerBoolean>().Populate(userResponse.ans_boolean);
    }

    public void GetAnswer(Answer ans){
        switch(ans.type){
            case anstype.numeric:
                ans.ans_numeric = GetAnsNumeric();
                break;
            case anstype.mcq:
                ans.ans_option_index = GetAnsOption();
                break;
            case anstype.mrq:
                ans.ans_multiple_responses = GetAnsOptions();
                break;
            case anstype.boolean:
                ans.ans_boolean = GetAnsBoolean();
                break;
            case anstype.short_text:
                ans.ans_short_text = GetAnsText();
                break;
            case anstype.long_text:
                ans.ans_long_text = GetAnsText();
                break;
            default:
                break;
        }
    }

    private float? GetAnsNumeric(){
        return pnlNumeric.GetComponent<AnswerManagerNumeric>().Response();
    }

    private bool? GetAnsBoolean(){
        return pnlBoolean.GetComponent<AnswerManagerBoolean>().Response();
    }

    private string GetAnsText(){
        return pnlText.GetComponent<AnswerManagerText>().Response();
    }

    private int GetAnsOption(){
        return pnlOptions.GetComponent<OptionsManager>().Response();
    }
    
    private bool[] GetAnsOptions(){
        return pnlOptions.GetComponent<OptionsManager>().Responses();
    }
    
}
