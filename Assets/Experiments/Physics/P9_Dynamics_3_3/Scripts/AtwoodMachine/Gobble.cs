using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
	public class Gobble : MonoBehaviour
	{

		[SerializeField] string floorTag;
		//[SerializeField] GameObject atBase;
		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		void OnCollisionEnter(UnityEngine.Collision other)
		{
			if (other.gameObject.tag == floorTag)
			{
				if (this.gameObject.name == "D1" || this.gameObject.name == "D2" || this.gameObject.name == "D3")
				{
					Debug.Log("part touched floor at " + this.transform.position.y);
					Destroy(this.transform.parent.gameObject);
				}
				else
				{
					Debug.Log("main object touched floor at " + this.transform.position.y);
					Destroy(this.gameObject);
				}
				for (int ci = 0; ci < other.contactCount; ci++)
				{
					Debug.Log(other.contacts[ci].point.y.ToString());
				}
			}
		}
	}
}
