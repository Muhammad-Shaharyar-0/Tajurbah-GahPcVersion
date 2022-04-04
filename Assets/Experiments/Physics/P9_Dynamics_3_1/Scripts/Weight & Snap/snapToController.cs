using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_1
{

    public class snapToController : MonoBehaviour
    {
        [Header("--Is Cylindrical Weight--")]
        public bool isCylindricalWeight = false;

        [Header("--Is Chips Weight--")]
        public bool isChipsWeight = false;
        public Vector3 snapPosition;

        private void OnTriggerEnter(Collider other)
        {
            GameObject gameObject = other.gameObject;

            while (gameObject.transform.parent != null)
            {
                if (gameObject.GetComponent<Tajurbah_Gah.ParentCheckController>())
                {
                    break;
                }
                else
                {
                    gameObject = gameObject.transform.parent.gameObject;
                }
            }
            if (gameObject.gameObject.tag == "ChipsWeight" && isChipsWeight)
            {
                P9_Dynamics_3_1.dragObjectsController.Instance.DropAppratus();
                gameObject.transform.parent = this.gameObject.transform;
                gameObject.transform.localPosition = snapPosition;
                gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
                gameObject.tag = "Untagged";
            }
            if (gameObject.gameObject.tag == "CylindricalWeight" && isCylindricalWeight)
            {
                dragObjectsController.Instance.DropAppratus();
                gameObject.transform.parent = this.gameObject.transform;
                gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
                gameObject.tag = "Untagged";
            }
        }

    }
}