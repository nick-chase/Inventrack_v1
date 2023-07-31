using UnityEngine;
using TMPro;
using System;

public class DashboardController : MonoBehaviour
{
    //Modifiable Object in Scene
    public TextMeshProUGUI UserText; // Chaneges per accoutn logged in
    

    // Start is called before the first frame update
    void Start()
    {
        
        
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
