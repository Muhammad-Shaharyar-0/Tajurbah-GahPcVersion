using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
	public class StackInfo : MonoBehaviour
	{

		public Transform Top;

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public float GetTotalMass()
		{

			float mass = 0f;
			foreach (Transform c in this.transform)
			{
				mass += c.gameObject.GetComponent<Rigidbody>().mass;
			}
			return mass;
		}
	}
}