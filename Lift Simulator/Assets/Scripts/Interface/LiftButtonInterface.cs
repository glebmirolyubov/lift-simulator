public interface IInitializable<T>
{
    void InitializeButton(T floorNumber);
}

public interface IPressable
{
    void ButtonPressed();
}

public interface IResettable
{
    void ResetButton();
}
