using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Floor : MonoBehaviour
{
    public int floorNumber;

    LiftManager liftManager;
    Button upButton;
    Button downButton;

    public void InitializeFloor(int floorNumber, bool isLastFloor)
    {
        this.floorNumber = floorNumber;
        liftManager = GameObject.FindWithTag("Lift Manager").GetComponent<LiftManager>();
        transform.GetChild(0).GetComponent<TextMeshPro>().SetText("Floor " + this.floorNumber);

        if (floorNumber == 1)
        {
            Destroy(transform.GetChild(2).gameObject);
            upButton = transform.GetChild(1).gameObject.GetComponent<Button>();
            upButton.onClick.AddListener(UpButtonPressed);
        }
        else if (isLastFloor)
        {
            Destroy(transform.GetChild(1).gameObject);
            downButton = transform.GetChild(2).gameObject.GetComponent<Button>();
            downButton.onClick.AddListener(DownButtonPressed);
        }
        else
        {
            upButton = transform.GetChild(1).gameObject.GetComponent<Button>();
            downButton = transform.GetChild(2).gameObject.GetComponent<Button>();
            upButton.onClick.AddListener(UpButtonPressed);
            downButton.onClick.AddListener(DownButtonPressed);
        }
    }

    public void UpButtonPressed()
    {
        upButton.GetComponent<Image>().color = Color.yellow;
        upButton.interactable = false;
        liftManager.ScheduleFloorUpButtonRequest(this);
    }

    public void DownButtonPressed()
    {
        downButton.GetComponent<Image>().color = Color.yellow;
        downButton.interactable = false;
        liftManager.ScheduleFloorDownButtonRequest(this);
    }

    public void ResetUpButton()
    {
        upButton.GetComponent<Image>().color = Color.white;
        upButton.interactable = true;
    }

    public void ResetDownButton()
    {
        downButton.GetComponent<Image>().color = Color.white;
        downButton.interactable = true;
    }
        
}