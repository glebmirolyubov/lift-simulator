using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameObject startCanvas;
    public Dropdown floorDropdown;
    public LiftButtonFactory liftButtonFactory;
    public FloorFactory floorFactory;

    List<string> floorOptions = new List<string> {};

    void Start()
    {
        startCanvas.SetActive(true);
    }

    public void SetNumberOfFloors(int numberOfFloors)
    {
        floorDropdown.ClearOptions();
        
        for (int i = 1; i <= numberOfFloors; i++)
        {
            floorOptions.Add(i.ToString());
        }

        floorDropdown.AddOptions(floorOptions);

        liftButtonFactory.InstantiateLiftButtons(numberOfFloors);
        floorFactory.InstantiateFloors(numberOfFloors);
    }
}