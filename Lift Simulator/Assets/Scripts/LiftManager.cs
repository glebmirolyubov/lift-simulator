using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiftManager : MonoBehaviour
{
    public Animation liftDoorsAnimation;
    public TextMeshPro liftIndicatorText;
    public int currentLiftFloor;

    int floorToMove;
    LiftButton mainLiftButtonPressed;
    Floor floorUpButton;
    Floor floorDownButton;
    public bool liftIsMoving;
    bool liftReachedDestination;

    List<LiftButton> queueFloorsList = new List<LiftButton>();
    Queue <LiftButton> liftQueue = new Queue <LiftButton>();

    void Start()
    {
        currentLiftFloor = 1;
        liftIsMoving = false;
        liftReachedDestination = true;
    }

    IEnumerator MoveLiftToFloor()
    {
        liftIsMoving = true;

        while (!liftReachedDestination)
        {
            yield return new WaitForSeconds(1f);

            if (liftIsMoving)
            {
                MoveLiftOneFloor(floorToMove);
            }
        }
    }

    public void MoveLiftOneFloor(int floorToMove)
    {
        if (currentLiftFloor < floorToMove)
        {
            currentLiftFloor++;
            liftIndicatorText.text = currentLiftFloor.ToString();

            if (queueFloorsList.Find(x => x.floorNumber == currentLiftFloor))
            {
                StopLift(currentLiftFloor);
            }
            
        }
        else if (currentLiftFloor > floorToMove)
        {
            currentLiftFloor--;
            liftIndicatorText.text = currentLiftFloor.ToString();

            if (queueFloorsList.Find(x => x.floorNumber == currentLiftFloor))
            {
                LiftButton currentFloor = queueFloorsList.Find(x => x.floorNumber == currentLiftFloor);
                StopLift(currentLiftFloor);
            }
        }
        else if (currentLiftFloor == floorToMove)
        {
            StopLift(currentLiftFloor);
        }
    }

    public void StopLift(int currentLiftFloor)
    {
        liftIndicatorText.text = this.currentLiftFloor.ToString();
        LiftButton currentFloor = queueFloorsList.Find(x => x.floorNumber == currentLiftFloor);
        currentFloor.ResetButton();
        queueFloorsList.Remove(currentFloor);

        //if (floorUpButton.floorNumber == currentLiftFloor)
        //{
        //    floorUpButton.ResetUpButton();
        //}
        //if (floorDownButton.floorNumber == currentLiftFloor)
        //{
        //    floorDownButton.ResetDownButton();
        //}

        liftIsMoving = false;

        if (currentLiftFloor == floorToMove)
        {
            queueFloorsList.Remove(mainLiftButtonPressed);
            CheckIfMoreFloorsAreQueued();
        }

        OpenLiftDoors();
    }

    public void OpenLiftDoors()
    {
        liftDoorsAnimation.Play();
    }

    public void DetermineNextBehaviourAfterLiftDoorsClosed()
    {
        if (queueFloorsList.Count > 0)
        {
            liftIsMoving = true;
        }
        else
        {
            liftIsMoving = false;
        }
    }

    public void CheckIfMoreFloorsAreQueued()
    {
        if (queueFloorsList.Count > 0)
        {
            liftReachedDestination = false;
            mainLiftButtonPressed = queueFloorsList[0];
            floorToMove = queueFloorsList[0].floorNumber;
        } 
        else
        {
            liftReachedDestination = true;
        }
    }

    public void AddFloorToQueue(LiftButton liftButton)
    {
        queueFloorsList.Add(liftButton);

        if (!liftIsMoving && liftReachedDestination)
        {
            liftReachedDestination = false;
            mainLiftButtonPressed = liftButton;
            floorToMove = liftButton.floorNumber;
            StartCoroutine("MoveLiftToFloor");
        }
    }

    public void ScheduleFloorUpButtonRequest(Floor floor)
    {
        floorUpButton = floor;
        if (!liftIsMoving && liftReachedDestination)
        {
            liftReachedDestination = false;
            floorToMove = floor.floorNumber;
            StartCoroutine("MoveLiftToFloor");
        }
    }

    public void ScheduleFloorDownButtonRequest(Floor floor)
    {
        floorDownButton = floor;
        if (!liftIsMoving && liftReachedDestination)
        {
            liftReachedDestination = false;
            floorToMove = floor.floorNumber;
            StartCoroutine("MoveLiftToFloor");
        }
    }
}