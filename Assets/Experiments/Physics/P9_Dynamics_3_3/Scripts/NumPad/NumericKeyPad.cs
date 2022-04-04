using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_3
{
    public class NumericKeyPad : MonoBehaviour
    {
        [SerializeField] InputGroup target;
        private bool alreadyDotted;
        // Start is called before the first frame update
        void Start()
        {
            alreadyDotted = false;
        }

        public void SetTarget(InputGroup numPadTarget = null)
        {
            this.target = numPadTarget;
            if (null != numPadTarget)
            {
                this.gameObject.SetActive(true);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
        public void KeyClick(string digit)
        {
            string curVal = target.getInputText();
            if (digit == "<<<")
            {
                if (curVal.Length > 0)
                {
                    if (curVal[curVal.Length - 1] == '.')
                    {
                        alreadyDotted = false;
                    }
                    target.setInputText(curVal.Remove(curVal.Length - 1, 1));
                }
            }
            else
            {
                // Debug.Log(target.caretPosition); Debug.Log(target.selectionAnchorPosition);
                if (alreadyDotted && digit == ".")
                {
                    return;
                }
                if (digit == ".")
                {
                    alreadyDotted = target.setInputText(curVal + digit.ToString());
                }
                else
                {
                    target.setInputText(curVal + digit.ToString());
                }
            }
            target.UpdateFinal();
        }
    }
}