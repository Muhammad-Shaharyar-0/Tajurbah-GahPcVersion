using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_1
{
    public class experimentsPerformedController : MonoBehaviour
    {

        public Button UsOfWoodWoodButton;
        public Button UsOfWoodGlassButton;
        public Button UsOfGlassRubberButton;

        public Text UsOfWoodWoodText;
        public Text UsOfWoodGlassText;
        public Text UsOfGlassRubberText;

        // Start is called before the first frame update
        void Start()
        {
            UsOfWoodWoodButton.onClick.AddListener(WoodAndWood);
            UsOfWoodGlassButton.onClick.AddListener(WoodAndGlass);
            UsOfGlassRubberButton.onClick.AddListener(GlassAndRubber);
        }

        private void OnEnable()
        {
            if (PlayerPrefs.HasKey("wwAverageUS"))
            {
                UsOfWoodWoodButton.gameObject.SetActive(false);
                UsOfWoodWoodText.text = PlayerPrefs.GetString("wwAverageUS");
            }

            if (PlayerPrefs.HasKey("wgAverageUS"))
            {
                UsOfWoodGlassButton.gameObject.SetActive(false);
                UsOfWoodGlassText.text = PlayerPrefs.GetString("wgAverageUS");
            }

            if (PlayerPrefs.HasKey("grAverageUS"))
            {
                UsOfGlassRubberButton.gameObject.SetActive(false);
                UsOfGlassRubberText.text = PlayerPrefs.GetString("grAverageUS");
            }
        }

        void WoodAndWood()
        {
            managerController.Instance.ChoosenExperiment("w", "w");
            this.gameObject.SetActive(false);
        }
        void WoodAndGlass()
        {
            managerController.Instance.ChoosenExperiment("w", "g");
            this.gameObject.SetActive(false);
        }
        void GlassAndRubber()
        {
            managerController.Instance.ChoosenExperiment("g", "r");
            this.gameObject.SetActive(false);
        }
    }
}