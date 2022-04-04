using System.Collections;
using System.Collections.Generic;
using Tajurbah_Gah;
using UnityEngine;

namespace B9_Bioenergetics_7_1
{
    public class OxygenTestTubeController : MonoBehaviour
    {
        [SerializeField]
        float distanceToDetectMatchstick = 0;

        int flag = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.up * distanceToDetectMatchstick);
            bool stopperFound = false;

            foreach(RaycastHit rayCastHit in hits)
            {
                if(rayCastHit.collider.CompareTag("StopperPH"))
                {
                    stopperFound = true;
                }
                
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.up* distanceToDetectMatchstick, out hit) && stopperFound==false)
            {
                if (hit.collider.CompareTag("MatchstickPH") && flag==0)
                {
                    flag++;

                    if(hit.collider.GetComponentInChildren<FlameController>().IsFlameBurning())
                    {
                        StartCoroutine(FlashFlameRoutine(hit.collider.GetComponentInChildren<FlameController>()));
                    }
                    else
                    {

                    }
                }
            }
            Debug.DrawRay(transform.position, Vector3.up* distanceToDetectMatchstick, Color.green);
        }
    
        IEnumerator FlashFlameRoutine(FlameController Object)
        {
            yield return new WaitForSeconds(0.25f);

            if(PlayerPrefs.GetFloat("OxygenTimer")>=12)
            {
                Object.FlashFlame(3);
            }
            else if (PlayerPrefs.GetFloat("OxygenTimer") >=7)
            {
                Object.FlashFlame(2);
            }
            else if (PlayerPrefs.GetFloat("OxygenTimer") >= 1)
            {
                Object.FlashFlame(1);
            }
        }
    }
}
