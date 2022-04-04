using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Tajurbah_Gah
{
    public class FlameController : MonoBehaviour
    {
        public ParticleSystem flameParticles;
        public Light flamePointLight;

        [SerializeField]
        float flashSpeedMultiplier = 1f;
        [SerializeField]
        float flashFlameSizeMultiplier = 1.5f;
        [SerializeField]
        float flashLightIntensityMultiplier = 3f;


        private float originalFlameSize;
        private float originalLightIntensity;

        private ParticleSystem.MainModule flameParticlesMain;
        [SerializeField]
        bool isFlameBurning = false;
        string m_SceneName;
        private void Awake()
        {
            StopFlame();
        }

        private void Start()
        {
            flameParticlesMain = flameParticles.main;
            originalFlameSize = flameParticles.main.startSize.constant;
            originalLightIntensity = flamePointLight.intensity;
            m_SceneName = SceneManager.GetActiveScene().name;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                FlashFlame(1);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                FlashFlame(2);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                FlashFlame(3);
            }
        }

        public void FlashFlame(int Level=1)
        {
            switch (Level)
                {
                case 1:
                    flashFlameSizeMultiplier /=1.5f;
                    flashLightIntensityMultiplier /=1.5f;
                    break;
                case 2:
                    flashFlameSizeMultiplier /= 1.25f;
                    flashLightIntensityMultiplier /= 1.25f;
                    break;
                case 3:
                    break;
                default:
                    break;
            }

            StartCoroutine("FlashFlameRoutine");
        }

        IEnumerator FlashFlameRoutine()
        {
            float sizeIncrement = 0.01f;

            while(flameParticles.main.startSize.constant<= (flashFlameSizeMultiplier*originalFlameSize))
            {
                sizeIncrement = flameParticles.main.startSize.constant;
                sizeIncrement += 0.01f;
                flameParticlesMain.startSize =new ParticleSystem.MinMaxCurve(sizeIncrement);
                flamePointLight.intensity += sizeIncrement * flashLightIntensityMultiplier;

                yield return new WaitForSeconds(0.01f/ flashSpeedMultiplier);
            }

            yield return new WaitForSeconds(0.1f/ flashSpeedMultiplier);

            while (flameParticles.main.startSize.constant >= originalFlameSize)
            {
                sizeIncrement = flameParticles.main.startSize.constant;
                sizeIncrement -= 0.01f;
                flameParticlesMain.startSize = new ParticleSystem.MinMaxCurve(sizeIncrement);
                flamePointLight.intensity -= sizeIncrement * flashLightIntensityMultiplier;

                yield return new WaitForSeconds(0.01f/ flashSpeedMultiplier);
            }
            flamePointLight.intensity = originalLightIntensity;
        }

        public void StartFlame()
        {
            SoundsManager.Instance.PlayBurnerSound();
            isFlameBurning = true;
            flameParticles.Play();
            flamePointLight.enabled = true;
        }
        public void StopFlame()
        {
            SoundsManager.Instance.StopBurnerSound();
            isFlameBurning = false;
            flameParticles.Stop();
            flamePointLight.enabled = false;
            
            if (C9_Solutions_6_11.LabManager.LabManager_Instance.GetStepsComppleted() == 3 && m_SceneName=="Sugar")
                C9_Solutions_6_11.LabManager.LabManager_Instance.StepCompleted();
        }

        public bool IsFlameBurning()
        {
            return isFlameBurning;
        }
    }
}
