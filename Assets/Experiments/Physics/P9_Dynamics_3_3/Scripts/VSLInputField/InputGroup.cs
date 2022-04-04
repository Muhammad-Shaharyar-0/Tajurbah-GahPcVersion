using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace P9_Dynamics_3_3
{
    public class InputGroup : MonoBehaviour
    {
        public Text lbl;
        [SerializeField] InputField txt;
        [SerializeField] TMPro.TMP_InputField tmpText;
        [SerializeField] Text VSLText;
        public Text lblFinal;
        public Text lblErr;
        public bool allowNull = false;
        [SerializeField] float baseline;
        [SerializeField] string postFix;
        private float finalVal;

        public float GetFinalVal()
        {
            return finalVal;
        }
        public float GetBaseline()
        {
            return baseline;
        }
        public void Activate(bool activate)
        {
            lbl.gameObject.SetActive(activate);
            if (null != VSLText)
            {
                VSLText.GetComponent<InputFieldController>().SetActive(activate);
            }
            else if (null != txt)
            {
                txt.gameObject.SetActive(activate);
            }
            else
            {
                tmpText.gameObject.SetActive(activate);
            }
            if (lblFinal != null)
            {
                finalVal = -1;
                lblFinal.gameObject.SetActive(activate);
            }
            //lblErr.gameObject.SetActive(activate);
        }

        public void UpdateFinal()
        {
            if (lblFinal == null)
            {
                return;
            }
            float temp;
            if (float.TryParse(getInputText(), out temp) || (getInputText() == "" && (temp = 0) == 0))
            {
                finalVal = temp + baseline;
                lblFinal.text = "= " + (finalVal).ToString() + postFix;
            }
            else
            {
                finalVal = -1;
                lblFinal.text = "";
            }
        }

        public string getInputText()
        {
            if (null != VSLText)
            {
                return VSLText.text;
            }
            else if (null != txt)
            {
                return txt.text;
            }
            else
            {
                return tmpText.text;
            }
        }

        public bool setInputText(string strVal)
        {
            if (null != VSLText)
            {
                return VSLText.GetComponent<InputFieldController>().SetText(strVal);
            }
            else if (null != txt)
            {
                txt.text = strVal;
                return true;
            }
            else
            {
                tmpText.text = strVal;
                return true;
            }
        }

        public bool isFocused()
        {

            if (null != VSLText)
            {
                return VSLText.GetComponent<InputFieldController>().IsFocused();
            }
            else if (null != txt)
            {
                Debug.Log("regular text box is focused? " + txt.isFocused);
                return txt.isFocused;
            }
            else
            {
                Debug.Log("TMP text box is focused? " + tmpText.isFocused);
                return tmpText.isFocused;
            }
        }


        public void SetFocus()
        {
            if (null != VSLText)
            {
                StartCoroutine(SelectInputFieldLater(null));
            }
            else if (null != txt)
            {
                StartCoroutine(SelectInputFieldLater(txt));
            }
            else
            {
                StartCoroutine(SelectInputFieldLater(tmpText));
            }
        }

        IEnumerator SelectInputFieldLater(Selectable toSelect = null)
        {
            yield return new WaitForEndOfFrame();
            if (null != toSelect)
            {
                toSelect.Select();
            }

            if (null != txt)
            {
                ((InputField)toSelect).ActivateInputField();
            }
            else if (null != tmpText)
            {
                ((TMP_InputField)toSelect).ActivateInputField();
            }
            else
            {
                VSLText.GetComponent<InputFieldController>().SetFocus();
            }
        }


        public void SetInputFieldColor(Color toSet)
        {
            if (null != VSLText)
            {
                VSLText.color = toSet;
            }
            else if (null != txt)
            {
                txt.GetComponentInChildren<Text>().color = toSet;
            }
            else
            {
                tmpText.GetComponentInChildren<TextMeshProUGUI>().color = toSet;
            }
        }

    }
}