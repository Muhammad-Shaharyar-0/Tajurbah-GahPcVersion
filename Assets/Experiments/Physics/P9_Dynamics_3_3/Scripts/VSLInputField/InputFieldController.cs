using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace P9_Dynamics_3_3
{
    // I am re-using and extending code shared by "Programmer" on SO: 
    // https://stackoverflow.com/questions/41391708/how-to-detect-click-touch-events-on-ui-and-gameobjects#41392130
    public class InputFieldController : MonoBehaviour, IPointerDownHandler,
    IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
    {
        // , IPointerClickHandler,
        //     IBeginDragHandler, IDragHandler, IEndDragHandler    

        // [SerializeField] IConnectInputGroup<InputGroup> Ctl;
        [SerializeField] ConnectInputGroup CtlInputGrp;
        [SerializeField] InputGroup myGroup;
        [SerializeField] Image bg;
        [SerializeField] Text placeHolder;
        [SerializeField] Color colorInactive, colorActive;
        [SerializeField] int CharacterLimt;
        private bool isFocused;
        [SerializeField] bool clearPlaceholderOnInput;
        // [SerializeField] tableDataController forPlayerPrefs;
        [SerializeField] string placeholderText;

        private void Start()
        {
            isFocused = false;
        }

        public bool IsFocused()
        {
            return isFocused;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Debug.Log("Mouse Enter");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // Debug.Log("Mouse Up");
            // eventData.pointerCurrentRaycast.gameObject.GetComponent<Text>().text += "|";
            // me.text += "|";
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
            // eventData.pointerCurrentRaycast.gameObject.GetComponent<Text>().text += "_";
            //me.text += "/";
            SetFocus();
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Mouse Exit");
            isFocused = false;
            bg.color = colorInactive;
            // Ctl.DeclareNumericInputField(null);
        }

        public void SetFocus()
        {
            Debug.Log("set focus to " + myGroup.gameObject.name);
            isFocused = true;
            bg.color = colorActive;
            CtlInputGrp.DeclareNumericInputField(myGroup);
        }

        public void SetActive(bool activate)
        {
            bg.gameObject.SetActive(activate);
            placeHolder.gameObject.SetActive(activate);
            this.gameObject.SetActive(activate);
        }

        public bool SetText(string toSet)
        {
            Debug.Log("VSLText, SetText: " + toSet);
            Debug.Log("VSLTest, current content length: " + this.gameObject.GetComponent<Text>().text.Length);
            if (CharacterLimt > 0 && toSet.Length >= this.gameObject.GetComponent<Text>().text.Length && this.gameObject.GetComponent<Text>().text.Length >= CharacterLimt)
            {
                return false;
            }
            this.gameObject.GetComponent<Text>().text = toSet;
            if (null != this.gameObject.GetComponent<tableDataController>())
            {
                this.gameObject.GetComponent<tableDataController>().collectDataForField(toSet);
            }
            if (!string.IsNullOrEmpty(toSet) && clearPlaceholderOnInput && !string.IsNullOrEmpty(placeHolder.text))
            {
                placeHolder.text = string.Empty;
            }
            else if (string.IsNullOrEmpty(toSet))
            {
                placeHolder.text = placeholderText;
            }
            return true;
        }

        public void SetCtlInputGrp(ConnectInputGroup toSet)
        {
            //Debug.Log("setting ConnectInputGroup of " + this.gameObject.name + "to " + toSet.gameObject.name);
            CtlInputGrp = toSet;
        }

    }
}