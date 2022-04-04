using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class WriteDataController : MonoBehaviour
{
    public static WriteDataController Instance;
    string ResultsFile;

    private List<string[]> rowData = new List<string[]>();

    int No = 1;

    //[RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        GameObject WriteDataControllerObject = new GameObject();
        WriteDataControllerObject.transform.position = Vector3.zero;
        WriteDataControllerObject.name = "WriteDataControllerObject";
        WriteDataControllerObject.AddComponent<WriteDataController>();
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        ResultsFile = Application.dataPath + "/Results of Session(" + (UnityEngine.Analytics.AnalyticsSessionInfo.sessionId).ToString() + ").csv";

        string[] rowDataTemp = new string[2];
        rowDataTemp[0] = "No";
        rowDataTemp[1] = "Event";
        rowData.Add(rowDataTemp);

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        StreamWriter outStream = System.IO.File.CreateText(ResultsFile);
        outStream.WriteLine(sb);
        outStream.Close();

    }

    public void WriteEventData(string Event=null)
    {
        string[] rowDataTemp = new string[2];
        rowDataTemp[0] = No.ToString();
        rowDataTemp[1] = Event;
        rowData.Add(rowDataTemp);

        No++;

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        StreamWriter outStream = System.IO.File.CreateText(ResultsFile);
        outStream.WriteLine(sb);
        outStream.Close();
    }
}
