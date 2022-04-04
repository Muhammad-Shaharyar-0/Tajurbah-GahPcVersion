using UnityEngine;
using UnityEngine.UI;

public class HorizontalRodHandler : MonoBehaviour
{
    [SerializeField] Button RodMovableBtn;

    int RodMovementCounter;

    void Start()
    {
        RodMovementCounter = 1;
    }

    public void OnClickHorizontalRodMovableBtn()
    {
        gameObject.GetComponent<MovementController>().EnableMovement();

        DisableHorizontalRodBtn();

        if (RodMovementCounter == 1)
        {
            //move down
            RodMovementCounter = 2;
        }
        else
        {
            //move up
            RodMovementCounter = 1;
        }
    }

    public void EnableHorizontalRodBtn()
    {
        RodMovableBtn.interactable = true;
    }

    public void DisableHorizontalRodBtn()
    {
        RodMovableBtn.interactable = false; 
    }
}
