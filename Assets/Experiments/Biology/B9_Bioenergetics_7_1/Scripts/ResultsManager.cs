using System.Collections;
using System.Collections.Generic;
using Tajurbah_Gah;
using UnityEngine;

namespace B9_Bioenergetics_7_1
{
    public class ResultsManager : MonoBehaviour
    {
        public GameObject matchStickOnButton;

        private void Start()
        {
            matchStickOnButton.SetActive(false);

            Tajurbah_Gah.PlaceholderManager.Instance.callBack = () => { matchStickOnButton.SetActive(true);};
        }
    }
}