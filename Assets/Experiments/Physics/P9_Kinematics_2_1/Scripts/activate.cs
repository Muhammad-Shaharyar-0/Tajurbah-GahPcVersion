using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class activate : MonoBehaviour
    {
        [SerializeField] Color activatedColorText, deactivatedColorText;
        [SerializeField] UnityEngine.UI.Button myButton;
        [SerializeField] UnityEngine.UI.Text me;
        // Start is called before the first frame update
        void Start()
        {
            deactivatedColorText = myButton.colors.disabledColor;
        }

        // Update is called once per frame
        void Update()
        {
            if (myButton.IsInteractable())
            {
                me.color = activatedColorText;
            }
            else
            {
                me.color = deactivatedColorText;
            }
        }
    }
}