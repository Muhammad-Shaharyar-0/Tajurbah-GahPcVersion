using System.Collections;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class ConnectInputGroup : MonoBehaviour
    {
        protected InputGroup target;

        // Parameters:
        //   currentInput:
        //     The control that needs to receive user input
        public virtual void DeclareNumericInputField(InputGroup toSet)
        {
            Debug.Log("setting input group of " + toSet.gameObject.name + " as target for " + this.gameObject.name);
            target = toSet;
        }
    }
}
