using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySimpleLiquid;

namespace C9_Solutions_6_7
{
    public class beakerController : MonoBehaviour
    {
        public LiquidContainer liquidContainerObject;
        public Color copperSulphateSolutionColor;
        int flag = 0;

        public GameObject copperSulphateChildObject;

        // Start is called before the first frame update
        void Start()
        {
            copperSulphateChildObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (flag == 0 && liquidContainerObject.FillAmountPercent > 0.6f)
            {
                flag = 1;

                GetComponent<objectController>().ChangeLayerToDragable();
                GetComponent<objectController>().MakeNonkinematic();

                LabManager.LabManager_Instance.EnableOtherBeakerPlaceholder();

            }
        }

        void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("CopperSulphateParticles"))
            {
                GetComponent<objectController>().EnableMeshCollider();
                copperSulphateChildObject.SetActive(true);
            }
        }

        int count = 0;

        public void MixingOfCopperSulphate()
        {
            liquidContainerObject.LiquidColor = Color.Lerp(liquidContainerObject.LiquidColor, copperSulphateSolutionColor, 0.1f);

            for(int i=count;i< copperSulphateChildObject.transform.childCount;i++)
            {
                copperSulphateChildObject.transform.GetChild(i).gameObject.SetActive(false);

                if(i%5==0)
                {
                    count = i;
                    count++;
                    break;
                }
            }
            
        }
    }
}
