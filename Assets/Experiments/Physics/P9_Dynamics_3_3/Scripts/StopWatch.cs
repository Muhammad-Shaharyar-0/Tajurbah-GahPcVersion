using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_3
{
	public class StopWatch : MonoBehaviour
	{
		private float sinceDropped, sinceDroppedActual;
		private bool runStopWatch, SWEnabled;
		[SerializeField] Text swText, swActualText;
		[SerializeField] string strActualTimePrefix;
		[SerializeField] UnityEngine.UI.Button btnStart, btnStop, btnReset;
		[SerializeField] Color deactivated, activatedSWText, activatedSWActualText;

		// Use this for initialization
		void Start()
		{
			sinceDropped = 0f;
			sinceDroppedActual = 0f;
			// swText = this.gameObject.transform.Find("StopWatchText").GetComponent<Text>();
			// swActualText = this.gameObject.transform.Find("ActualStopWatchText").GetComponent<Text>();
			runStopWatch = false;
			SWEnabled = false;
		}

		void UpdateSW(bool blnForce = false)
		{
			System.TimeSpan t = System.TimeSpan.FromSeconds(sinceDropped);
			string answer = System.String.Format("{0:D2}.{1:D2}",
				t.Seconds,
				t.Milliseconds / 10);
			swText.text = answer;
			// update actual time as well, unless...
			// 		m1 has touched down...
			// 		but even if m1 has touched down, if the blnForce flag is on, update!
			// blnForce is on when user clicks on Reset SW button
			// if(!MarsiveAttack.Ginstance.HasTouchedDown() || blnForce){
			// 	swActualText.text = this.strActualTimePrefix + "\n\n" + answer;
			// 	swActualText.gameObject.SetActive(true);
			// }
		}

		void UpdateSWActualReading(bool blnForce = false)
		{
			if (MarsiveAttack.Ginstance.IsDropping() || blnForce)
			{
				System.TimeSpan t = System.TimeSpan.FromSeconds(sinceDroppedActual);
				string answer = System.String.Format("{0:D2}.{1:D2}",
					t.Seconds,
					t.Milliseconds / 10);
				swActualText.text = (string.IsNullOrEmpty(this.strActualTimePrefix) ? answer : this.strActualTimePrefix + "\n\n" + answer);
				swActualText.gameObject.SetActive(true);
			}
		}

		private void ActivateStopWatch(MarsiveState MarsiveStatus)
		{
			// Debug.Log("marsive status: " + MarsiveStatus);
			if (MarsiveStatus == MarsiveState.swinging && this.SWEnabled)
			{
				EnableStopWatch(false);
				this.SWEnabled = false;
			}
			else if (MarsiveStatus == MarsiveState.stabilised && !this.SWEnabled)
			{
				EnableStopWatch(true);
				this.SWEnabled = true;
			}
		}

		public void EnableStopWatch(bool activateSW)
		{
			btnStart.gameObject.SetActive(activateSW);
			btnStop.gameObject.SetActive(false);
			btnReset.gameObject.SetActive(false);
			swText.GetComponent<Text>().color = activateSW ? activatedSWText : deactivated;
			swActualText.GetComponent<Text>().color = activateSW ? activatedSWActualText : deactivated;
		}

		void FixedUpdate()
		{

			ActivateStopWatch(MarsiveAttack.Ginstance.state);
			if (MarsiveAttack.Ginstance.IsDropping())
			{
				sinceDroppedActual += Time.fixedDeltaTime;
			}
			if (runStopWatch)
			{
				sinceDropped += Time.fixedDeltaTime;
			}
			UpdateSW();
			UpdateSWActualReading();
		}

		void Update()
		{

		}

		public void ToggleSW()
		{
			if (!IsInteractable())
			{
				Debug.Log("cannot toggle as not interactable");
				return;
			}
			if (MarsiveAttack.Ginstance.currentObs.s < 0)
			{
				MarsiveAttack.Ginstance.GetComponent<ManageObs>().ActivatePanelForS(true);
				return;
			}
			runStopWatch = !runStopWatch;
			Debug.Log("just toggled it to: " + runStopWatch);
		}

		public bool IsInteractable()
		{
			if (MarsiveAttack.Ginstance.state == MarsiveState.swinging ||
				(!runStopWatch &&
				(MarsiveAttack.Ginstance.state == MarsiveState.grounded || MarsiveAttack.Ginstance.state == MarsiveState.crashed)))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		public void ResetSW()
		{
			if (!IsInteractable())
			{
				Debug.Log("cannot reset as not interactable");
				return;
			}
			runStopWatch = false;
			sinceDropped = 0f;
			sinceDroppedActual = 0f;
			UpdateSW(true);
			UpdateSWActualReading(true);
		}

		public bool isRunning()
		{
			return runStopWatch;
		}

		public float GetObservedTime()
		{
			return float.Parse(swText.text);
		}
	}
}