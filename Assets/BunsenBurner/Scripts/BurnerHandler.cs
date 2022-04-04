using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BurnerHandler : MonoBehaviour
{
    [SerializeField] Button BurnerOnOffBtn;
 
    int BurnerCounter;

    void Start()
    {
       
        BurnerCounter = 1;
      
        ChangeButtonColorAndTextInChild(BurnerOnOffBtn, Color.red, "ON");

        
    }

    public void OnClickBurnerOnOffBtn()
    {
        if (BurnerCounter == 1)
        {
         
            ChangeButtonColorAndTextInChild(BurnerOnOffBtn, Color.green, "OFF");

            gameObject.GetComponent<DisplayFire>().PlayFire();

            BurnerCounter = 2;

            
        }
        else
        {
            //Turn Off flame


            ChangeButtonColorAndTextInChild(BurnerOnOffBtn, Color.red, "ON");

            gameObject.GetComponent<DisplayFire>().StopFire();

            BurnerCounter = 1;


        }
    }

    public void EnableBurnerOnOffBtn()
    {
        BurnerOnOffBtn.GetComponent<Button>().interactable = true;
    }

    public void DisableBurnerOnOffBtn()
    {
        BurnerOnOffBtn.GetComponent<Button>().interactable = false ;
    }

    void ChangeButtonColorAndTextInChild(Button btn, Color clr, string Txt)
    {
        btn.GetComponent<Image>().color = clr;
        btn.GetComponentInChildren<TextMeshProUGUI>().text = Txt;
    }

}
