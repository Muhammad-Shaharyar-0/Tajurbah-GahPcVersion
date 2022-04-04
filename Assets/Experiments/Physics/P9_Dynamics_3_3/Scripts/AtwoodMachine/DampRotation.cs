using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
	public class DampRotation : MonoBehaviour
	{

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void FixedUpdate()
		{
			if (this.gameObject.GetComponent<Rigidbody>().angularVelocity.magnitude > 0.01f)
			{
				this.gameObject.GetComponent<Rigidbody>().angularVelocity.Set(
					this.gameObject.GetComponent<Rigidbody>().angularVelocity.x / 2,
					this.gameObject.GetComponent<Rigidbody>().angularVelocity.y / 2,
					this.gameObject.GetComponent<Rigidbody>().angularVelocity.z / 2);
			}
			else if (this.gameObject.GetComponent<Rigidbody>().angularVelocity.magnitude > 0f)
			{
				this.gameObject.GetComponent<Rigidbody>().angularVelocity.Set(
					0f, 0f, 0f);
			}
		}
	}
}