using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace P9_Dynamics_3_3
{
    public class TMP_ScaleLabel2 : MonoBehaviour
    {
        public enum objectType { TextMeshPro = 0, TextMeshProUGUI = 1 };

        public objectType ObjectType;
        public bool isStatic;

        public float fontSize;
        public string displayText;
        public Vector3 Rotation, Position;
        public TMP_FontAsset textFont;
        public Material textFontMaterial;
        public Color textFontColor;
        public TextAlignmentOptions textAlignment;

        [SerializeField] Transform PosRef;
        public float yOffSet;

        private TMP_Text m_text;

        //private TMP_InputField m_inputfield;


        //private const string k_label = "The count is <#0080ff>{0}</color>";
        private int count;

        void Awake()
        {
            // Get a reference to the TMP text component if one already exists otherwise add one.
            // This example show the convenience of having both TMP components derive from TMP_Text. 
            if (ObjectType == 0)
                m_text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
            else
                m_text = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();

            // Load a new font asset and assign it to the text object.
            // m_text.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Anton SDF");
            m_text.font = textFont;

            // Load a new material preset which was created with the context menu duplicate.
            // m_text.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/Anton SDF - Drop Shadow");
            if (null != textFontMaterial)
            {
                m_text.fontSharedMaterial = textFontMaterial;
            }

            // Set the size of the font.
            m_text.fontSize = this.fontSize;

            m_text.alignment = textAlignment;
            m_text.color = this.textFontColor;

            // Set the text
            m_text.text = displayText;

            // Get the preferred width and height based on the supplied width and height as opposed to the actual size of the current text container.
            Vector2 size = m_text.GetPreferredValues(Mathf.Infinity, Mathf.Infinity);

            // Set the size of the RectTransform based on the new calculated values.
            m_text.rectTransform.sizeDelta = new Vector2(size.x, size.y);
            // m_text.rectTransform.Rotate(this.Rotation);
            // Debug.Log(this.Position);
            //Transform scaleObject = transform.parent;
            if (textAlignment == TextAlignmentOptions.CaplineRight)
            {
                this.Position.y = PosRef.position.y + (-1 * yOffSet);
            }
            else
            {
                this.Position.y = PosRef.position.y + yOffSet;
            }
            m_text.rectTransform.SetPositionAndRotation(this.Position, Quaternion.Euler(this.Rotation));
        }


        void Update()
        {
            // if (!isStatic)
            // {
            //     m_text.SetText(k_label, count % 1000);
            //     count += 1;
            // }
        }

    }
}