using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class QuizManager : MonoBehaviour
{


     
    int QuizCompleted = 0;


    [SerializeField] List<GameObject> Quiz;
    [SerializeField] List<GameObject> Helps;
    [SerializeField] List<string> Anwsers;
    [SerializeField] GameObject Message;
    int activeQuizIndex;

    bool PracticleCompleted = false;
    public Button nextButton;
    public Button helpButton;
    // Start is called before the first frame update
    void Start()
    {
        Quiz[0].SetActive(true);
        activeQuizIndex = 0;
        PracticleCompleted = false;
        Debug.Log(Quiz.Count);
        nextButton.interactable=false;
    }


    public void Next()
    {
       
        Message.SetActive(false);
        
        if (activeQuizIndex < Quiz.Count - 1)
        {

            activeQuizIndex++;
            Quiz[activeQuizIndex].SetActive(true);
           
        }
        else
        {
            NextScene();
        }
        if (activeQuizIndex == Quiz.Count - 1)
        {
            nextButton.interactable = true;
            helpButton.interactable = false;
        }
        else
        {
            nextButton.interactable = false;
            helpButton.interactable = true;
        }

    }
    public void Help()
    {
        if (activeQuizIndex != Quiz.Count - 1)
        {
            if (Helps[activeQuizIndex].activeSelf == true)
            {
                Quiz[activeQuizIndex].SetActive(true);
                Helps[activeQuizIndex].SetActive(false);
            }
                    
            else
            {
                Quiz[activeQuizIndex].SetActive(false);
                Helps[activeQuizIndex].SetActive(true);
            }
            
        }
    }

    public void RestartExperiment()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextScene()
    {
        int currenctScence = SceneManager.GetActiveScene().buildIndex;
        currenctScence++;

        if (currenctScence < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currenctScence);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
            
    }
    public void CheckAnswer()
    {

        Quiz[activeQuizIndex].SetActive(false);
        
        if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text== Anwsers[activeQuizIndex])
        {
            Message.GetComponentInChildren<Text>().text = "Correct Answer";
            Message.SetActive(true);
        }
        else
        {
            Message.GetComponentInChildren<Text>().text = "Worng Answer \nCorrect Answer was "+ Anwsers[activeQuizIndex];
            Message.SetActive(true);
        }
        nextButton.interactable = true;
        helpButton.interactable = false;
    }
    public void EndExperiment()
    {
        Application.Quit();
    }

     

}
