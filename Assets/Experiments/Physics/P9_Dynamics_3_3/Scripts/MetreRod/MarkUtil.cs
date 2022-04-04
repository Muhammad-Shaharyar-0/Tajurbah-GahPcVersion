using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
	public class MarkUtil : MonoBehaviour
	{

		[SerializeField] GameObject theMark;
		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void moveRight()
		{
			theMark.transform.Translate(0f, (-2 * theMark.transform.localPosition.y), 0f);
		}
	}
}