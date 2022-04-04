using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Tajurbah_Gah
{
    public class AssignButtonController : MonoBehaviour
    {
        Button assignButton;
        InputField inputField=null;

        calc calculatorObject;


        private void Start()
        {
            assignButton = GetComponent<Button>();
            calculatorObject = GetComponentInParent<calc>();

            assignButton.onClick.AddListener(SendDataFromCalToInputField);
        }

        public void AssignInputField(InputField Object)
        {
            inputField = Object;
        }

        void SendDataFromCalToInputField()
        {
            if (inputField != null)
            {
                inputField.text = calculatorObject.GetOutputValue();
            }
        }
    }
}
