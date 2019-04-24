using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiftManager : MonoBehaviour
{
    public Animation liftDoorsAnimation;
    public TextMeshPro liftIndicatorText;
    public int currentLiftFloor;

    LiftButton floorToMove;
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
        //CheckIfMoreFloorsAreQueued();
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
                StopLift(currentFloor);
            }
            
        }
        else if (currentLiftFloor > liftButton.floorNumber)
        {
            currentLiftFloor--;
            liftIndicatorText.text = currentLiftFloor.ToString();

            if (queueFloorsList.Find(x => x.floorNumber == currentLiftFloor))
            {
                LiftButton currentFloor = queueFloorsList.Find(x => x.floorNumber == currentLiftFloor);
                StopLift(currentFloor);
            }
        }
        else if (currentLiftFloor == floorToMove.floorNumber)
        {
            StopLift(liftButton);
        }
    }

    public void StopLift(LiftButton liftButton)
    {
        liftIndicatorText.text = currentLiftFloor.ToString();
        queueFloorsList.Remove(liftButton);
        liftButton.ResetLiftButton();
        liftIsMoving = false;

        if (liftButton.floorNumber == floorToMove.floorNumber)
        {
            //liftReachedDestination = true;
            queueFloorsList.Remove(floorToMove);
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
            floorToMove = queueFloorsList[0];
            //StartCoroutine("MoveLiftToFloor");
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
            floorToMove = liftButton;
            StartCoroutine("MoveLiftToFloor");
        }
    }
}