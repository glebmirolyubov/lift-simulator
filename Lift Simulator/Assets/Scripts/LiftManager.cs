using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiftManager : MonoBehaviour
{
    public TextMeshPro liftIndicatorText;
    public int currentLiftFloor;

    LiftButton liftButton;
    bool liftIsMoving;
    bool liftReachedDestination;

    Queue <LiftButton> liftQueue = new Queue <LiftButton>();

    void Start()
    {
        currentLiftFloor = 1;
        liftIsMoving = false;
        liftReachedDestination = false;
    }

    IEnumerator MoveLiftToFloor()
    {
        liftIsMoving = true;
        liftButton = liftQueue.Dequeue();

        while (!liftReachedDestination)
        {
            yield return new WaitForSeconds(1f);

            MoveLiftOneFloor(liftButton);
        }
        CheckIfMoreFloorsAreQueued();
    }

    public void MoveLiftOneFloor(LiftButton liftButton)
    {
        if (currentLiftFloor < liftButton.floorNumber)
        {
            currentLiftFloor++;
            liftIndicatorText.text = currentLiftFloor.ToString();
            
        }
        else if (currentLiftFloor > liftButton.floorNumber)
        {
            currentLiftFloor--;
            liftIndicatorText.text = currentLiftFloor.ToString();
        }
        else
        {
            StopLiftAndOpenDoor(liftButton);
        }
    }

    public void StopLiftAndOpenDoor(LiftButton liftButton)
    {
        currentLiftFloor = liftButton.floorNumber;
        liftIndicatorText.text = currentLiftFloor.ToString();
        liftButton.ResetLiftButton();
        liftIsMoving = false;
        liftReachedDestination = true;
    }

    public void CheckIfMoreFloorsAreQueued()
    {
        if (liftQueue.Count > 0)
        {
            liftReachedDestination = false;
            StartCoroutine("MoveLiftToFloor");
        }
    }

    public void AddFloorDestinationToQueue(LiftButton liftButton)
    {
        liftQueue.Enqueue(liftButton);

        if (!liftIsMoving)
        {
            liftReachedDestination = false;
            StartCoroutine("MoveLiftToFloor");
        }
    }
}