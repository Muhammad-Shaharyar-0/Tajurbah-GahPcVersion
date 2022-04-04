using UnityEngine;
using TMPro;


/*NOTE: This script is for controlling boiling water temp..,
 * The time and temperature readings used in this script. have been observed through a real time boiling procedure
 {
    You have to set current and final temperature..
    if IAmActivated== true, DecreaseTemp==false -> temperature starts increasing until reach final temperature
    and
    if IAmActivated== false, DecreaseTemp==true -> temperature starts decreasing until reach starting temperature

    _KTimesSpeedUpProcess shoukd not be less than or equal to zero
 }
*/


struct TemperatureTimeEntry
{
    public float StartingTempRange { get; }
    public float EndingTempRange { get; }
    public float DeltaTemp { get; }
    public float TotalTimeRequired { get; }
    public float TimeRequiredFor1Degree { get; }

    public TemperatureTimeEntry(float str, float etr, float ttr)
    {
        StartingTempRange = str;
        EndingTempRange = etr;
        DeltaTemp = (etr - str)+1;
        TotalTimeRequired = ttr;
        TimeRequiredFor1Degree = TotalTimeRequired/ DeltaTemp;
    }
}
namespace Tajurbah_Gah
{
    public class TemperatureController : MonoBehaviour
    {
        //List<TemperatureTimeEntry> TemperatureTimeGraph = new List<TemperatureTimeEntry>();
        //TemperatureTimeEntry NewTTGEntry = new TemperatureTimeEntry();

        int TTGEntriesSize = 8;
        TemperatureTimeEntry[] TemperatureTimeGraph = new TemperatureTimeEntry[]
         {
        new TemperatureTimeEntry(16, 25, 102),
        new TemperatureTimeEntry(25, 34, 102),
        new TemperatureTimeEntry(35, 45, 102),
        new TemperatureTimeEntry(46, 55, 102),
        new TemperatureTimeEntry(56, 65, 102),
        new TemperatureTimeEntry(66, 76, 126),
        new TemperatureTimeEntry(77, 90, 156),
        new TemperatureTimeEntry(90, 100, 300)
         };

        [SerializeField] GameObject ReferencePositionObj;
        public float CurrentTemp, FinalTemp, StartingTemp, DecreasingTimeFor1Degree, NewTemp;

        int TTGCurrentIndex;

        float PassedTime, TTTime;

        bool ReachedItsFinalTemp { get; set; }

        /*for heating setup Thermo: 
         TotalMercuryScaledObj = 5.56f;  //1 - 6.56 
         TotalReadingPartsOnThermometer = 140f;
         CurrentTemp = 18, FinalTemp = 100;

        Degree1TransformYPos -> 0.00876
        */

        public float TotalMercuryScaledObj;

        public float TotalReadingPartsOnThermometer;

        float _1DegreeMercuryRiseScale;

        public bool IAmActivated, DecreaseTemp;

        public int _KTimesSpeedUpProcess;

        void Start()
        {
            TTTime = 0.0f;
            IAmActivated = false;
            DecreaseTemp = false;
            ReachedItsFinalTemp = false;

            StartingTemp = CurrentTemp;

            // _KTimesSpeedUpProcess = 2;  //byDefault        how fastly increase or decrease temperature.
            Set_KTimesSpeedUpProcess(2);

            TTGCurrentIndex = FindCurrentIndexInTTG(CurrentTemp);

            PassedTime = 0.0f;

            CalculateMercuryRiseScale();
            DecreasingTimeFor1Degree = 2;

            ResizeMercury(gameObject, ((CurrentTemp) * _1DegreeMercuryRiseScale));   //0 tk 30 degree temp pehly add

        }

       

        int FindCurrentIndexInTTG(float Temp)
        {
            for (int i = 0; i < TTGEntriesSize; i++)
            {
                if (Temp >= TemperatureTimeGraph[i].StartingTempRange && Temp < TemperatureTimeGraph[i].EndingTempRange)
                    return i;
            }
            return 0;
        }

        public void SetIAmActivated(bool status)
        {
            IAmActivated = status;
        }

        public bool GetIAmActivated()
        {
            return IAmActivated;

        }
        public void StartDecreasingTemp(bool status)
        {
            DecreaseTemp = status;

        }

        public bool GetDecreasingTempStatus()
        {
            return DecreaseTemp;
        }

        void CalculateMercuryRiseScale()
        {
            _1DegreeMercuryRiseScale = TotalMercuryScaledObj / TotalReadingPartsOnThermometer;
        }

        void ResizeMercury(GameObject Gobj, float amount)
        {
            Gobj.transform.localScale = new Vector3(Gobj.transform.localScale.x, Gobj.transform.localScale.y, Gobj.transform.localScale.z + amount);
        }

        public void ResetPassedTime()
        {
            PassedTime = 0.0f;
        }
        public bool GetReachedItsFinalTempStatus()
        {
            return ReachedItsFinalTemp;
        }

        public float GetCurrentTemperature()
        {
            return CurrentTemp;
        }

        public void SetCurrentTemp(float temp)
        {
            CurrentTemp = temp;
        }

        public void SetFinalTemp(float temp)
        {
            FinalTemp = temp;
            SetReachedItsFinalTempStatus(false);
        }

        public void SetReachedItsFinalTempStatus(bool s)
        {
            ReachedItsFinalTemp = s;
        }

        public void Set_KTimesSpeedUpProcess(int t)
        {
            if (_KTimesSpeedUpProcess <= 0)
            {
                _KTimesSpeedUpProcess = 1;
                return;
            }

            _KTimesSpeedUpProcess = t;
        }

    }
}