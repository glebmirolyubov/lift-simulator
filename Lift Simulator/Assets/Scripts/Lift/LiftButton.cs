using UnityEngine;
using UnityEngine.UI;

public class LiftButton : MonoBehaviour, IInitializable<int>, IPressable, IResettable
{
    LiftManager liftManager;
    public int floorNumber;

    Button liftButton;

    public void InitializeButton (int floorNumber)
    {
        this.floorNumber = floorNumber;
        liftManager = GameObject.FindWithTag("Lift Manager").GetComponent<LiftManager>();
        gameObject.transform.GetChild(0).GetComponent<Text>().text = floorNumber.ToString();
        liftButton = GetComponent<Button>();
        liftButton.onClick.AddListener(ButtonPressed);
    }

    public void ButtonPressed()
    {
        liftButton.GetComponent<Image>().color = Color.yellow;
        liftButton.interactable = false;
        liftManager.AddFloorToQueue(this);
    }

    public void ResetButton()
    {
        liftButton.GetComponent<Image>().color = Color.white;
        liftButton.interactable = true;
    }
}