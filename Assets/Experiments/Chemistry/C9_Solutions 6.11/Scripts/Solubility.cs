using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C9_Solutions_6_11
{
    public class Solubility : MonoBehaviour
    {
        float Temperature;
        
        [SerializeField]
        float initialTemperature = 25f;
        [SerializeField]
        float MaxTemperature = 100f;

        List<GameObject> SugarEntered;
        float solubiltyofSugar;
        float SugarDisolved;



        List<GameObject> NaClEntered;
        float solubiltyofNaCl;
        float NaClDisolved;



        List<GameObject> BaCO3Entered;
        float solubiltyofBaCO3;
        float BaCO3Disolved;
        int BaCO3Active;


        List<GameObject> KNO3Entered;
        float solubiltyofKNO3;
        float KNO3Disolved;

        [Header("Relative Solubility change Rate due to temperature")]
        [SerializeField]
        float Sugar_ChangeRate = 1.75f;
        [SerializeField]
        float Nacl_ChangeRate = 1.001f;
        [SerializeField]
        float BaCO3_ChangeRate = 0.75f;
        [SerializeField]
        float KNO3_ChangeRate = 2.5f;

        [SerializeField] ParticleSystem smoke;
        [SerializeField] ParticleSystem waterEffect;
        [SerializeField] ParticleSystem bubbleEffect;
        Tajurbah_Gah.FlameController flame;
        Tajurbah_Gah.TemperatureController Thermometer;
        bool flag = false;

        bool NaclStoppedDisolving = false;
        bool BaCO3StoppedDisolving = false;
        bool KnO3StoppedDisolving = false;

        bool SugarPracticle = false;
        bool SmookePlaying = false;
        WaitForSeconds waitfor1second = new WaitForSeconds(1f);
        // Start is called before the first frame update
        void Start()
        {
            flame = GameObject.FindObjectOfType<Tajurbah_Gah.FlameController>();
            Thermometer = GameObject.FindObjectOfType<Tajurbah_Gah.TemperatureController>();
            SugarDisolved = 0;
            SugarEntered = new List<GameObject>();
            NaClEntered = new List<GameObject>();
            BaCO3Entered = new List<GameObject>();
            KNO3Entered = new List<GameObject>();
            Temperature = initialTemperature;
            solubiltyofSugar = 40;
            solubiltyofNaCl = 30;
            solubiltyofBaCO3 = 25;
            solubiltyofKNO3 = 25;
            BaCO3Active = 0;
            SugarPracticle = false;
            if(Thermometer!=null)
            {
                Thermometer.CurrentTemp = initialTemperature;
            }
            
            SmookePlaying = false;
        }
        private void ChangeSolubility()
        {
            solubiltyofSugar = 40 + ((Temperature - initialTemperature) * Sugar_ChangeRate);
            if (solubiltyofNaCl >= 40)
            {
                solubiltyofNaCl = 30 + ((Temperature - initialTemperature) * Nacl_ChangeRate);
            }

            solubiltyofBaCO3 = 25 - ((Temperature - initialTemperature) * BaCO3_ChangeRate);
            solubiltyofKNO3 = 25 + ((Temperature - initialTemperature) * KNO3_ChangeRate);
        }
        private void DisolveSugar()
        {
            if (SugarDisolved <= solubiltyofSugar - 5)
            {
                if (SugarEntered.Count != 0)
                {

                    GameObject temp = SugarEntered[0];
                    SugarEntered.RemoveAt(0);
                    //Disolve(temp);
                    StartCoroutine("Disolve", temp);
                    // Invoke("Disolve", 1f);
                    SugarDisolved += 5;
                }
                else
                {
                    if (C9_Solutions_6_11.LabManager.LabManager_Instance.GetStepsComppleted() == 2)
                        C9_Solutions_6_11.LabManager.LabManager_Instance.StepCompleted();


                    if (C9_Solutions_6_11.LabManager.LabManager_Instance.GetStepsComppleted() == 4 && flag == true)
                        C9_Solutions_6_11.LabManager.LabManager_Instance.StepCompleted();
                }
                flag = false;
            }
            else
            {
                flag = true;
                if (SugarEntered.Count != 0)
                {
                    if (C9_Solutions_6_11.LabManager.LabManager_Instance.GetStepsComppleted() == 1)
                        C9_Solutions_6_11.LabManager.LabManager_Instance.StepCompleted();
                }

            }
        }

        IEnumerator Disolve(GameObject solute)
        {
            yield return waitfor1second;
            ParticleSystem particle = solute.GetComponentInChildren<ParticleSystem>();
            if(particle != null)
            particle.Play();
            if(solute.CompareTag("BaCO3"))
            {
                yield return waitfor1second;
                solute.SetActive(false);

            }
            else
            {
                Destroy(solute, .5f);
            }
        

        }
        private void DisolveNaCl()
        {
            if (NaClDisolved <= solubiltyofNaCl - 5)
            {
                if (NaClEntered.Count != 0)
                {

                    GameObject temp = NaClEntered[0];
                    NaClEntered.RemoveAt(0);
                    StartCoroutine("Disolve", temp);
                    NaClDisolved += 5;
                }
            }
            else
            {
                NaclStoppedDisolving = true;
            }

        }
        private void DisolveBaCO3()
        {
            if (BaCO3Disolved <= solubiltyofBaCO3 - 5)
            {
                if (BaCO3Entered.Count != BaCO3Active)
                {

                    GameObject temp = BaCO3Entered[BaCO3Active];
                    BaCO3Active++;
                    StartCoroutine("Disolve", temp);
                    BaCO3Disolved += 5;
                }

            }
            else if(BaCO3Disolved >= solubiltyofBaCO3+5)
            {
                if (BaCO3Active !=0)
                {

                    GameObject temp = BaCO3Entered[BaCO3Active];
                    BaCO3Active--;
                    //BaCO3Entered.RemoveAt(0);
                    temp.SetActive(true);
                    //Destroy(temp, 1.5f); ;
                    BaCO3Disolved -= 5;
                    if (C9_Solutions_6_11.LabManager.LabManager_Instance.GetStepsComppleted() == 2)
                        C9_Solutions_6_11.LabManager.LabManager_Instance.StepCompleted();

                }
            }
            else
            {
                BaCO3StoppedDisolving = true;
            }
        }
        private void DisolveKNO3()
        {
            if (KNO3Disolved <= solubiltyofKNO3 - 5)
            {
                if (KNO3Entered.Count != 0)
                {

                    GameObject temp = KNO3Entered[0];
                    KNO3Entered.RemoveAt(0);
                    StartCoroutine("Disolve", temp);
                    KNO3Disolved += 5;
                }
            }
            else
            {
                KnO3StoppedDisolving = true;
            }

        }
        private void Update()
        {
            if(SugarPracticle)
            {
                DisolveSugar();
            }
            else
            {
                DisolveKNO3();
                DisolveBaCO3();
                DisolveNaCl();
                if (KnO3StoppedDisolving && BaCO3StoppedDisolving && NaclStoppedDisolving)
                {
                    if (NaClEntered.Count != 0 && KNO3Entered.Count != 0 && BaCO3Entered.Count != BaCO3Active)
                    {
                       
                        if (C9_Solutions_6_11.LabManager.LabManager_Instance.GetStepsComppleted() == 1)
                            C9_Solutions_6_11.LabManager.LabManager_Instance.StepCompleted();
                    }

                }
            }

            if (Temperature >= 85f)
            {
                if (SmookePlaying == false)
                {
                    smoke.Play();
                    bubbleEffect.Play();
                    SmookePlaying = true;
                }
                   
            }
           
        }
        private void FixedUpdate()
        {
            if (flame != null)
            {
                if (flame.IsFlameBurning())
                {
                    if (Temperature != MaxTemperature)
                    {
                        Temperature += 0.02f;
                        Thermometer.CurrentTemp = Temperature;
                    }
                }
                ChangeSolubility();
            }
          

         }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Outline>() == null)
            {
                if (other.gameObject.CompareTag("Sugar"))
                {
                    waterEffect.transform.position=other.gameObject.transform.position;
                    waterEffect.Play();
                 
                    SugarEntered.Add(other.gameObject);
                    SugarPracticle = true;
                }
                if (other.gameObject.CompareTag("NaCl"))
                {
                    waterEffect.transform.position = other.gameObject.transform.position;
                    waterEffect.Play();
                    NaClEntered.Add(other.gameObject);
                }
                if (other.gameObject.CompareTag("BaCO3"))
                {
                    waterEffect.transform.position = other.gameObject.transform.position;
                    waterEffect.Play();
                    BaCO3Entered.Add(other.gameObject);
                }
                if (other.gameObject.CompareTag("KNO3"))
                {
                    waterEffect.transform.position = other.gameObject.transform.position;
                    waterEffect.Play();
                    KNO3Entered.Add(other.gameObject);
                }
            }

        }
        
    }
}