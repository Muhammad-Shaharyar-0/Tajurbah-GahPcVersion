using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnelController : MonoBehaviour
{
    public MeshCollider topFunnel;
    public MeshCollider bottomFunnel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FunnelPH"))
        {
            topFunnel.convex = false;
            bottomFunnel.convex = false;
        }
    }
}
