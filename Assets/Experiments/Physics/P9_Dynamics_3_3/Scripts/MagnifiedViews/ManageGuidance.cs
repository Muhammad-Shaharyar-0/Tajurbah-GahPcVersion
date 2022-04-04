using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
    public class ManageGuidance : MonoBehaviour
    {
        [SerializeField] Material matTop, matBottom, matPlain;
        [SerializeField] MeshRenderer rendererLens;

        [SerializeField] float tolerance;

        [SerializeField] Color looking, found;

        // Start is called before the first frame update
        void Start()
        {

        }

        void FixedUpdate()
        {
            // Debug.Log("mark is at " + MarsiveAttack.Ginstance.GetProjectedMark1().position.y + ", lens is at: " + rendererLens.transform.position.y);
            // Debug.Log("diff: " + (MarsiveAttack.Ginstance.GetProjectedMark1().position.y - rendererLens.transform.position.y) + ", tolerance: " + tolerance);
            if ((MarsiveAttack.Ginstance.GetProjectedMark1().position.y - rendererLens.transform.position.y) < (-1 * tolerance))
            {
                // Debug.Log("lens is above");
                rendererLens.material = matBottom;
                MarsiveAttack.Ginstance.GetCameraWatchingMetreRod().backgroundColor = looking;
            }
            else if ((MarsiveAttack.Ginstance.GetProjectedMark1().position.y - rendererLens.transform.position.y) > tolerance)
            {
                // Debug.Log("lens is below");
                rendererLens.material = matTop;
                MarsiveAttack.Ginstance.GetCameraWatchingMetreRod().backgroundColor = looking;
            }
            else
            {
                // Debug.Log("overhead now");
                rendererLens.material = matPlain;
                MarsiveAttack.Ginstance.GetCameraWatchingMetreRod().backgroundColor = found;
            }
        }
    }
}