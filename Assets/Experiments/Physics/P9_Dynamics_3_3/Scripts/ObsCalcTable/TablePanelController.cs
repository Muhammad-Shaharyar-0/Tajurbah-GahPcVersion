using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{

    public class TablePanelController : ConnectInputGroup
    {
       // [SerializeField] calc myCalculator;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /*     public void btnAssign_Click()
             {
                 Debug.Log("about to assign calculator value, " + myCalculator.GetOutputValue());
                 Debug.Log(", to " + base.target.gameObject.name);

                 base.target.setInputText(myCalculator.GetOutputValue());
             }*/

        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}
