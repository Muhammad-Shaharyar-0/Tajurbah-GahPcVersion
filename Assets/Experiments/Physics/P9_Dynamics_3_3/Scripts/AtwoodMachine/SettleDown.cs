using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
	public class SettleDown : MonoBehaviour
	{

		private bool centred;
		[SerializeField] Vector3 Centre, Tolerances;
		// Use this for initialization
		void Start()
		{
			centred = false;
		}

		// Update is called once per frame
		void FixedUpdate()
		{
			if (this.gameObject.GetComponent<Layers>().State < 2)
			{
				return;
			}
			this.CheckIfOffCentre();
			if (!centred)
			{
				// this.CentreAlign();
			}
		}

		/// <summary>
		/// OnCollisionEnter is called when this collider/rigidbody has begun
		/// touching another rigidbody/collider.
		/// </summary>
		/// <param name="other">The Collision data associated with this collision.</param>
		void OnCollisionEnter(UnityEngine.Collision other)
		{
			// Debug.Log("I, " + this.gameObject.name + ", have collided with " + other.gameObject.name);
			// if this is a just-spawned mass colliding with a mass
			// on the hotplate
			if (this.gameObject.GetComponent<Layers>().State == 0)
			{
				if ((other.gameObject.tag == "10" ||
					other.gameObject.tag == "50" ||
					other.gameObject.tag == "100") &&
					other.gameObject.GetComponent<Layers>().State == 1 &&
					other.transform.parent != null &&
					other.transform.parent.gameObject.name == "Stack")
				{
					other.transform.parent.parent.gameObject.GetComponent<StackIt>().addToStack(this.transform);
				}
				return;
			}
			// only if mass has already been snapped into place
			if (this.transform.parent != null && this.transform.parent.gameObject.name == "Stack" &&
				this.centred == false)
			{
				//Debug.Log("... and I'm already in the stack");
				//Debug.Log("I collided with " + other.gameObject.name);
				//this.CentreAlign();
			}
		}

		void CheckIfOffCentre()
		{
			// only check if you're already on the stack
			if (this.transform.parent != null && this.transform.parent.gameObject.name == "Stack")
			{
				if (Mathf.Abs(this.transform.localPosition.x - Centre.x) > Tolerances.x ||
					Mathf.Abs(this.transform.localPosition.z - Centre.z) > Tolerances.z)
				{
					//Debug.Log("off-centre");
					centred = false;
				}
			}
		}
		void CentreAlign()
		{
			Vector3 tempPos = this.transform.localPosition;
			tempPos.x = Centre.x;
			tempPos.z = Centre.z;
			this.transform.localPosition = tempPos;
			centred = true;
			Debug.Log("centred at (locally): " + this.transform.localPosition);
			Debug.Log("centred at (globally): " + this.transform.position);
		}

	}
}