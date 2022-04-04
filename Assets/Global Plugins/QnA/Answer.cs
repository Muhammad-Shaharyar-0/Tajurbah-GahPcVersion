using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Answer{
    public int q_no {get; set;}
    public anstype type {get; set;}
    public string ans_short_text {get; set;}
    public string ans_long_text {get; set;}
    public float? ans_numeric {get; set;}
    public bool? ans_boolean {get; set;}
    public int ans_option_index {get; set;}
    public bool[] ans_multiple_responses{get; set;}
    public string session_id {get; set;}
    public static string GetTimestamp(DateTime value)
    {
        // return value.ToString("yyyyMMddHHmmssffff");
        return value.ToString("yyyyMMddHHmmss");
    }
    public string timestamp {get; set;}

    [JsonConstructor]
    public Answer(){
        ans_option_index = -1;
    }
    public Answer(string uid){
        timestamp = GetTimestamp(DateTime.Now);
        session_id = uid;
        ans_option_index = -1;
    }

    public override string ToString(){
        string stringified = "no: " + q_no.ToString() + ", type: " + type.ToString() + ", session_id: " + session_id + ", timestamp: " + timestamp;
        switch(type){
            case anstype.numeric:
                stringified += ", ans_numeric: " + ans_numeric;
                break;
            case anstype.mcq:
                stringified += ", ans_option_index: " + ans_option_index;
                break;
            case anstype.boolean:
                stringified += ", ans_boolean: " + ans_boolean;
                break;
            case anstype.short_text:
                stringified += ", ans_short_text: " + ans_short_text;
                break;
            case anstype.long_text:
                stringified += ", ans_long_text: " + ans_long_text;
                break;
            default:
                break;
        }

        return stringified;
    }
}

public class AnswerSet{
    public string sessionID {get; set;}
    public string timestamp {get; set;}
    public Answer[] answers {get; set;}
    public static string GetTimestamp(DateTime value)
    {
        // return value.ToString("yyyyMMddHHmmssffff");
        return value.ToString("yyyyMMddHHmmss");
    }

    [JsonConstructor]
    public AnswerSet(){
        
    }
    public AnswerSet(string sID, int size){
        timestamp = GetTimestamp(DateTime.Now);
        sessionID = sID;
        answers = new Answer[size];
        for(int q = 0; q < size; q++)
        {
            answers[q] = new Answer();
            answers[q].timestamp = this.timestamp;
            answers[q].session_id = this.sessionID;
            answers[q].q_no = q + 1;
        }
    }
    public AnswerSet(string sID, Question[] QSet){
        timestamp = GetTimestamp(DateTime.Now);
        sessionID = sID;
        answers = new Answer[QSet.Length];
        for(int q = 0; q < QSet.Length; q++)
        {
            answers[q] = new Answer();
            answers[q].timestamp = this.timestamp;
            answers[q].session_id = this.sessionID;
            answers[q].type = QSet[q].type;
            answers[q].q_no = q + 1;
        }
    }
}
