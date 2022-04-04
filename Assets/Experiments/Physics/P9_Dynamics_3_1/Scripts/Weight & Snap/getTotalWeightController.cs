using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tajurbah_Gah;

namespace P9_Dynamics_3_1
{
    public class getTotalWeightController : MonoBehaviour
    {
        [Header("Total Weight Mesh Text")]
        public bool isTotalWeightMeshText = false;
        public string keyName1;
        public TextMesh totalWeightTextMesh;

        [Header("--Total Weight Placed System--")]
        public bool isTotalWeightCanvasText = false;
        public Canvas canvasObject;
        Text totalWeightText;
        float totalWeight = 0;

        string typeOfExperiment;

        private void Start()
        {
            typeOfExperiment = managerController.Instance.ReturnTypeOfExperiment();

            if (isTotalWeightMeshText)
            {
                totalWeightTextMesh.text = ((float.Parse(PlayerPrefs.GetString(typeOfExperiment + keyName1)))).ToString("f1") + " kg";
            }

            if (isTotalWeightCanvasText)
            {
                canvasObject.gameObject.SetActive(false);
                totalWeightText = canvasObject.GetComponentInChildren<Text>();
                totalWeightText.text = "0 kg";
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isTotalWeightCanvasText)
            {
                canvasObject.gameObject.SetActive(true);
                totalWeight = 0;

                if (other.transform.parent.GetComponent<ParentCheckController>())
                {
                    int children = transform.childCount;

                    if (children > 0)
                    {
                        for (int i = 0; i < children; ++i)
                        {
                            if (transform.GetChild(i).GetComponent<ParentCheckController>())
                            {
                                totalWeight += transform.GetChild(i).GetComponent<Rigidbody>().mass;
                            }
                        }
                        totalWeightText.text = ((totalWeight)).ToString("f1") + " kg";
                    }
                }
            }
        }
        //private void OnTriggerExit(Collider other)
        //{
        //    if (isTotalWeightCanvasText)
        //    {
        //        if (other.transform.parent.GetComponent<parentCheckController>())
        //        {
        //            int children = transform.childCount;

        //            if (children > 0)
        //            {
        //                for (int i = 0; i < children; ++i)
        //                {
        //                    if (transform.GetChild(i).GetComponent<parentCheckController>())
        //                    {
        //                        totalWeight -= transform.GetChild(i).GetComponent<Rigidbody>().mass;
        //                    }
        //                }
        //                totalWeightText.text = ((totalWeight)).ToString("f1") + " kg";
        //            }

        //            if(totalWeight<=0)
        //            {
        //                canvasObject.gameObject.SetActive(false);
        //            }
        //        }

                
        //    }
        //}

        public float GetTotalWeight()
        {
            return totalWeight;
        }
    }
}
