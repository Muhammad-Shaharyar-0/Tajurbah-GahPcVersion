using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_1
{
    public class boxController : MonoBehaviour
    {

        int flag = 0;

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (other.gameObject.CompareTag("Stopper") && flag == 0)
            {
                SoundsManager.Instance.PlaySnapClickSound();
                flag++;
                phase2Controller.Instance.CheckPhase2Completion();

            }
        }
    }
}