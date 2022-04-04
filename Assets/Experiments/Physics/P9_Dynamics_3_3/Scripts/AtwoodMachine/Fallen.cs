using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
	public class Fallen : MonoBehaviour
	{
		[SerializeField] int targetLayer;
		//[SerializeField] GameObject hotplate;

		private Vector3 spawnSpot;

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
			Debug.Log("on floor, fallen has layer " + other.gameObject.layer);
			if (other.gameObject.layer == targetLayer)
			{
				//if(0==MarsiveAttack.Ginstance.getSpawnCount()){
				MarsiveAttack.Ginstance.returnToBase(other.gameObject);
				//}
			}
		}
	}
}