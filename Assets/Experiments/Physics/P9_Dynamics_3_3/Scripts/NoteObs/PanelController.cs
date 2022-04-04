using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_3
{
    public class PanelController : ConnectInputGroup
    {

        public bool onlyS = false;
        [SerializeField] Vector2 posBtnOK;
        [SerializeField] InputGroup height, m1, m2, t;
        private ManageObs ObsManager;
        private Color bad = Color.red;

        [SerializeField] Color good;
        [SerializeField] GameObject btnOKHeight, btnOKAll, btnCancel;
        [SerializeField] NumericKeyPad numPad;

        // Start is called before the first frame update
        void Start()
        {
            // whenever this panel is loaded, it should ask the main controller
            // to hide all the buttons on the main UI
            MarsiveAttack.Ginstance.HideAllButtonsOnMainUI();
        }
        void Awake()
        {
            // good = new Color(0.0f, 148.0f, 255.0f, 255.0f);
            ObsManager = MarsiveAttack.Ginstance.GetComponent<ManageObs>();
        }

        void OnEnable()
        {
            //Debug.Log("currentObs: " + MarsiveAttack.Ginstance.currentObs.inspect());
            if (onlyS)
            {
                height.Activate(true);
                m1.Activate(false);
                m2.Activate(false);
                t.Activate(false);
                btnOKAll.gameObject.SetActive(false);
                btnOKHeight.gameObject.SetActive(true);
                height.SetFocus();
                //height.ActivateInputField();
            }
            else
            {
                height.Activate(true);
                m1.Activate(true);
                m2.Activate(true);
                t.Activate(true);
                if (MarsiveAttack.Ginstance.currentObs.m1 != -1)
                {
                    m1.setInputText((m1.GetBaseline() == -1) ?
                                    MarsiveAttack.Ginstance.currentObs.m1.ToString() :
                                    (MarsiveAttack.Ginstance.currentObs.m1 - m1.GetBaseline()).ToString());
                    m1.UpdateFinal();
                }
                if (MarsiveAttack.Ginstance.currentObs.m2 != -1)
                {
                    m2.setInputText((m2.GetBaseline() == -1) ?
                                    MarsiveAttack.Ginstance.currentObs.m2.ToString() :
                                    (MarsiveAttack.Ginstance.currentObs.m2 - m2.GetBaseline()).ToString());
                    m2.UpdateFinal();
                }
                if (MarsiveAttack.Ginstance.currentObs.t != -1)
                {
                    t.setInputText(MarsiveAttack.Ginstance.currentObs.t.ToString());
                }
                btnOKAll.gameObject.SetActive(true);
                btnOKHeight.gameObject.SetActive(false);
                m1.SetFocus();
                //m1.ActivateInputField();
            }
            if (MarsiveAttack.Ginstance.currentObs.s != -1)
            {
                height.setInputText(MarsiveAttack.Ginstance.currentObs.s.ToString());
            }

            Time.timeScale = 0;
        }

        public void SetSFromUI()
        {
            // float temp;
            // if(float.TryParse(height.txt.text, out temp)){
            //     ObsManager.SetS(temp);
            //     height.txt.GetComponentInChildren<Text>().color = good;
            //     height.lblErr.gameObject.SetActive(false);
            //     ObsManager.ActivatePanelForS(false);
            // }
            // else{
            //     height.txt.GetComponentInChildren<Text>().color = bad;
            //     height.lblErr.gameObject.SetActive(true);
            // }
            float tempObVal;
            if (SetObFromUI(height, out tempObVal))
            {
                if (tempObVal != -1)
                {
                    ObsManager.SetS(tempObVal);
                }
                ObsManager.ActivatePanelForS(false);
            }
            Debug.Log("currentObs: " + MarsiveAttack.Ginstance.currentObs.inspect());
        }

        public bool SetObFromUI(InputGroup fromUI, out float obVal)
        {

            if ((fromUI.lblFinal != null && (obVal = fromUI.GetFinalVal()) != -1) ||
                (fromUI.lblFinal == null && float.TryParse(fromUI.getInputText(), out obVal)))
            {
                fromUI.SetInputFieldColor(good);
                fromUI.lblErr.gameObject.SetActive(false);
                return true;
            }
            else
            {
                if (fromUI.getInputText() == "" && fromUI.allowNull)
                {
                    obVal = -1;
                    return true;
                }
                else
                {
                    obVal = -1;
                    fromUI.SetInputFieldColor(bad);
                    fromUI.lblErr.gameObject.SetActive(true);
                    return false;
                }
            }
        }

        public void SetAllObsFromUI()
        {
            float tempObVal;
            bool allGood = true;
            if (SetObFromUI(height, out tempObVal))
            {
                if (tempObVal != -1)
                {
                    ObsManager.SetS(tempObVal);
                }
            }
            else
            {
                allGood = false;
            }
            m1.UpdateFinal();
            if (SetObFromUI(m1, out tempObVal))
            {
                if (tempObVal != -1)
                {
                    ObsManager.SetM1(tempObVal);
                }
            }
            else
            {
                allGood = false;
            }
            m2.UpdateFinal();
            if (SetObFromUI(m2, out tempObVal))
            {
                if (tempObVal != -1)
                {
                    ObsManager.SetM2(tempObVal);
                }
            }
            else
            {
                allGood = false;
            }
            if (SetObFromUI(t, out tempObVal))
            {
                if (tempObVal != -1)
                {
                    ObsManager.SetT(tempObVal);
                }
            }
            else
            {
                allGood = false;
            }
            if (allGood)
            {
                ObsManager.ActivatePanelForAll(false);
            }
            Debug.Log("currentObs: " + MarsiveAttack.Ginstance.currentObs.inspect());
            MarsiveAttack.Ginstance.SaveObs(m1.GetBaseline(), m2.GetBaseline());
            MarsiveAttack.Ginstance.PrintPlayerPrefs();
            MarsiveAttack.Ginstance.SetStateToSaved();
        }

        // Update is called once per frame
        public void CancelObs()
        {
            // MarsiveAttack.Ginstance.SetStateToGrounded();
        }

        // called by the OnFocus script associated with fields
        // requiring numeric input
        public override void DeclareNumericInputField(InputGroup currentInput = null)
        {
            if (null != currentInput)
            {
                //numPad.gameObject.SetActive(true);
                numPad.SetTarget(currentInput);
            }
            else
            {
                //numPad.gameObject.SetActive(false);

            }
        }

        void OnDisable()
        {
            Time.timeScale = 1;
            // and when it gets unloaded, it should let the main controller
            // know that it should restore all the buttons on the main UI
            // to their previous state
            MarsiveAttack.Ginstance.ResetAllHiddenButtonsOnMainUI();

        }
    }
}