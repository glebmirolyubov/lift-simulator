public interface IPressable
{
    void LiftButtonPressed();
}

public interface IInitializable<T>
{
    void InitializeLiftButton(T floorNumber);
}
