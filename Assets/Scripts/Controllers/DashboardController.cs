using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DashboardController : MonoBehaviour
{
    //Modifiable Object in Scene
    public TextMeshProUGUI UserText; // Chaneges per accoutn logged in
    public Button yourButton;  // Reference to your UI Button.
    public DatabaseManager databaseManager;  // Reference to your DatabaseManager.

    // Start is called before the first frame update
    void Start()
    {
        // Ensure both references have been set in the inspector.
        if (yourButton != null && databaseManager != null)
        {
            yourButton.onClick.AddListener(databaseManager.TestDB);  // Bind the TestDB method to your button's onClick event.
        }
        else
        {
            Debug.LogError("Button or DatabaseManager reference is not set.");
        }

        //Set the User name from Session.
        SetCurrentUser();
    }

    private void SetCurrentUser()
    {
        //Get saved string
        string userName = SceneController.Instance.UserName;

        if (string.IsNullOrEmpty(userName))
        {
            userName = "Loading..";
            //Add more logic to relode -> login screen
        }
        //Set Scene string
        UserText.text = userName;
    }
}
