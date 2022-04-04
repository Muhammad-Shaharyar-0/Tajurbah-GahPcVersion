using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class CleanUpApp : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            PlayerPrefs.DeleteKey("quiz_mode");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
