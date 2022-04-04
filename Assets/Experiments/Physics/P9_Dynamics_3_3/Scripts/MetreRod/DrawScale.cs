using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P9_Dynamics_3_3
{
	public class DrawScale : MonoBehaviour
	{
		public enum Handedness { LeftHanded = 0, RightHanded = 1 };
		public Handedness handedness;
		public bool CountBackwards;

		[SerializeField] Transform startAt, endAt, scaleObject, viewObject, markings;
		[SerializeField] GameObject markSmallest, markMid, markTallest, lblCM;
		[SerializeField] float startMargin, endMargin;
		[SerializeField] int size;
		[SerializeField] Vector3 Pos, RotationEuler;
		private bool SetUpComplete;
		private const float _ONEMM = 1 / 1000f;
		// [SerializeField] int myLayer; // I think Unity has a bug!!!


		// Use this for initialization
		void SetUpScale()
		{
			float totalLength = size + startMargin + endMargin;
			resizeScale(viewObject, totalLength);
			translateInX(startAt, (totalLength / (-2.0f)) + startMargin);
			translateInX(endAt, (totalLength / (2.0f)) - endMargin);
			// translateInX(startAt, (CountBackwards? size/(2.0f) : size/(-2.0f) ) ) ;
			// translateInX(endAt, (CountBackwards? size/(-2.0f) : size/(2.0f) ) );
		}

		private void translateInX(Transform obj, float locX)
		{
			Vector3 temp = obj.localPosition;
			temp.x = locX;
			obj.localPosition = temp;
		}

		private void resizeScale(Transform obj, float locX)
		{
			Vector3 temp = obj.localScale;
			temp.x = locX;
			obj.localScale = temp;
		}

		// Update is called once per frame
		void Update()
		{

		}

		GameObject placeOneMark(Vector3 pos, GameObject markToPlace)
		{
			GameObject mark = Instantiate(markToPlace, pos, Quaternion.identity, markings);
			//mark.layer = this.myLayer;
			//Debug.Log("assigned layer: "  + mark.layer + ", tried to assign: " + this.myLayer);
			if (this.handedness == Handedness.RightHanded)
			{
				mark.GetComponent<MarkUtil>().moveRight();
			}
			return mark;
		}

		GameObject placeOneLabel(Vector3 pos, GameObject textToPlace, string strMsg)
		{
			//Debug.Log(strMsg+": "+ pos);
			textToPlace.GetComponent<TMP_ScaleLabel2>().displayText = strMsg;
			textToPlace.GetComponent<TMP_ScaleLabel2>().Position.x = pos.x;
			if (this.handedness == Handedness.RightHanded)
			{
				//textToPlace.GetComponent<TMP_ScaleLabel2>().yOffSet = -1*textToPlace.GetComponent<TMP_ScaleLabel2>().yOffSet;
				textToPlace.GetComponent<TMP_ScaleLabel2>().textAlignment = TMPro.TextAlignmentOptions.CaplineRight;
			}
			else
			{
				//textToPlace.GetComponent<TMP_ScaleLabel2>().Position.y = pos.y;
				textToPlace.GetComponent<TMP_ScaleLabel2>().textAlignment = TMPro.TextAlignmentOptions.CaplineLeft;
			}
			// Debug.Log(strMsg + ": " + textToPlace.GetComponent<TMP_ScaleLabel2>().Position);
			GameObject lbl = Instantiate(textToPlace, scaleObject.position, Quaternion.identity, markings);
			//lbl.layer = this.myLayer;
			// Debug.Log(strMsg + ": " + textToPlace.GetComponent<TMP_ScaleLabel2>().Position);
			return lbl;
		}

		Vector3 drawBatch(Vector3 start, int count, float separation, GameObject markToPlace)
		{

			Vector3 pos = start;
			for (int i = 0; i < count; i++)
			{
				// Debug.Log("pos vector: " + pos);
				placeOneMark(pos, markToPlace);
				pos.x += separation;
			}

			return pos;
		}

		Vector3 drawOneCM(Vector3 start, int label)
		{
			placeOneLabel(start, lblCM, label.ToString());
			Vector3 tempNextStart = drawBatch(start, 1, _ONEMM, markTallest);
			tempNextStart = drawBatch(tempNextStart, 4, _ONEMM, markSmallest);
			tempNextStart = drawBatch(tempNextStart, 1, _ONEMM, markMid);
			return drawBatch(tempNextStart, 4, _ONEMM, markSmallest);
		}

		Vector3 drawOneMetre(Vector3 start, int metreIndex, bool printLast = false)
		{
			Vector3 pos = start;
			int i, label;
			for (i = 0; i < 100; i++)
			{
				if (this.CountBackwards)
				{
					label = (this.size * 100) - ((metreIndex * 100) + i);
				}
				else
				{
					label = (metreIndex * 100) + i;
				}
				pos = drawOneCM(pos, label);
				// Debug.Log((i+1) +": "+ pos);
			}
			if (printLast)
			{
				placeOneMark(pos, markTallest);
				placeOneLabel(pos, lblCM, (CountBackwards ? 0 : (metreIndex * 100) + i).ToString());
			}
			return pos;
		}

		void Start()
		{
			SetUpComplete = false;
			SetUpScale();
			Vector3 tempNextPos = startAt.position;
			int mi = 0;
			while (mi < this.size)
			{
				tempNextPos = drawOneMetre(tempNextPos, mi, (mi == (this.size - 1)));
				mi++;
			}
			this.scaleObject.SetPositionAndRotation(this.Pos, Quaternion.Euler(this.RotationEuler));
			SetUpComplete = true;
		}

	}
}
