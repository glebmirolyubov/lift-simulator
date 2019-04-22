public interface IMovable<T>
{
    void MoveToFloor(T floorToMove);
}

public interface IStoppable
{
    void StopLift();
}