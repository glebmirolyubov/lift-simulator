using UnityEngine;

public class LiftButtonFactory : MonoBehaviour
{
    protected static LiftButtonFactory instance;

    public GameObject liftButton;

    void Start()
    {
        instance = this;
    }

    public void InstantiateLiftButtons(int numberOfFloors)
    {
        for (int i = 1; i <= numberOfFloors; i++)
        {
            CreateLiftButton(i, gameObject);
        }
    }

    public static LiftButton CreateLiftButton (int liftButtonNumber, GameObject parent)
    {
        LiftButton liftButton = Instantiate(instance.liftButton, Vector3.zero, Quaternion.identity).GetComponent<LiftButton>();
        liftButton.InitializeLiftButton(liftButtonNumber);
        liftButton.gameObject.transform.SetParent(parent.transform, false);

        return liftButton;
    }

}
