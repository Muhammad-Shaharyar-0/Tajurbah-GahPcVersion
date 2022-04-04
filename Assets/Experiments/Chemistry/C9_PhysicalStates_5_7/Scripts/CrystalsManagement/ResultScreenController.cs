using System.Collections;
using System.Collections.Generic;
using Tajurbah_Gah;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace C9_PhysicalStates_5_7
{
    public class ResultScreenController : MonoBehaviour
    {
        public GameObject timerPanel;
        public Text timer_text;
        public Button result_button;
        int time = 10;

        [SerializeField] PlayableDirector timeLine;
        
        // Start is called before the first frame update
        void Start()
        {
            StepsLabelController.Instance.UpdateStep(0, false, null);

            Time.timeScale = 1;
            timer_text.text = time.ToString();
            StartCoroutine("StartDelay");
            StartCoroutine("StartTimer");
            //result_button.onClick.AddListener(StartCrystalsAnimation);
        }

        IEnumerator StartDelay()
        {
            yield return new WaitForSeconds(6f);
        }

        IEnumerator StartTimer()
        {
            while(true)
            {
                timer_text.text = time.ToString();
                time--;
                if (time < 0)
                {
                    timerPanel.SetActive(false);
                    result_button.interactable = true;
                   
                }
                yield return new WaitForSeconds(1f);
            }
        }

        public void TimeLinePlayed()
        {
            StepsLabelController.Instance.UpdateStep(1, false, null);
        }
        public void ObserveButtonClick()
        {
            timeLine.Play();

            if(CameraController.Instance)
            {
                CameraController.Instance.followWaypointCallBack.Invoke();
            }
        }
    }
}
