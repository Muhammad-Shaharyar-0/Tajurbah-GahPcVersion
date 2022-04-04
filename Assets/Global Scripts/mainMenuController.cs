using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuController : MonoBehaviour
{
    [Header("Selected Subject")]
    [SerializeField] Text subjectTitle;
    [SerializeField] GameObject physicsSubject;
    [SerializeField] GameObject chemistrySubject;
    [SerializeField] GameObject biologySubject;

    [Header("Select Chapters And Experiments Container")]
    public GameObject selectChaptersAndExperimentsContainer;

    [Header("Chapters And Experiments")]
    public ChapterAndExperiments physicsCE;
    public ChapterAndExperiments chemistryCE;
    public ChapterAndExperiments biologyCE;

    [Header("Back Button")]
    public Button backButton;

    [Header("Loading Screen")]
    public GameObject loadingScreen;

    [Header("Main Menu Animator")]
    [SerializeField] Animator mainMenuSelectionAnimator;


    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }

    // Start is called before the first frame update
    void Start()
    {
        EnableDisableSelectedSubjects(false, false, false);

        InitializeChaptersAndExperimentsButtons(physicsCE);
        InitializeChaptersAndExperimentsButtons(chemistryCE);
        InitializeChaptersAndExperimentsButtons(biologyCE);

    }

    void InitializeChaptersAndExperimentsButtons(ChapterAndExperiments Object)
    {
        for (int i = 0; i < Object.chapters.Length; i++)
        {
            int count = i;

            Object.chapters[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                EnableDisableSelectedExperiments(physicsCE);
                EnableDisableSelectedExperiments(chemistryCE);
                EnableDisableSelectedExperiments(biologyCE);

                Object.experiments[count].SetActive(true);
                mainMenuSelectionAnimator.Play("Select Chapter Animation");
                backButton.onClick.AddListener(ExperimentSelectBackButton);
            });
        }
    }


    public void SubjectSelected(string Data)
    {
        switch(Data)
        {
            case "Physics":
                EnableDisableSelectedSubjects(true,false,false);
                break;
            case "Chemistry":
                EnableDisableSelectedSubjects(false, true, false);
                break;
            case "Biology":
                EnableDisableSelectedSubjects(false, false, true);
                break;
        }

        subjectTitle.text = Data;
        mainMenuSelectionAnimator.Play("Select Subject Animation");
        backButton.onClick.AddListener(ChapterSelectBackButton);
    }

    void ChapterSelectBackButton()
    {
        mainMenuSelectionAnimator.Play("Select Subject Animation Reversed");
    }
    void ExperimentSelectBackButton()
    {
        mainMenuSelectionAnimator.Play("Select Chapter Animation Reversed");

    }
    void EnableDisableSelectedSubjects(bool SelectPhysics = false, bool SelectChemistry = false, bool SelectBiology = false)
    {
        physicsSubject.SetActive(SelectPhysics);
        chemistrySubject.SetActive(SelectChemistry);
        biologySubject.SetActive(SelectBiology);
    }

    void EnableDisableSelectedExperiments(ChapterAndExperiments Object, bool Status=false)
    {
        for (int i = 0; i < Object.chapters.Length; i++)
        {
            Object.experiments[i].SetActive(Status);
        }
    }


    public void LoadExperiment(string data)
    {
        if (!string.IsNullOrEmpty(data))
        {
            StartCoroutine(LoadNewScene(data));
        }
    }

    IEnumerator LoadNewScene(string data)
    {
        selectChaptersAndExperimentsContainer.SetActive(false);
        loadingScreen.SetActive(true);

        yield return new WaitForSeconds(3);
        AsyncOperation async = SceneManager.LoadSceneAsync(data);
        while (!async.isDone)
        {
            yield return null;
        }
    }

    public void PlayClickSound()
    {
        SoundsManager.Instance.PlayButtonClickSound();
    }
}

[System.Serializable]
public class ChapterAndExperiments
{
    public GameObject[] chapters;
    public GameObject[] experiments;
}