using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace P9_Dynamics_3_3
{
    public class tableController : MonoBehaviour
    {

        public GameObject Data;
        [SerializeField] ConnectInputGroup connector;
        [SerializeField] string T_TextName, A_TextName, G_TextName, hdrCSV, fNameCSV, strResPathObs;
        [SerializeField] Color zeroRow, oneRow;
        [SerializeField] Text result, log;
        int No;

        // Use this for initialization
        void Start()
        {
            if (string.IsNullOrEmpty(hdrCSV))
            {
                hdrCSV = "session_id,write_time,obs_no,m1,m2,s,t1,t2,T,A,G";
            }
            if (string.IsNullOrEmpty(fNameCSV))
            {
                fNameCSV = "AtwoodObs.csv";
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnEnable()
        {
            while (this.transform.childCount > 0)
            {
                Transform c = this.transform.GetChild(0);
                c.SetParent(null);
                Destroy(c.gameObject);
            }

            DataRowController drcTmp;

            for (int i = 0; i < PlayerPrefs.GetInt("No"); i++)
            {

                No = i + 1;
                if (PlayerPrefs.HasKey("m1[" + No + "]") && PlayerPrefs.HasKey("m2[" + No + "]") &&
                    PlayerPrefs.HasKey("s[" + No + "]") && PlayerPrefs.HasKey("t1[" + No + "]"))
                {
                    GameObject go = Instantiate(Data);
                    Data.GetComponent<Image>().color = (i % 2 == 0 ? zeroRow : oneRow);
                    drcTmp = go.GetComponent<DataRowController>();
                    drcTmp.SetConnector(connector);
                    go.transform.SetParent(this.transform, false);
                    go.transform.Find("No").GetComponent<Text>().text = No.ToString();
                    go.transform.Find("m1").GetComponent<Text>().text = PlayerPrefs.GetFloat("m1[" + No + "]").ToString();
                    go.transform.Find("m2").GetComponent<Text>().text = PlayerPrefs.GetFloat("m2[" + No + "]").ToString();
                    go.transform.Find("s").GetComponent<Text>().text = PlayerPrefs.GetFloat("s[" + No + "]").ToString();
                    go.transform.Find("t1").GetComponent<Text>().text = PlayerPrefs.GetFloat("t1[" + No + "]").ToString();
                    if (PlayerPrefs.HasKey("t2[" + No + "]"))
                    {
                        go.transform.Find("t2").GetComponent<Text>().text = PlayerPrefs.GetFloat("t2[" + No + "]").ToString();
                    }
                    if (PlayerPrefs.HasKey("T[" + No + "]"))
                    {
                        drcTmp.VSLText_T.SetText(PlayerPrefs.GetString("T[" + No + "]"));
                        go.transform.Find("T").GetComponent<InputField>().SetTextWithoutNotify(PlayerPrefs.GetString("T[" + No + "]"));
                    }
                    if (PlayerPrefs.HasKey("A[" + No + "]"))
                    {
                        drcTmp.VSLText_A.SetText(PlayerPrefs.GetString("A[" + No + "]"));
                        go.transform.Find("A").GetComponent<InputField>().SetTextWithoutNotify(PlayerPrefs.GetString("A[" + No + "]"));
                    }
                    if (PlayerPrefs.HasKey("G[" + No + "]"))
                    {
                        drcTmp.VSLText_G.SetText(PlayerPrefs.GetString("G[" + No + "]"));
                        go.transform.Find("G").GetComponent<InputField>().SetTextWithoutNotify(PlayerPrefs.GetString("G[" + No + "]"));
                    }
                    // connect to the correct table panel controller
                    drcTmp.VSLText_T.SetCtlInputGrp(this.transform.parent.GetComponent<TablePanelController>());
                    drcTmp.VSLText_A.SetCtlInputGrp(this.transform.parent.GetComponent<TablePanelController>());
                    drcTmp.VSLText_G.SetCtlInputGrp(this.transform.parent.GetComponent<TablePanelController>());
                }
            }
        }

        void OnDisable()
        {
            int i = 0;
            while (i < PlayerPrefs.GetInt("No"))
            {
                No = i + 1;
                if (PlayerPrefs.HasKey("m1[" + No + "]") && PlayerPrefs.HasKey("m2[" + No + "]") &&
                    PlayerPrefs.HasKey("s[" + No + "]") &&
                    !PlayerPrefs.HasKey("t1[" + No + "]") && !PlayerPrefs.HasKey("t2[" + No + "]"))
                {
                    // move down verything that comes after this point 
                    ReIndex(No);
                    // and then delete the last entry 
                    // (since it's been copied to the second-last entry)
                    DeleteObs(PlayerPrefs.GetInt("No"));
                    PlayerPrefs.SetInt("No", PlayerPrefs.GetInt("No") - 1);
                }
                i++;
            }
        }

        private void ReIndex(int GapAt)
        {
            for (int i = GapAt; i <= PlayerPrefs.GetInt("No"); i++)
            {
                DemoteObs(i + 1);
            }
        }

        private void DeleteObs(int DeleteThis)
        {
            PlayerPrefs.DeleteKey("m1[" + DeleteThis + "]");
            PlayerPrefs.DeleteKey("m2[" + DeleteThis + "]");
            PlayerPrefs.DeleteKey("s[" + DeleteThis + "]");
            PlayerPrefs.DeleteKey("T[" + DeleteThis + "]");
            PlayerPrefs.DeleteKey("A[" + DeleteThis + "]");
            PlayerPrefs.DeleteKey("G[" + DeleteThis + "]");
        }
        private void DemoteObs(int CurrentIndex)
        {
            PlayerPrefs.SetFloat("m1[" + (CurrentIndex - 1) + "]", PlayerPrefs.GetFloat("m1[" + CurrentIndex + "]"));
            PlayerPrefs.SetFloat("m2[" + (CurrentIndex - 1) + "]", PlayerPrefs.GetFloat("m2[" + CurrentIndex + "]"));
            PlayerPrefs.SetFloat("s[" + (CurrentIndex - 1) + "]", PlayerPrefs.GetFloat("s[" + CurrentIndex + "]"));
            if (PlayerPrefs.HasKey("t1[" + (CurrentIndex) + "]"))
            {
                PlayerPrefs.SetFloat("t1[" + (CurrentIndex - 1) + "]", PlayerPrefs.GetFloat("t1[" + CurrentIndex + "]"));
            }
            if (PlayerPrefs.HasKey("t2[" + (CurrentIndex) + "]"))
            {
                PlayerPrefs.SetFloat("t2[" + (CurrentIndex - 1) + "]", PlayerPrefs.GetFloat("t2[" + CurrentIndex + "]"));
            }
            if (PlayerPrefs.HasKey("T[" + (CurrentIndex) + "]"))
            {
                PlayerPrefs.SetString("T[" + (CurrentIndex - 1) + "]", PlayerPrefs.GetString("T[" + CurrentIndex + "]"));
            }
            if (PlayerPrefs.HasKey("A[" + (CurrentIndex) + "]"))
            {
                PlayerPrefs.SetString("A[" + (CurrentIndex - 1) + "]", PlayerPrefs.GetString("T[" + CurrentIndex + "]"));
            }
            if (PlayerPrefs.HasKey("G[" + (CurrentIndex) + "]"))
            {
                PlayerPrefs.SetString("G[" + (CurrentIndex - 1) + "]", PlayerPrefs.GetString("G[" + CurrentIndex + "]"));
            }
        }

        public void toCSV()
        {
            log.gameObject.SetActive(true);
            log.text += "\nstarting";
            string collector = string.Empty;
            int localNo = 0;

            for (int i = 0; i < PlayerPrefs.GetInt("No"); i++)
            {

                localNo = i + 1;
                collector += "\n" + PlayerPrefs.GetString("SessionID") + "," + System.DateTime.Now.ToString("yyyyMMddHHmmss") + "," + localNo.ToString();
                if (PlayerPrefs.HasKey("m1[" + localNo + "]"))
                {
                    collector += "," + PlayerPrefs.GetFloat("m1[" + localNo + "]").ToString();
                }
                else
                {
                    collector += ",";
                }
                if (PlayerPrefs.HasKey("m2[" + localNo + "]"))
                {
                    collector += "," + PlayerPrefs.GetFloat("m2[" + localNo + "]").ToString();
                }
                else
                {
                    collector += ",";
                }
                if (PlayerPrefs.HasKey("s[" + localNo + "]"))
                {
                    collector += "," + PlayerPrefs.GetFloat("s[" + localNo + "]").ToString();
                }
                else
                {
                    collector += ",";
                }
                if (PlayerPrefs.HasKey("t1[" + localNo + "]"))
                {
                    collector += "," + PlayerPrefs.GetFloat("t1[" + localNo + "]").ToString();
                }
                else
                {
                    collector += ",";
                }
                if (PlayerPrefs.HasKey("t2[" + localNo + "]"))
                {
                    collector += "," + PlayerPrefs.GetFloat("t2[" + localNo + "]").ToString();
                }
                else
                {
                    collector += ",";
                }
                if (PlayerPrefs.HasKey("T[" + localNo + "]"))
                {
                    collector += "," + PlayerPrefs.GetString("T[" + localNo + "]");
                }
                else
                {
                    collector += ",";
                }
                if (PlayerPrefs.HasKey("A[" + localNo + "]"))
                {
                    collector += "," + PlayerPrefs.GetString("A[" + localNo + "]");
                }
                else
                {
                    collector += ",";
                }
                if (PlayerPrefs.HasKey("G[" + localNo + "]"))
                {
                    collector += "," + PlayerPrefs.GetString("G[" + localNo + "]");
                }
                else
                {
                    collector += ",";
                }
            }

            log.text += " | about to write " + localNo + " obs";
            string data = hdrCSV + collector;
            string path = Path.Combine(GetPersistanceLocation(), fNameCSV);
            log.text += " to " + path;
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(data);
            writer.Close();

            result.text += "\nOK";
        }

        string GetPersistanceLocation()
        {
            string folderPath = strResPathObs;
            if (Application.isEditor)
            {
                // working in Unity Editor
                folderPath = Path.Combine(Path.Combine("Assets", "Resources"), folderPath);
            }
            else
            {
                // working on deployment target
                folderPath = Path.Combine(Application.persistentDataPath, folderPath);
                if (!Directory.Exists(folderPath))
                {
                    Debug.Log("going to create new folder for table of observations at " + folderPath);
                    Directory.CreateDirectory(folderPath);
                }
            }
            return folderPath;
        }

    }
}