using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class MarkMetreRod : MonoBehaviour
    {
        [SerializeField] string metreRodTag;
        [SerializeField] Transform markProjected, hangerBase;
        [SerializeField] float zOffSet, xOffSet, yOffSetStart;
        [SerializeField] Vector3 direction;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 startOfRay = hangerBase.position;
            startOfRay.y += yOffSetStart;
            RaycastHit[] hits = Physics.RaycastAll(startOfRay, direction, 5.0f);
            if (hits.Length > 0)
            {
                // Debug.Log("hitting " + hits.Length);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject.tag == metreRodTag)
                    {
                        // Debug.Log("collided with object " + hit.collider.gameObject.name + " at " + hit.point.ToString("0.###"));
                        Vector3 posMark = new Vector3(hit.collider.transform.position.x + xOffSet, hit.point.y, hit.collider.transform.position.z + zOffSet);
                        // Debug.Log("need to place marker at " + posMark.ToString("0.###"));
                        markProjected.SetPositionAndRotation(posMark, Quaternion.Euler(0.0f, 0.0f, -90.0f));
                    }
                }
                Debug.DrawRay(startOfRay, direction, Color.blue);
            }
        }

        public Transform GetProjectedMark()
        {
            return markProjected;
        }
    }
}
