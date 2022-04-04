using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_3
{
    public class RemoveDataElement : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] GameObject ObsNum, t_this, t_other;
        void Start()
        {

        }

        // Update is called once per frame
        public void RemoveObs()
        {
            // un-set value in associated text element
            // tx.GetComponent<Text>().text = "";
            // remove value from playerprefs
            string No = ObsNum.GetComponent<Text>().text;
            // if removing t1, move t2 to t1
            if (t_this.name == "t1")
            {
                if (PlayerPrefs.HasKey("t2[" + No + "]"))
                {
                    PlayerPrefs.SetFloat("t1[" + No + "]", PlayerPrefs.GetFloat("t2[" + No + "]"));
                    PlayerPrefs.DeleteKey("t2[" + No + "]");
                    t_this.GetComponent<Text>().text = PlayerPrefs.GetFloat("t1[" + No + "]").ToString();
                    t_other.GetComponent<Text>().text = "-";
                }
                else
                {
                    PlayerPrefs.DeleteKey("t1[" + No + "]");
                    t_this.GetComponent<Text>().text = "-";
                }
            }
            else if (t_this.name == "t2")
            {
                PlayerPrefs.DeleteKey("t2[" + No + "]");
                t_this.GetComponent<Text>().text = "-";
            }

            // checkUserCalc should automatically highlight inconsistencies downstream
        }
    }
}