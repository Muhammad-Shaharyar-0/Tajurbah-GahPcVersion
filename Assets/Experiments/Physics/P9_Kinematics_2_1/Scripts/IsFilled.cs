using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Kinematics_2_1
{
public class IsFilled : MonoBehaviour
{
    [SerializeField] Text T, TSquared, TwoS, A, G;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isFilled(){
        if(string.IsNullOrEmpty(T.text) || string.IsNullOrEmpty(TSquared.text) || string.IsNullOrEmpty(TwoS.text) || string.IsNullOrEmpty(A.text) || string.IsNullOrEmpty(G.text)){
            return false;
        }
        else{
            return true;
        }
    }
}
}