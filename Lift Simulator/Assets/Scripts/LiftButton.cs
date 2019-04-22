using UnityEngine;
using UnityEngine.UI;

public class LiftButton : MonoBehaviour, IPressable, IInitializable<int>
{
    public void InitializeLiftButton (int floorNumber)
    {
        gameObject.transform.GetChild(0).GetComponent<Text>().text = floorNumber.ToString();
    }

    public void LiftButtonPressed()
    {

    }
}
