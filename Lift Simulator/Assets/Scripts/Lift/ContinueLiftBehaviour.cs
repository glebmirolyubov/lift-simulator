using UnityEngine;

public class ContinueLiftBehaviour : MonoBehaviour
{
    public LiftManager liftManager;

    public void ContinueMovingLift()
    {
        liftManager.DetermineNextBehaviourAfterLiftDoorsClosed();
    }
}
