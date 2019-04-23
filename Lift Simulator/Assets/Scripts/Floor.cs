using UnityEngine;
using TMPro;

public class Floor : MonoBehaviour
{
    public int floorNumber;

    GameObject upButton;
    GameObject downButton;

    public void InitializeFloor(int floorNumber)
    {
        this.floorNumber = floorNumber;
        transform.GetChild(0).GetComponent<TextMeshPro>().SetText("Floor " + this.floorNumber);
    }

    public void UpButtonPressed()
    {

    }

    public void DownButtonPressed()
    {

    }
        
}
