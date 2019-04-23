using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FloorManager : MonoBehaviour
{
    public GameObject leftWall;
    public Dropdown floorsDropdown;

    [SerializeField] List<Floor> floorsList = new List<Floor>();

    public void ConfigureCurrentFloor(int currentFloorNumber)
    {
        foreach (Floor currentFloor in floorsList)
        {
            if (currentFloor.floorNumber == currentFloorNumber)
            {
                currentFloor.gameObject.SetActive(true);
            }
            else
            {
                currentFloor.gameObject.SetActive(false);
            }
        }
    }

    public void GetReferenceToAllFloors()
    {
        foreach (Transform child in leftWall.transform)
        {
            floorsList.Add(child.GetComponent<Floor>());
        }

        floorsDropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(floorsDropdown);
        });
    }

    void DropdownValueChanged(Dropdown change)
    {
        ConfigureCurrentFloor(change.value + 1);
    }
}