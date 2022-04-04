using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class PanelLoadingController : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (MarsiveAttack.Ginstance.inDesignMode)
            {
                this.gameObject.SetActive(false);
                MarsiveAttack.Ginstance.ResetAllHiddenButtonsOnMainUI();
                return;
            }
            if (MarsiveAttack.Ginstance.state == MarsiveState.stabilised)
            {
                this.gameObject.SetActive(false);
                MarsiveAttack.Ginstance.ResetAllHiddenButtonsOnMainUI();
            }
        }
    }
}