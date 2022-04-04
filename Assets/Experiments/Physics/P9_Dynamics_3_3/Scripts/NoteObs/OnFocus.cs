using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_3
{
    public class OnFocus : MonoBehaviour
    {
        // Start is called before the first frame update
        // [SerializeField] InputField txt;
        [SerializeField] InputGroup grp;
        [SerializeField] TMPro.TextMeshProUGUI lblCameraM1;
        [SerializeField] PanelController pnlCtl;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (grp.isFocused())
            {
                Debug.Log(grp.gameObject.name + "is in focus");
                pnlCtl.DeclareNumericInputField(grp);

                if (grp.gameObject.name == "M1")
                {
                    // Change the Color of the label of the zoomed view
                    // tracking the position of m1
                    lblCameraM1.color = Color.yellow;
                }
                else
                {
                    lblCameraM1.color = Color.white;
                }
            }
            else
            {
                Debug.Log(grp.gameObject.name + "is not in focus");
                pnlCtl.DeclareNumericInputField(null);
            }
        }
    }
}