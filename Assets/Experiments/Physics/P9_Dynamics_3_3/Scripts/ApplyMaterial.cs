using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class ApplyMaterial : MonoBehaviour
    {
        [SerializeField] Material toApply;
        [SerializeField] MeshRenderer D1, D2, D3;
        // Start is called before the first frame update
        void Start()
        {
            D1.material = toApply;
            D2.material = toApply;
            D3.material = toApply;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}