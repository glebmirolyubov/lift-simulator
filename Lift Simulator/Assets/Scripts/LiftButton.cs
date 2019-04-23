using UnityEngine;
using UnityEngine.UI;

public class LiftButton : MonoBehaviour, IPressable, IInitializable<int>
{
    LiftManager liftManager;
    public int floorNumber;

    Button liftButton;

    public void InitializeLiftButton (int floorNumber)
    {
        this.floorNumber = floorNumber;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = floorNumber.ToString();
        liftButton = GetComponent<Button>();
        liftManager = GameObject.FindWithTag("Lift Manager").GetComponent<LiftManager>();
        liftButton.onClick.AddListener(LiftButtonPressed);
    }

    public void LiftButtonPressed()
    {
        liftButton.GetComponent<Image>().color = Color.yellow;
        liftManager.AddFloorToQueue(this);
    }

    public void ResetLiftButton()
    {
        liftButton.GetComponent<Image>().color = Color.white;
    }
}