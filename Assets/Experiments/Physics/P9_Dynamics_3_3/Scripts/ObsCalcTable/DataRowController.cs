using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class DataRowController : MonoBehaviour
    {
        public InputFieldController VSLText_T, VSLText_A, VSLText_G;
        [SerializeField] ConnectInputGroup connector;
        private bool isConnected;
        // Start is called before the first frame update
        void Start()
        {
            isConnected = false;
            StartCoroutine(SetCtlInputGrpLater());
        }

        // Update is called once per frame
        void Update()
        {
            if (isConnected)
            {
                StopCoroutine(SetCtlInputGrpLater());
            }
        }

        public void SetConnector(ConnectInputGroup toSet)
        {
            connector = toSet;
        }

        private bool IsConnectorNull()
        {
            return (connector == null);
        }

        IEnumerator SetCtlInputGrpLater()
        {
            yield return new WaitWhile(IsConnectorNull);

            VSLText_T.SetCtlInputGrp(connector);
            VSLText_A.SetCtlInputGrp(connector);
            VSLText_G.SetCtlInputGrp(connector);
            Debug.Log("have set connectors");
            isConnected = true;
        }

    }
}
