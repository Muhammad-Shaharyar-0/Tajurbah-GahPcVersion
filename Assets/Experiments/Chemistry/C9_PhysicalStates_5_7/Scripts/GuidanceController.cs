using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C9_PhysicalStates_5_7
{
    public class GuidanceController : MonoBehaviour
    {
        public int off_time;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            StartCoroutine("StartGuidanceAnimation");
        }

        IEnumerator StartGuidanceAnimation()
        {
            yield return new WaitForSeconds(off_time);
            this.gameObject.SetActive(false);
        }
    }

    [System.Serializable]
    public class Guidance_transform
    {
        public Vector3 pos;
        public Vector3 rot;
    }
}