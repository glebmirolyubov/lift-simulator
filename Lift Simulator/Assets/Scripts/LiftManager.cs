using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiftManager : MonoBehaviour
{
    public enum MovementDirection {Up, Down};
    public TextMeshPro liftIndicatorText;
    public int currentLiftFloor;

    MovementDirection currentLiftMovementDirection;
    LiftButton floorToMove;
    bool liftIsMoving;
    bool liftReachedDestination;

    List<LiftButton> queueFloorsList = new List<LiftButton>();
    Queue <LiftButton> liftQueue = new Queue <LiftButton>();

    void Start()
    {
        currentLiftFloor = 1;
        liftIsMoving = false;
    }

    IEnumerator MoveLiftToFloor()
    {
        liftIsMoving = true;

        while (!liftReachedDestination)
        {
            yield return new WaitForSeconds(1f);

            MoveLiftOneFloor(floorToMove);
        }
        CheckIfMoreFloorsAreQueued();
    }

    public void MoveLiftOneFloor(LiftButton liftButton)
    {
        if (currentLiftFloor < liftButton.floorNumber)
        {
            currentLiftFloor++;
            liftIndicatorText.text = currentLiftFloor.ToString();

            if (queueFloorsList.Find(x => x.floorNumber == currentLiftFloor))
            {
                LiftButton currentFloor = queueFloorsList.Find(x => x.floorNumber == currentLiftFloor);
                StopLiftAndOpenDoor(currentFloor);
            }
            
        }
        else if (currentLiftFloor > liftButton.floorNumber)
        {
            currentLiftFloor--;
            liftIndicatorText.text = currentLiftFloor.ToString();

            if (queueFloorsList.Find(x => x.floorNumber == currentLiftFloor))
            {
                LiftButton currentFloor = queueFloorsList.Find(x => x.floorNumber == currentLiftFloor);
                StopLiftAndOpenDoor(currentFloor);
            }
        }
        else if (currentLiftFloor == floorToMove.floorNumber)
        {
            StopLiftAndOpenDoor(liftButton);
        }
    }

    public void StopLiftAndOpenDoor(LiftButton liftButton)
    {
        Debug.Log(queueFloorsList.Count);
        liftIndicatorText.text = currentLiftFloor.ToString();
        queueFloorsList.Remove(liftButton);
        liftButton.ResetLiftButton();

        if (liftButton.floorNumber == floorToMove.floorNumber)
        {
            liftReachedDestination = true;
            liftIsMoving = false;
            queueFloorsList.Remove(floorToMove);
            Debug.Log(queueFloorsList.Count);
        } 
    }

    public void CheckIfMoreFloorsAreQueued()
    {
        if (queueFloorsList.Count > 0)
        {
            liftReachedDestination = false;
          //  floorToMove = queueFloorsList[0];
            StartCoroutine("MoveLiftToFloor");
            Debug.Log("Floor we should move to: " + floorToMove.floorNumber);
        }
    }

    public void AddFloorToQueue(LiftButton liftButton)
    {
        queueFloorsList.Add(liftButton);

        if (!liftIsMoving)
        {
            currentLiftMovementDirection = MovementDirection.Up;
            floorToMove = liftButton;
            liftReachedDestination = false;
            StartCoroutine("MoveLiftToFloor");
        }
    }
}