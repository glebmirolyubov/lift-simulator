using UnityEngine;

public class FloorFactory : MonoBehaviour
{
    protected static FloorFactory instance;

    public FloorManager floorManager;
    public GameObject floor;

    void Start()
    {
        instance = this;
    }

    public void InstantiateFloors(int numberOfFloors)
    {
        for (int i = 1; i <= numberOfFloors; i++)
        {
            CreateFloor(i, numberOfFloors, gameObject);
        }
        floorManager.GetReferenceToAllFloors();
        floorManager.ConfigureCurrentFloor(1);
    }

    public static Floor CreateFloor(int floorNumber, int numberOfFloors, GameObject parent)
    {
        Floor floor = Instantiate(instance.floor, Vector3.zero, Quaternion.identity).GetComponent<Floor>();
        //floor.gameObject.transform.parent = parent.transform;

        floor.InitializeFloor(floorNumber);

        floor.gameObject.transform.SetParent(parent.transform, false);

        return floor;
    }
}
