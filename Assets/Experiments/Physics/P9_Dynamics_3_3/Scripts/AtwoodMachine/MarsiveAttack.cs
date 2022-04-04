using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace P9_Dynamics_3_3
{

	public enum MarsiveState
	{
		swinging,
		stabilised,
		dropping,
		grounded,
		crashed,
		saved
	}

	public class MarsiveAttack : MonoBehaviour
	{
		static MarsiveAttack ginstance;
		public static MarsiveAttack Ginstance
		{
			get
			{
				if (ginstance == null)
				{
					ginstance = FindObjectOfType<MarsiveAttack>();
				}
				return ginstance;
			}
		}

		[SerializeField] GameObject mass10g, mass50g, mass100g, PanelLoading, MetreRod, Table, pnlTable;
		[SerializeField] Button btnSave, btnObsCalc, btnResetPractical, btnQuit;

		private bool btnSaveActivationState, btnObsCalcActivationState, btnResetPracticalActivationState, btnQuitActivationState, btnsAllHidden;

		public GameObject hotplate, used, rejected, StopWatch;
		public Transform InActiveUse, Floor;
		public MarsiveState state;
		[SerializeField] MarkMetreRod markerM1;
		[SerializeField] float suspense;
		private int Spawned;
		[SerializeField] int dragAndDropLayer;
		private Vector3 spawnSpot;
		private StopWatch sw;
		private ishtaaap ishtaapu;
		private int ObservationIndex;
		public bool inDesignMode, isMouseDragging;
		[SerializeField] Camera watchesMetreRod;

		[SerializeField] int MIN_OBSERVATIONS;

		private System.Random rnd;
		public ObSet currentObs;

		void Awake()
		{
			PanelLoading.SetActive(!inDesignMode);
			if (PanelLoading.active)
			{
				HideAllButtonsOnMainUI();
			}

			Time.timeScale = 1;
		}
		// Use this for initialization
		void Start()
		{

			Spawned = 0;
			spawnSpot = hotplate.transform.position;
			spawnSpot.y += suspense;

			//PlayerPrefs.DeleteAll();
			ObservationIndex = PlayerPrefs.HasKey("No") ? PlayerPrefs.GetInt("No") : 0;

			rnd = new System.Random();

			sw = StopWatch.GetComponent<StopWatch>();
			ishtaapu = this.GetComponentInChildren<ishtaaap>();
			currentObs = new ObSet();
		}

		// Update is called once per frame
		void FixedUpdate()
		{
			if (!btnsAllHidden)
			{
				// btnSaveActivationState = 
				btnSave.interactable = this.ReadyToSave();
				if (PlayerPrefs.HasKey("No") && PlayerPrefs.GetInt("No") >= MIN_OBSERVATIONS && !pnlTable.activeSelf)
				{
					btnQuit.gameObject.SetActive(true);
				}
				else
				{
					btnQuit.gameObject.SetActive(false);
				}
			}
		}

		public void add(int massReq)
		{
			// if(Spawned>0){
			// 	return ;
			// }
			// if(noSpace()){
			// 	return;
			// }
			if (!ReadyToPlay())
			{
				return;
			}
			GameObject spawnedMass;
			// spawnSpot.z = used.transform.Find("MassHanger1").position.z;
			switch (massReq)
			{
				case 10:
					spawnedMass = Instantiate(mass10g, spawnSpot, new Quaternion());
					break;
				case 50:
					spawnedMass = Instantiate(mass50g, spawnSpot, new Quaternion());
					break;
				case 100:
					spawnedMass = Instantiate(mass100g, spawnSpot, new Quaternion());
					break;
				default:
					spawnedMass = Instantiate(mass10g, spawnSpot, new Quaternion());
					break;
			}
			spawnedMass.GetComponent<Layers>().SetLayersOfSelfAndChildren(dragAndDropLayer);
			Spawned++;
			Debug.Log("added mass of " + massReq + "g");
		}

		private bool noSpace()
		{
			Debug.Log("Spawned: " + Spawned);
			Debug.Log("on hotplate: " + this.GetComponentInChildren<StackIt>().Stack.childCount);
			if ((this.Spawned + this.GetComponentInChildren<StackIt>().Stack.childCount) >= 2)
			{
				return true;
			}
			return false;
		}
		public void AddedToReadyStack()
		{
			Spawned--;
		}

		public void returnToBase(GameObject returned)
		{
			returned.transform.position = spawnSpot;
			returned.transform.rotation = Quaternion.identity;
			if (returned.GetComponent<Rigidbody>() == null)
			{
				returned.AddComponent<Rigidbody>();
				returned.GetComponent<Rigidbody>().mass = float.Parse(returned.tag) / 1000;
				Debug.Log("set mass to " + (float.Parse(returned.tag) / 1000));
				returned.GetComponent<Rigidbody>().angularDrag = 0f;
				returned.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
			}
			returned.GetComponent<Rigidbody>().velocity = Vector3.zero;
			returned.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			returned.transform.SetParent(null);
			Spawned++;
		}

		public int getSpawnCount()
		{
			return Spawned;
		}

		public float getSuspense()
		{
			return suspense;
		}

		public void Save()
		{
			// bool withTime=false
			if (!ReadyToSave())
			{
				return;
			}
			Debug.Log("currentObs: " + currentObs.inspect());
			MarsiveAttack.ginstance.currentObs.t = sw.GetObservedTime();
			MarsiveAttack.Ginstance.GetComponent<ManageObs>().ActivatePanelForAll(true);
			Debug.Log("activated Obs UI");
		}

		public void SaveObs(float baselineM1, float baselineM2)
		{
			int RecordIndex = CheckMassesAndDistance(MarsiveAttack.Ginstance.currentObs.s,
								MarsiveAttack.Ginstance.currentObs.m1,
								MarsiveAttack.Ginstance.currentObs.m2, baselineM1, baselineM2);
			if (RecordIndex == 0)
			{
				// no observations already made for given distance and masses
				// so start new record
				ObservationIndex++;
				RecordIndex = ObservationIndex;
				Save_Worker(RecordIndex, MarsiveAttack.Ginstance.currentObs.s,
							MarsiveAttack.Ginstance.currentObs.m1,
							MarsiveAttack.Ginstance.currentObs.m2,
							MarsiveAttack.Ginstance.currentObs.t);
			}
			else
			{
				// an observation already exists, so need to figure out
				// whether to update t1 or t2.
				// if t2 is 0, then set t2			
				if (0f == PlayerPrefs.GetFloat("t2[" + RecordIndex + "]"))
				{
					Save_Worker(RecordIndex, MarsiveAttack.Ginstance.currentObs.s,
					MarsiveAttack.Ginstance.currentObs.m1,
					MarsiveAttack.Ginstance.currentObs.m2, 0f,
					MarsiveAttack.Ginstance.currentObs.t);
				}
				else
				{
					// else move t2 to t1 and save the new value in t2 
					Save_Worker(RecordIndex, MarsiveAttack.Ginstance.currentObs.s,
					MarsiveAttack.Ginstance.currentObs.m1, MarsiveAttack.Ginstance.currentObs.m2,
					PlayerPrefs.GetFloat("t2[" + RecordIndex + "]"), MarsiveAttack.Ginstance.currentObs.t);
				}
			}
		}
		private void Save_Worker(int RecordIndex, float s, float m1, float m2,
								float t1 = 0f, float t2 = 0f)
		{
			PlayerPrefs.SetFloat("m1[" + RecordIndex + "]", m1);
			PlayerPrefs.SetFloat("m2[" + RecordIndex + "]", m2);
			PlayerPrefs.SetFloat("s[" + RecordIndex + "]", s);
			if (t1 != 0)
			{
				PlayerPrefs.SetFloat("t1[" + RecordIndex + "]", t1);
			}
			if (t2 != 0)
			{
				PlayerPrefs.SetFloat("t2[" + RecordIndex + "]", t2);
			}
			PlayerPrefs.SetInt("No", ObservationIndex);
			PlayerPrefs.Save();

		}
		private int CheckMassesAndDistance(float s, float m1, float m2, float baselineM1, float baselineM2)
		{
			for (int i = 1; i <= ObservationIndex; i++)
			{
				if (PlayerPrefs.GetFloat("s[" + i + "]") == s &&
					(PlayerPrefs.GetFloat("m1[" + i + "]") == m1 &&
					PlayerPrefs.GetFloat("m2[" + i + "]") == m2))
				{
					return i;
				}
			}
			return 0;
		}

		public void PrintPlayerPrefs()
		{
			string strMsg = "";
			for (int i = 1; i <= ObservationIndex; i++)
			{
				strMsg = "m1[" + i + "]: " + PlayerPrefs.GetFloat("m1[" + i + "]");
				strMsg += "m2[" + i + "]: " + PlayerPrefs.GetFloat("m2[" + i + "]");
				strMsg += "s[" + i + "]: " + PlayerPrefs.GetFloat("s[" + i + "]");
				strMsg += "t1[" + i + "]: " + PlayerPrefs.GetFloat("t1[" + i + "]");
				strMsg += "t2[" + i + "]: " + PlayerPrefs.GetFloat("t2[" + i + "]");
				Debug.Log(strMsg);
			}
		}

		public void ResetExperiment()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		public bool IsSWRunning()
		{
			return sw.isRunning();
		}

		public bool IsDropping()
		{
			return (this.state == MarsiveState.dropping);
		}
		public bool HasTouchedDown()
		{
			return (this.state == MarsiveState.grounded || this.state == MarsiveState.crashed || this.state == MarsiveState.saved);
		}

		public bool ReadyToPlay()
		{
			return (this.state == MarsiveState.stabilised);
		}

		public bool ReadyToSave()
		{
			//return (this.state==MarsiveState.stabilised || this.state==MarsiveState.grounded);
			return (this.state == MarsiveState.grounded || this.state == MarsiveState.crashed);
		}

		public void FinalAdjustmentToMetreRod()
		{
			Debug.Log("final adjustment by MarsiveAttack");
			FinalAdjustment(Floor.position.y);
		}

		public void SetStateToGrounded()
		{
			this.state = MarsiveState.grounded;
		}
		public void SetStateToSaved()
		{
			this.state = MarsiveState.saved;
		}

		public bool IsTargetDroppingOnExpectedDestination(out Transform Destination)
		{
			Destination = InActiveUse;
			if (null == Destination)
			{
				return false;
			}
			if (Destination.GetComponent<SnapTo>().GetMassToSnapIntoPlace() != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public void FinalAdjustment(float adjY)
		{

			Debug.Log("current pos: " + this.transform.position + ", y adjustment: " + (Vector3.up * adjY));
			MetreRod.transform.Translate(Vector3.up * adjY, Space.World);
			Table.transform.Translate(Vector3.up * adjY, Space.World);
			Debug.Log("pos after translation: " + this.transform.position);
		}

		public Transform GetProjectedMark1()
		{
			return markerM1.GetProjectedMark();
		}

		public Camera GetCameraWatchingMetreRod()
		{
			return watchesMetreRod;
		}


		public void ResetAllHiddenButtonsOnMainUI()
		{
			// reset the activeSelf propery of each previously hidden button
			// using the state-saving bools
			btnSave.gameObject.SetActive(btnSaveActivationState);
			btnObsCalc.gameObject.SetActive(btnObsCalcActivationState);
			btnResetPractical.gameObject.SetActive(btnResetPracticalActivationState);
			btnQuit.gameObject.SetActive(btnQuitActivationState);

			// reset the "All Hidden" flag
			btnsAllHidden = false;
		}

		/*
		Purpose: Hide all the buttons on the main user-interface
		Need: Just to be able to present a less cluttered look
		*/
		public void HideAllButtonsOnMainUI()
		{
			Debug.Log("hiding all buttons on UI");
			// first save current state of all the buttons
			btnSaveActivationState = btnSave.gameObject.activeSelf;
			btnObsCalcActivationState = btnObsCalc.gameObject.activeSelf;
			btnResetPracticalActivationState = btnResetPractical.gameObject.activeSelf;
			btnQuitActivationState = btnQuit.gameObject.activeSelf;

			// then un-set their activeSelf property
			btnSave.gameObject.SetActive(false);
			btnObsCalc.gameObject.SetActive(false);
			btnResetPractical.gameObject.SetActive(false);
			btnQuit.gameObject.SetActive(false);

			// then declare that all the buttons have been hidden
			// probably useful for some other function wanting to know
			// what's up with the UI
			btnsAllHidden = true;
			Debug.Log("btnSave activation status : " + btnSave.gameObject.activeSelf.ToString());
		}

		public bool AreAllButtonsHidden()
		{
			return btnsAllHidden;
		}

		public void QuitAtwoodMachine()
		{
			PlayerPrefs.SetString("quiz_mode", "post");
			SceneManager.LoadScene("quiz");
		}


	}
}