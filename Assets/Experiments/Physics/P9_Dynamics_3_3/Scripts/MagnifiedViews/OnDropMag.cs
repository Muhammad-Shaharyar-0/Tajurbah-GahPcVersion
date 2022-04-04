using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class OnDropMag : OnDrop
    {
        [SerializeField] string zoomCameraName, massHangerIdentifier;
        [SerializeField] Transform lens;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void HandleDrop()
        {
            Debug.Log("handling drag, setting leader of " + zoomCameraName);
            GameObject zc = GameObject.Find(zoomCameraName);
            if (null != zc)
            {
                zc.SetActive(true);
                zc.GetComponent<moveZoomCameraMag>().SetLeader(lens);
            }

            Debug.Log("going to look for: " + massHangerIdentifier);
            GameObject MassHanger1 = GameObject.Find(massHangerIdentifier);
            if(null != MassHanger1){
                Debug.Log("found target for slow-mo mode: " + MassHanger1.name);
                // assign the projected mark as the target to watch for the super slo-mo mode of the magnifying glass
                this.GetComponent<DropProperties>().SuperSloMoTarget = MassHanger1.GetComponent<MarkMetreRod>().GetProjectedMark();
            }
            else{
                Debug.Log("could not find target to set for slow-mo mode");
                this.GetComponent<DropProperties>().SuperSloMoTarget = null;
            }
        }
    }
}
