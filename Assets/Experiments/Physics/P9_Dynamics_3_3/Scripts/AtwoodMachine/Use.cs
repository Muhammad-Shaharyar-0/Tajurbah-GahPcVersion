using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
	public class Use : MonoBehaviour
	{

		[SerializeField] int targetLayer;
		private List<GameObject> stack;
		private float top;

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
			Debug.Log("collided");
			if (other.gameObject.layer == targetLayer)
			{
				StartCoroutine(waitUntilStable(other.gameObject.GetComponent<Rigidbody>()));
				//this.GetComponentInParent<MarsiveAttack>().remove();
			}
		}

		IEnumerator waitUntilStable(Rigidbody moving)
		{
			float timePassed = 1f;
			while (timePassed > 0f)
			{
				if (moving.velocity.magnitude < 0.01 && moving.angularVelocity.magnitude < 0.01)
				{
					break;
				}
				timePassed -= Time.deltaTime;
				yield return null;
			}
			if (timePassed > 0)
			{
				moving.gameObject.transform.parent = this.transform;
				Destroy(moving);
			}

		}

		// private void addToStack(GameObject mass){
		// 	stack.Add(mass);
		// 	this.top = mass.transform.position.y; // needs refinement

		// }

		// stack.Sort(delegate(GameObject m1, GameObject m2) { 
		// 	return m1.GetComponent<Rigidbody>().mass.CompareTo( m2.GetComponent<Rigidbody>().mass ); 
		// });
	}
}