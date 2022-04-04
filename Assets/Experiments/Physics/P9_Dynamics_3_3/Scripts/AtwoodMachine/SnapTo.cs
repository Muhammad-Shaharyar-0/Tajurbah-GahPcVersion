using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
	public class SnapTo : MonoBehaviour
	{

		[SerializeField] Vector3 snapToPosition;

		private Transform MassToSnapIntoPlace;
		[SerializeField] Transform Stack;

		public Transform GetMassToSnapIntoPlace()
		{
			return MassToSnapIntoPlace;
		}
		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}


		private void OnTriggerEnter(Collider other)
		{
			if (!MarsiveAttack.Ginstance.isMouseDragging)
			{
				return;
			}

			Transform toSnap;
			if (other.transform.parent)
			{
				toSnap = other.transform.parent;
			}
			else
			{
				toSnap = other.transform;
			}
			Debug.Log(toSnap.gameObject.name + ", tag: " + toSnap.gameObject.tag);
			if (toSnap.gameObject.tag == "10" ||
				toSnap.gameObject.tag == "50" ||
				toSnap.gameObject.tag == "100")
			{

				MassToSnapIntoPlace = toSnap;
				this.transform.parent.parent.GetComponent<MarsiveAttack>().InActiveUse = this.transform;
				/*
				this.transform.parent.parent.GetComponent<DragDropScript>().isMouseDragging=false;
				toSnap.transform.parent = this.transform;
				toSnap.transform.localPosition = snapToPosition;		
				*/
			}
		}

		/// <summary>
		/// OnTriggerExit is called when the Collider other has stopped touching the trigger.
		/// </summary>
		/// <param name="other">The other Collider involved in this collision.</param>
		void OnTriggerExit(Collider other)
		{
			if (!MarsiveAttack.Ginstance.isMouseDragging)
			{
				return;
			}
			MassToSnapIntoPlace = null;
			this.transform.parent.parent.GetComponent<MarsiveAttack>().InActiveUse = null;
		}

		public void SnapIntoPlace()
		{
			MassToSnapIntoPlace.transform.parent = Stack;
			MassToSnapIntoPlace.transform.localPosition = snapToPosition;
			MassToSnapIntoPlace.transform.rotation = Quaternion.Euler(Vector3.zero);
			MassToSnapIntoPlace.gameObject.GetComponent<Layers>().State = 2;
			Stack.GetComponent<StackInfo>().Top = MassToSnapIntoPlace.transform;
			MassToSnapIntoPlace = null;
			this.transform.parent.parent.GetComponent<MarsiveAttack>().InActiveUse = null;
		}
	}
}