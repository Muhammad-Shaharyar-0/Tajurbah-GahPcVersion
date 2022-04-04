using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_3
{
    public class Teoman_Eval
    {
        // Copied from: https://stackoverflow.com/a/6052679
        public static double Evaluate(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }
    }
    public class CheckUserCalc : MonoBehaviour
    {
        [SerializeField] int NumOfTerms;
        [SerializeField] GameObject t1, t2, t3, myself;
        [SerializeField] string expression, emptyFlag;
        [SerializeField] float tolerance;
        [SerializeField] Color OK, Bad;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (0 == NumOfTerms)
            {
                return;
            }
            else if (1 == NumOfTerms && (string.IsNullOrEmpty(t1.GetComponent<Text>().text) || t1.GetComponent<Text>().text == emptyFlag))
            {
                return;
            }
            else if (2 == NumOfTerms && (string.IsNullOrEmpty(t1.GetComponent<Text>().text) || t1.GetComponent<Text>().text == emptyFlag ||
                string.IsNullOrEmpty(t2.GetComponent<Text>().text) || t2.GetComponent<Text>().text == emptyFlag))
            {
                return;
            }
            else if (3 == NumOfTerms && (string.IsNullOrEmpty(t1.GetComponent<Text>().text) || t1.GetComponent<Text>().text == emptyFlag ||
                string.IsNullOrEmpty(t2.GetComponent<Text>().text) || t2.GetComponent<Text>().text == emptyFlag ||
                string.IsNullOrEmpty(t3.GetComponent<Text>().text) || t3.GetComponent<Text>().text == emptyFlag))
            {
                return;
            }


            double userCalc = 0;
            string parsedExpression = "0";
            if (double.TryParse(myself.GetComponentInChildren<Text>().text, out userCalc))
            {
                if (1 == NumOfTerms)
                {
                    parsedExpression = expression.Replace("{1}", t1.GetComponent<Text>().text);
                }
                else if (2 == NumOfTerms)
                {
                    parsedExpression = expression.Replace("{1}", t1.GetComponent<Text>().text).Replace("{2}", t2.GetComponent<Text>().text);
                }
                else if (3 == NumOfTerms)
                {
                    parsedExpression = expression.Replace("{1}", t1.GetComponent<Text>().text).Replace("{2}", t2.GetComponent<Text>().text).Replace("{3}", t3.GetComponent<Text>().text);
                }
                double actualCalc = Teoman_Eval.Evaluate(parsedExpression);
                // Debug.Log(myself.name + ": actualCalc: " + actualCalc + ", userCalc: " + userCalc);
                if (Mathf.Abs((float)(actualCalc - userCalc)) > tolerance)
                {
                    // Debug.Log("inaccurate");
                    myself.GetComponentInChildren<Text>().color = Bad;
                }
                else
                {
                    // Debug.Log("good job");
                    myself.GetComponentInChildren<Text>().color = OK;
                }
            }
        }
    }
}