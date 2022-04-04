using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    bool EnableHorizontalMovement = false;

    int Counter;

    public Vector3 pointB;
    public Vector3 pointA;

    float speed;
    float t;

    bool RodReachedItsFinalPos;
   
    void Start()
    {
        Counter = 2;
        speed = 0.5f;
        t = 0;
        RodReachedItsFinalPos = false;
    }

    private void FixedUpdate()
    {
        if (EnableHorizontalMovement)
        {
            Debug.Log("horizontal rod move ho rhi ha...");
            t += Time.deltaTime * speed;
            // Moves the object to target position
            transform.localPosition = Vector3.Lerp(pointA, pointB, t);

            if ( t >= 1 )
            {
                Debug.Log("me apni jgah pe puhnch gya hoo..");
                
                EnableHorizontalMovement = false;

                this.GetComponent<HorizontalRodHandler>().EnableHorizontalRodBtn();

                if (Counter == 1)      //rod reached down
                {
                    SetReachedItsDownFinalPos(true);
                }
                else { 
                    //reached up
                }
               
            }
        }

    }

 
    // What Linear interpolation actually looks like in terms of code
    private Vector3 CustomLerp(Vector3 a, Vector3 b, float t)
    {
        return a * (1 - t) + b * t;
    }

    void SwapPointAWithPointB()
    {
        var C = pointA;             //a=b, b=c, c=a;
        pointA = pointB;
        pointB = C;

        t = 0;

        if (Counter == 2)           //move down
            Counter = 1;
        else
            Counter = 2;            //move up

    }

    public void EnableMovement()
    {
        SwapPointAWithPointB();
        SetReachedItsDownFinalPos(false);   
        EnableHorizontalMovement = true;
    }


    public void SetReachedItsDownFinalPos(bool s)
    {
        RodReachedItsFinalPos = s;
    }

    public bool GetReachedItsFinalPos()
    {
        return RodReachedItsFinalPos;
    }
}
