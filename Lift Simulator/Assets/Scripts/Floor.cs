using UnityEngine;
using TMPro;

public class Floor : MonoBehaviour
{
    public int floorNumber;

    GameObject upButton;
    GameObject downButton;

    public void InitializeFloor(int floorNumber, bool isLastFloor)
    {
        this.floorNumber = floorNumber;
        transform.GetChild(0).GetComponent<TextMeshPro>().SetText("Floor " + this.floorNumber);

        if (floorNumber == 1)
        {
            Destroy(transform.GetChild(2).gameObject);
        }
        else if (isLastFloor)
        {
            Destroy(transform.GetChild(1).gameObject);
        }
    }

    public void UpButtonPressed()
    {

    }

    public void DownButtonPressed()
    {

    }
        
}