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

    List<LiftButton> queueForLiftButtons = new List<LiftButton>();
    List<Floor> queueForFloorUpButtons = new List<Floor>();
    List<Floor> queueForFloorDownButtons = new List<Floor>();

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

            if (queueForLiftButtons.Find(x => x.floorNumber == currentLiftFloor))
            {
                StopLift(currentLiftFloor);
            }

            if (queueForFloorUpButtons.Find(x => x.floorNumber == currentLiftFloor))
            {
                StopLift(currentLiftFloor);
            }
            
        }
        else if (currentLiftFloor > floorToMove)
        {
            currentLiftFloor--;
            liftIndicatorText.text = currentLiftFloor.ToString();

            if (queueForLiftButtons.Find(x => x.floorNumber == currentLiftFloor))
            {
                StopLift(currentLiftFloor);
            }

            if (queueForFloorDownButtons.Find(x => x.floorNumber == currentLiftFloor))
            {
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
        if (mainLiftButtonPressed != null)
        {
            if (queueForLiftButtons.Find(x => x.floorNumber == currentLiftFloor))
            {
                LiftButton currentFloor = queueForLiftButtons.Find(x => x.floorNumber == currentLiftFloor);
                currentFloor.ResetButton();
                queueForLiftButtons.Remove(currentFloor);
            }
        }

        if (floorUpButton != null)
        {
            if (floorUpButton.floorNumber == currentLiftFloor)
            {
                Floor currentFloorUpButton = queueForFloorUpButtons.Find(x => x.floorNumber == currentLiftFloor);
                currentFloorUpButton.ResetUpButton();
                queueForFloorUpButtons.Remove(currentFloorUpButton);
            }
        }

        if (floorDownButton != null)
        {
            if (floorDownButton.floorNumber == currentLiftFloor)
            {
                Floor currentFloorDownButton = queueForFloorDownButtons.Find(x => x.floorNumber == currentLiftFloor);
                currentFloorDownButton.ResetUpButton();
                queueForFloorDownButtons.Remove(currentFloorDownButton);
            }
        }

        liftIsMoving = false;

        if (floorUpButton != null)
        {
            if (currentLiftFloor == floorUpButton.floorNumber)
            {
                floorUpButton.ResetUpButton();
                queueForFloorUpButtons.Remove(floorUpButton);
                CheckIfMoreFloorsAreQueued();
            }
        }

        if (floorDownButton != null)
        {
            if (currentLiftFloor == floorDownButton.floorNumber)
            {
                floorDownButton.ResetDownButton();
                queueForFloorDownButtons.Remove(floorDownButton);
                CheckIfMoreFloorsAreQueued();
            }
        }

        if (currentLiftFloor == floorToMove)
        {
            if (mainLiftButtonPressed != null)
            {
                queueForLiftButtons.Remove(mainLiftButtonPressed);
            }
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
        if (queueForLiftButtons.Count > 0)
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
        if (queueForLiftButtons.Count > 0)
        {
            liftReachedDestination = false;
            if (mainLiftButtonPressed != null)
            {
                mainLiftButtonPressed = queueForLiftButtons[0];
                floorToMove = queueForLiftButtons[0].floorNumber;
            }
        } 
        else
        {
            liftReachedDestination = true;
        }

        if (queueForFloorUpButtons.Count > 0)
        {
            floorUpButton = queueForFloorUpButtons[0];
        }

        if (queueForFloorDownButtons.Count > 0)
        {
            floorDownButton = queueForFloorDownButtons[0];
        }
    }

    public void AddFloorToQueue(LiftButton liftButton)
    {
        queueForLiftButtons.Add(liftButton);

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
        queueForFloorUpButtons.Add(floorUpButton);
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
        queueForFloorDownButtons.Add(floorDownButton);
        if (!liftIsMoving && liftReachedDestination)
        {
            liftReachedDestination = false;
            floorToMove = floor.floorNumber;
            StartCoroutine("MoveLiftToFloor");
        }
    }
}