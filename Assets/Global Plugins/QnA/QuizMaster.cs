using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizMaster : MonoBehaviour
{
    public Question First;
    [SerializeField] string strQFileName, strQFileName_Pre, strQFileName_Post, strResPathQnA, strAnsFileName;
    [SerializeField] GameObject pnlQuestion;
    [SerializeField] Button btnClose;
    private int qIndex;
    private Question[] QSetRead;
    // public string CloseTo;
    private string sessionID;
    //private Answer[] ASet;
    private AnswerSet[] ASet;
    [SerializeField] string nextScene;
    public string GetSessionID(){
        return sessionID;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("quiz_mode") && PlayerPrefs.GetString("quiz_mode") == "post"){
            nextScene = "trans";
            // pair it with the session ID of the pre-test
            sessionID = PlayerPrefs.GetString("SessionID") + "_post";
            strQFileName = strQFileName_Post;
        }
        else{
            // new session ID
            sessionID = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString("SessionID", sessionID);
            strQFileName = strQFileName_Pre;
            nextScene = "V2";
        }
        if(string.IsNullOrEmpty(strQFileName)){
            strQFileName = "QSetAtwood_Pre";
        }
        if(string.IsNullOrEmpty(strResPathQnA)){
            strResPathQnA = "Assets/qna/";
        }
        if(string.IsNullOrEmpty(strAnsFileName)){
            strAnsFileName = "firstASet";
        }

        // essai();

        QSetRead = (Question[]) JsonConvert.DeserializeObject<Question[]>(ReadFileIntoString(strQFileName));
        SetUpASet();
        // Debug.Log("got this: " + QSetRead[0].no + ": " + QSetRead[0].type + ", " + QSetRead[0].q + ", " + QSetRead[0].ans_min + "-" + QSetRead[0].ans_max);
        // Debug.Log("got this: " + QSetRead[1].no + ": " + QSetRead[1].type + ", " + QSetRead[1].q + ", " + QSetRead[1].ans);
        // Debug.Log("options: " + QSetRead[1].ans_opts.GetOpts());
        // Debug.Log("got this: " + QSetRead[2].no + ": " + QSetRead[2].type + ", " + QSetRead[2].q + ", " + QSetRead[2].ans);

        qIndex = 0;

        pnlQuestion.GetComponent<QuestionManager>().currentQ = QSetRead[qIndex];
        pnlQuestion.GetComponent<QuestionManager>().currentA = ASet[0].answers[qIndex];
        pnlQuestion.GetComponent<QuestionManager>().Populate();
        // WriteString(JsonConvert.SerializeObject(ASet), strAnsFileName);
        // Answer[] ASetRead = (Answer[]) JsonConvert.DeserializeObject<Answer[]>(ReadFileIntoString(strAnsFileName));
        // Debug.Log("got this: " + ASetRead[0].q_no + ": " + ASetRead[0].type + ", " + ASetRead[0].ans_numeric);
        // Debug.Log("got this: " + ASetRead[2].q_no + ": " + ASetRead[2].type + ", " + ASetRead[2].ans_option_index);
    }

    // private void SetUpASet(){
    //     ASet = new Answer[QSetRead.Length];
    //     for(int q = 0; q < QSetRead.Length; q++)
    //     {
    //         ASet.answers[q] = new Answer(this.sessionID);
    //         ASet.answers[q].type = QSetRead[q].type;
    //         ASet.answers[q].q_no = q + 1;
    //     }
    // }

    private void SetUpASet(){
        ASet = new AnswerSet[1];
        ASet[0] = new AnswerSet(this.sessionID, QSetRead);
    }
    public void NextQ(){
        if(qIndex>=(QSetRead.Length-1)){
            return;
        }

        pnlQuestion.GetComponent<QuestionManager>().GetAnswer(ASet[0].answers[qIndex]);
        Debug.Log(ASet[0].answers[qIndex].ToString());
        qIndex++;
        pnlQuestion.GetComponent<QuestionManager>().currentQ = QSetRead[qIndex];
        pnlQuestion.GetComponent<QuestionManager>().currentA = ASet[0].answers[qIndex];
        pnlQuestion.GetComponent<QuestionManager>().Populate();

        if(qIndex==(QSetRead.Length-1)){
            if(!btnClose.IsInteractable()){
                btnClose.interactable = true;
            }
        }
    }

    public void PrevQ(){
        if(qIndex<=0){
            return;
        }
        pnlQuestion.GetComponent<QuestionManager>().GetAnswer(ASet[0].answers[qIndex]);
        Debug.Log(ASet[0].answers[qIndex].ToString());
        qIndex--;
        pnlQuestion.GetComponent<QuestionManager>().currentQ = QSetRead[qIndex];
        pnlQuestion.GetComponent<QuestionManager>().currentA = ASet[0].answers[qIndex];
        pnlQuestion.GetComponent<QuestionManager>().Populate();
    }

    public void CloseQuiz(){
        pnlQuestion.GetComponent<QuestionManager>().GetAnswer(ASet[0].answers[qIndex]);
        Debug.Log(ASet[0].answers[qIndex].ToString());
        // WriteString(JsonConvert.SerializeObject(ASet), strAnsFileName);
        SaveCurrentAnswer(strAnsFileName, true);
        SceneManager.UnloadSceneAsync("quiz", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        SceneManager.LoadScene(nextScene);
    }
    string serializeQ(Question toSerialize)
    {
        return JsonConvert.SerializeObject(toSerialize);
    }
    public void Help()
    {

    }
    string GetPersistanceLocation(){
        string folderPath = strResPathQnA;
        if(Application.isEditor){
            // working in Unity Editor
            folderPath = Path.Combine("Assets/Resources/", folderPath);
        }
        else{
            // working on deployment target
            folderPath = Path.Combine(Application.persistentDataPath, folderPath);
            if(!Directory.Exists(folderPath)){
                Debug.Log("going to create new folder for QnA at " + folderPath);
                Directory.CreateDirectory(folderPath);
            }
        }
        return folderPath;
    }

    void SaveCurrentAnswer(string fileName, bool append = true){
    // Idea and implementation for manipulating Json data as array of bytes, as found here: https://stackoverflow.com/questions/40965645/what-is-the-best-way-to-save-game-state/40966346#40966346
        if(append){
            string path = Path.Combine(GetPersistanceLocation(), fileName + ".json");
            if(File.Exists(path)){
                // some answer sets have already been persisted, so need to append
                byte[] jsonByte = null;
                try
                {
                    // read file as bytes
                    jsonByte = File.ReadAllBytes(path);
                    Debug.Log("Loaded Data from: " + path.Replace("/", "\\"));
                }
                catch (Exception e)
                {
                    Debug.LogWarning("Failed To Load Data from: " + path.Replace("/", "\\"));
                    Debug.LogWarning("Error: " + e.Message);
                }
                //Convert to json string
                string jsonData = Encoding.ASCII.GetString(jsonByte);

                //Convert to array of AnswerSets...
                AnswerSet[] existingAnswerSets = (AnswerSet[])JsonConvert.DeserializeObject<AnswerSet[]>(jsonData);
                //string CurrentAnswerSets = ReadFileIntoString()
                Debug.Log("retrieved answer sets, " + existingAnswerSets.Length);

                // ... and append to it:
                Array.Resize(ref existingAnswerSets, existingAnswerSets.Length + 1);
                Debug.Log("new number of answer sets: " + existingAnswerSets.Length);
                existingAnswerSets[existingAnswerSets.Length - 1] = ASet[0];
                jsonByte = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(existingAnswerSets));
                
                try
                {
                    File.WriteAllBytes(path, jsonByte);
                    Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
                }
                catch (Exception e)
                {
                    Debug.LogWarning("Failed To Save Quiz Response Data to: " + path.Replace("/", "\\"));
                    Debug.LogWarning("Error: " + e.Message);
                }
            }
            else{
                // no need to append, just save the current answer as the first element of an array
                // of AnswerSets
                WriteString(JsonConvert.SerializeObject(ASet), fileName, false);
            }
        }
        else{
            // just save the current answer as the first element of an array
            // of AnswerSets
            WriteString(JsonConvert.SerializeObject(ASet), fileName, false);    
        }
    }

    void WriteString(string toWrite, string fileName, bool append = true)
    {
        string path = Path.Combine(GetPersistanceLocation(), fileName + ".json");
        Debug.Log("full output path: " + path);
        Debug.Log("full output path: " + GetPersistanceLocation());
        Debug.Log("full output path: " + fileName);
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, append);
        writer.WriteLine(toWrite);
        writer.Close();
        
        // //Re-import the file to update the reference in the editor
        // AssetDatabase.ImportAsset(path); 
        // TextAsset asset = Resources.Load("test");

        // //Print the text from the file
        // Debug.Log(asset.text);
    }

    string ReadFileIntoString(string fileName){
        // string path = strResPathQnA + fileName + ".json";
        string path = strResPathQnA + fileName;
        Debug.Log("reading file from: " + path);
        TextAsset reader = Resources.Load(path) as TextAsset;         

        return reader.text;
    }

    void essai(){
        First = new Question();
        First.no = 1;
        First.q = "What is the value of g at the equator?";
        First.type = anstype.numeric;
        First.ans = (9.81f).ToString();
        First.ans_min = 9.8f;
        First.ans_max = 9.9f;
        First.in_series = false;
        WriteString(serializeQ(First), "firstTestQ");

        // Question[] QSet = new Question[4];
        // QSet[0] = First;
        
        // QSet[1] = new Question();
        // QSet[1].no = 2;
        // QSet[1].q = "A heavier body falls to the ground faster than a lighter body. True or False?";
        // QSet[1].type = anstype.boolean;
        // QSet[1].ans = false.ToString();
        // QSet[1].in_series = false;
        // QSet[1].ans_opts = new Opts(2);
        // QSet[1].ans_opts.options[0] =  "true";
        // QSet[1].ans_opts.options[1] = "false";

        // QSet[2] = new Question();
        // QSet[2].no = 3;
        // QSet[2].q = "Which calculation affects the acceleration of the masses?";
        // QSet[2].type = anstype.mcq;
        // QSet[2].ans = 2.ToString();
        // QSet[2].in_series = true;
        // QSet[2].prev_q = 0;
        // QSet[2].next_q = 4;
        // QSet[2].ans_opts = new Opts(5);
        // QSet[2].ans_opts.options[0] =  "m1 - m2";
        // QSet[2].ans_opts.options[1] = "(m1+m2) / (m1-m2)";
        // QSet[2].ans_opts.options[2] = "m1 + m2";
        // QSet[2].ans_opts.options[3] = "m1 / m2";
        // QSet[2].ans_opts.options[4] = "none of the above";
        
        // QSet[3] = new Question();
        // QSet[3].no = 4;
        // QSet[3].q = "Justify your answer.";
        // QSet[3].type = anstype.short_text;
        // QSet[3].in_series = true;
        // QSet[3].prev_q = 3;
        // QSet[3].next_q = 0;

        // Answer[] ASet = new Answer[4];
        // ASet.answers[0] = new Answer(System.Guid.NewGuid().ToString());
        // ASet.answers[0].q_no = 1;
        // ASet.answers[0].type = anstype.numeric;
        // ASet.answers[0].ans_numeric = 9.81f;

        // ASet.answers[1] = new Answer(System.Guid.NewGuid().ToString());
        // ASet.answers[1].q_no = 2;
        // ASet.answers[1].type = anstype.boolean;
        // ASet.answers[1].ans_boolean = false;

        // ASet.answers[2] = new Answer(System.Guid.NewGuid().ToString());
        // ASet.answers[2].q_no = 3;
        // ASet.answers[2].type = anstype.mcq;
        // ASet.answers[2].ans_option_index = 1;

        // ASet.answers[3] = new Answer(System.Guid.NewGuid().ToString());
        // ASet.answers[3].q_no = 4;
        // ASet.answers[3].type = anstype.short_text;
        // ASet.answers[3].ans_short_text = "both the sum and the difference make a difference to the result";

        // WriteString(JsonConvert.SerializeObject(QSet), strQFileName);
        // WriteString(JsonConvert.SerializeObject(ASet), strAnsFileName);

        // Question got = (Question) JsonConvert.DeserializeObject<Question>(ReadFileIntoString("firstTestQ"));
        // Debug.Log("got this: " + got.q + ", " + got.ans_min + "-" + got.ans_max);    
        // Debug.Log("options: " + QSet[2].ans_opts.GetOpts());

    }

}
