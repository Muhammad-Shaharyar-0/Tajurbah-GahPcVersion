using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_3
{
    public class ManageObs : MonoBehaviour
    {
        [SerializeField] GameObject PanelForObs;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ActivatePanelForS(bool activate)
        {
            if (activate)
            {
                PanelForObs.GetComponent<PanelController>().onlyS = true;
            }
            PanelForObs.SetActive(activate);
        }
        public void ActivatePanelForAll(bool activate)
        {
            if (activate)
            {
                PanelForObs.GetComponent<PanelController>().onlyS = false;
            }
            PanelForObs.SetActive(activate);
        }


        public void SetS(float s)
        {
            MarsiveAttack.Ginstance.currentObs.s = s;
        }
        public void SetM1(float m1)
        {
            MarsiveAttack.Ginstance.currentObs.m1 = m1;
        }
        public void SetM2(float m2)
        {
            MarsiveAttack.Ginstance.currentObs.m2 = m2;
        }
        public void SetT(float t)
        {
            MarsiveAttack.Ginstance.currentObs.t = t;
        }
    }
}