using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
	public class ishtaaap : MonoBehaviour
	{

		[SerializeField] float upperLimit, switchingOnMargin, maxInstantaneousChange;
		private bool switchedGravityOff, First;
		private float sinceGSwitchedOn;
		[SerializeField] Transform MassHanger1, MassHanger2;
		private StackInfo m1Stack, m2Stack;
		private Vector3 lastM1Pos, lastM2Pos;


		// Use this for initialization
		void Start()
		{
			switchedGravityOff = false; MarsiveAttack.Ginstance.state = MarsiveState.swinging;
			sinceGSwitchedOn = 0f;
			First = true;
			m1Stack = MassHanger1.GetComponentInChildren<StackInfo>();
			m2Stack = MassHanger2.GetComponentInChildren<StackInfo>();
			lastM1Pos = Vector3.zero;
			lastM2Pos = Vector3.zero;
			Debug.Log("start: " + Time.time);
		}

		public bool gravitySwitchedOff()
		{
			return switchedGravityOff;
		}

		// Update is called once per frame
		void FixedUpdate()
		{
			// Debug.Log("now: " + Time.time);
			// Debug.Log("m1.y: " + MassHanger1.position.y + "m2.y: " + MassHanger2.position.y);
			// Debug.Log("is gravity switched off? " + switchedGravityOff);
			if (!switchedGravityOff && (MassHanger1.position.y > upperLimit) &&
				((First && !isSwinging()) || sinceGSwitchedOn > switchingOnMargin))
			{

				Debug.Log("fixing for the first time");
				// freeze the mass hangers (switch off gravity & set as kinematic)
				MassHanger1.GetComponent<Rigidbody>().isKinematic =
					MassHanger2.GetComponent<Rigidbody>().isKinematic = true;
				MassHanger1.GetComponent<Rigidbody>().useGravity =
					MassHanger2.GetComponent<Rigidbody>().useGravity = false;

				// set the flags that others need to collaborate
				switchedGravityOff = true; MarsiveAttack.Ginstance.state = MarsiveState.stabilised;

				sinceGSwitchedOn = 0f;
				First = false;
				// fix orientation to vertical
				// Vector3 HangerRotation = MassHanger1.rotation.eulerAngles;
				// HangerRotation.z = 0f;
				// MassHanger1.rotation = Quaternion.Euler(HangerRotation);

				// HangerRotation = MassHanger2.rotation.eulerAngles;
				// HangerRotation.z = 0f;
				// MassHanger2.rotation = Quaternion.Euler(HangerRotation);
				MassHanger2.rotation = MassHanger1.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
			}
			else if (switchedGravityOff && needsToReverse() &&
					MarsiveAttack.Ginstance.IsSWRunning())
			{
				// un-freeze the mass hangers
				MassHanger1.GetComponent<Rigidbody>().useGravity =
					MassHanger2.GetComponent<Rigidbody>().useGravity = true;
				MassHanger1.GetComponent<Rigidbody>().isKinematic =
					MassHanger2.GetComponent<Rigidbody>().isKinematic = false;

				// re-set the gravity flag and update the state
				switchedGravityOff = false; MarsiveAttack.Ginstance.state = MarsiveState.dropping;

				sinceGSwitchedOn += Time.fixedDeltaTime;
			}
			else if (!switchedGravityOff)
			{
				sinceGSwitchedOn += Time.fixedDeltaTime;
			}
			lastM1Pos = MassHanger1.position;
			lastM2Pos = MassHanger2.position;
			// Debug.Log("m1.y:" + lastM1Pos.y + ", m2.y: " + lastM2Pos.y + ", m1.x: " + lastM1Pos.x + ", m2.x: " + lastM2Pos.x);
		}

		bool needsToReverse()
		{
			// Debug.Log("m1.mass: " + MassHanger1.GetComponentInChildren<StackInfo>().GetTotalMass() +
			// 			", m2.mass: " + MassHanger2.GetComponentInChildren<StackInfo>().GetTotalMass());
			if (MassHanger1.position.y > MassHanger2.position.y)
			{
				if ((MassHanger1.GetComponent<Rigidbody>().mass + m1Stack.GetTotalMass()) > (MassHanger2.GetComponent<Rigidbody>().mass + m2Stack.GetTotalMass()))
				{
					return true;
				}
			}
			else if (MassHanger1.position.y < MassHanger2.position.y)
			{
				if ((MassHanger1.GetComponent<Rigidbody>().mass + m1Stack.GetTotalMass()) < (MassHanger2.GetComponent<Rigidbody>().mass + m2Stack.GetTotalMass()))
				{
					return true;
				}
			}
			return false;
		}

		bool isSwinging()
		{
			// if not the first time, this logic does not apply
			if (!First)
			{
				return false;
			}
			// deal with the hanger that is closer to the pulley
			Vector3 toCheck;
			Vector3 lastPosToCheck;
			if (MassHanger1.position.y > MassHanger2.position.y)
			{
				toCheck = MassHanger1.position;
				lastPosToCheck = lastM1Pos;
			}
			else
			{
				toCheck = MassHanger2.position;
				lastPosToCheck = lastM2Pos;
			}
			// Debug.Log("hanger.y now: " + toCheck.y + ", last hanger.y: " + lastPosToCheck.y + ", diff: " + Mathf.Abs(toCheck.y - lastPosToCheck.y));
			// Debug.Log("hanger.x now: " + toCheck.x + ", last hanger.x: " + lastPosToCheck.x + ", diff: " + Mathf.Abs(toCheck.x - lastPosToCheck.x));
			float sway = Mathf.Pow(Mathf.Pow(toCheck.z - lastPosToCheck.z, 2.0f) + Mathf.Pow(toCheck.y - lastPosToCheck.y, 2.0f) + Mathf.Pow(toCheck.x - lastPosToCheck.x, 2), 0.5f);
			if (!MarsiveAttack.Ginstance.inDesignMode)
			{
				// Debug.Log("sway: " + sway);
			}
			return (sway >= maxInstantaneousChange);
		}
	}
}

