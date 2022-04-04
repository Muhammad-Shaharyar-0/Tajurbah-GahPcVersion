using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public bool populated;
    public Question currentQ;
    public Answer currentA;
    [SerializeField] GameObject pnlAnswer, lblQ;
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

    public void Populate(){
        if(currentQ==null || currentA==null){
            return;
        }
        Debug.Log(currentQ.q);
        Debug.Log(lblQ.GetComponent<RectTransform>().rect);
        Debug.Log("type of Q: " + currentQ.type + ", is it an MCQ? " + (currentQ.type==anstype.mcq));
        lblQ.GetComponent<TextMeshProUGUI>().text = currentQ.q;
        
        pnlAnswer.GetComponent<AnswerManager>().type = currentQ.type;
        if(currentQ.type==anstype.mcq || currentQ.type==anstype.mrq){
            pnlAnswer.GetComponent<AnswerManager>().choices = currentQ.ans_opts;
        }
        pnlAnswer.GetComponent<AnswerManager>().userResponse = currentA;
        pnlAnswer.GetComponent<AnswerManager>().Populate();
        populated = true;
    }

    public void GetAnswer(Answer ans){
        pnlAnswer.GetComponent<AnswerManager>().GetAnswer(ans);
    }
}
