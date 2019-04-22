using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public Dropdown floorDropdown;
    public LiftButtonFactory liftButtonFactory;

    List<string> floorOptions = new List<string> {};

    public void SetNumberOfFloors(int numberOfFloors)
    {
        floorDropdown.ClearOptions();
        
        for (int i = 1; i <= numberOfFloors; i++)
        {
            floorOptions.Add("Floor " + i);
        }

        floorDropdown.AddOptions(floorOptions);

        liftButtonFactory.InstantiateLiftButtons(numberOfFloors);
    }
}
