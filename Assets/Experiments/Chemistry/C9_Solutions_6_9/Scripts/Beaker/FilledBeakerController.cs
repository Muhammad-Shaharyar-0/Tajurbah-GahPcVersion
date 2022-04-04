using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace C9_Solutions_6_7
{
    public class FilledBeakerController : MonoBehaviour
    {

        Button pourLiquidButton;

        private void Start()
        {
            pourLiquidButton = GameObject.FindGameObjectWithTag("PourLiquid").GetComponent<Button>();
            pourLiquidButton.onClick.AddListener(PourLiquidFromBeaker);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("BeakerPH"))
            {
                pourLiquidButton.interactable = true;
            }
        }

        void PourLiquidFromBeaker()
        {
            StartCoroutine("PourLiquidFromBeakerRoutine");
        }

        IEnumerator PourLiquidFromBeakerRoutine()
        {
            while(true)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0, 0, 70), 1);

                if(transform.localRotation==Quaternion.Euler(0,0,70))
                {
                    yield return new WaitForSeconds(2f);
                    StartCoroutine("ResetBeakerRoutine");
                    break;
                }

                yield return null;
            }
        }

        IEnumerator ResetBeakerRoutine()
        {
            while (true)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0, 0, 0), 1);

                if (transform.localRotation == Quaternion.Euler(0, 0, 0))
                {
                    LabManager.LabManager_Instance.nextButton.interactable = true;

                    break;
                }

                yield return null;
            }
        }
    }
}