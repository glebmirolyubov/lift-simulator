using UnityEngine;
using UnityEngine.UI;

public class GetNumberOfFloors : MonoBehaviour
{
    public GameObject startCanvas;
    public UIManager uIManager;
    public GameObject errorText;

    InputField input;

    void Start()
    {
        startCanvas.SetActive(true);
        errorText.SetActive(false);
        input = gameObject.GetComponent<InputField>();
    }

    public void SubmitNumberOfFloors()
    {
        try
        {
            int.TryParse(input.text, out int numberOfFloors);

            if (numberOfFloors >= 5 && numberOfFloors <= 20)
            {
                uIManager.SetNumberOfFloors(numberOfFloors);
                Debug.Log("Correct! There are " + numberOfFloors + " floors.");
                startCanvas.SetActive(false);
            }
            else
            {
                errorText.SetActive(true);
            }
        }
        catch
        {
            errorText.SetActive(true);
        }
    }
}