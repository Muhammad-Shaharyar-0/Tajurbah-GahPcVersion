using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySimpleLiquid;

namespace C9_Solutions_6_7
{

    public class TapController : MonoBehaviour
    {
        public LiquidContainer liquidContainerObject;
        public GameObject tapOnOffObject;

        public bool isOn = false;
        // Start is called before the first frame update
        void Start()
        {
            liquidContainerObject.IsOpen = isOn;

            tapOnOffObject.transform.localRotation = Quaternion.Euler(-20, transform.localRotation.y, 20);
        }

        private void OnMouseDown()
        {
            isOn = !isOn;

            if (isOn)
            {
                liquidContainerObject.IsOpen = isOn;
                tapOnOffObject.transform.localRotation = Quaternion.Euler(20, transform.localRotation.y, 20);
                
            }
            else
            {
                liquidContainerObject.IsOpen = isOn;
                tapOnOffObject.transform.localRotation = Quaternion.Euler(-20, transform.localRotation.y, 20);
            }
        }
    }
}
