using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
	public class Layers : MonoBehaviour
	{

		// 0: initial/not yet landed on hotplate
		// 1: ready for use in practical
		// 2: in use
		public int State;

		// Use this for initialization
		void Start()
		{
			State = 0; // initial state
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void SetLayersOfSelfAndChildren(int newLayer)
		{
			this.gameObject.layer = newLayer;
			foreach (Transform child in transform)
			{
				child.gameObject.layer = this.gameObject.layer;
			}
		}
	}
}