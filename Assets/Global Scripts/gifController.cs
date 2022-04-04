using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class gifController : MonoBehaviour
    {
        Image gif;
        [SerializeField] float gifTime=0.1f;
        [SerializeField] Sprite[] gifSprites;

        private void Start()
        {
            gif = GetComponent<Image>();
            StartCoroutine("GifRoutine");
        }

        IEnumerator GifRoutine()
        {
            int i = 0;
            while(true)
            {
                gif.sprite = gifSprites[i];
                i++;

                if(i==gifSprites.Length)
                {
                    i = 0;
                }

                yield return new WaitForSeconds(gifTime);
            }
        }
    }
