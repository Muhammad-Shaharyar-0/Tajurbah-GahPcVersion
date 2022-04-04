using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
	public class StackIt : MonoBehaviour
	{

		public Transform Stack;
		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		/// <summary>
		/// OnCollisionEnter is called when this collider/rigidbody has begun
		/// touching another rigidbody/collider.
		/// </summary>
		/// <param name="other">The Collision data associated with this collision.</param>
		void OnCollisionEnter(UnityEngine.Collision other)
		{
			Transform toSnap;
			if (other.transform.parent && other.transform.parent.gameObject.name != "Stack")
			{
				toSnap = other.transform.parent;
			}
			else
			{
				toSnap = other.transform;
			}
			Debug.Log(toSnap.gameObject.name);

			if (toSnap.gameObject.GetComponent<Layers>().State > 0)
			{
				return;
			}
			if (toSnap.gameObject.tag == "10" ||
				toSnap.gameObject.tag == "50" ||
				toSnap.gameObject.tag == "100")
			{
				this.addToStack(toSnap);
				// toSnap.transform.parent = Stack;
				// toSnap.gameObject.GetComponent<Layers>().State = 1;
			}
		}

		public void addToStack(Transform toAdd)
		{
			toAdd.parent = Stack;
			toAdd.gameObject.GetComponent<Layers>().State = 1;
		}
	}
}
