using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace C9_Solutions_6_11
{

    
    public class Burner : MonoBehaviour
    {
        static Burner instance;
        public static Burner Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<Burner>();
                }
                return instance;
            }
        }

        public Button on_btn;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void EnableOnBtn()
        {
            on_btn.interactable = true;

            C9_Solutions_6_11.LabManager.LabManager_Instance.nextButton.interactable = true;
        }
    }
}