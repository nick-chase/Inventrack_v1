using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using InventoryDTOs;


public class InventoryController : MonoBehaviour
{
    //Unity Ediotor Objects and Variables for use
    public TMP_InputField inputDateAdded; // Chaneges per day
    public TMP_InputField inputLocationField; // Set the location per previous selection
    public Button submitButton; //turn btn on/off
    public Button backButton; // to return after success
    public TMP_InputField[] inputFields; //fields for validationa dn submission
    public TMP_InputField inputLocationName; //hardcoded location
    public TMP_InputField inputExpirationDate; //optional paramater
    private int locationId; //this is for InsertINto database
    public GameObject successPanel; //Popup
    public DatabaseManager databaseManager;


    //Handle the excecution of validation in list order - elements list built in editor
    public delegate bool ValidationFunction(string input);

    //Private list for vladiation
    private List<ValidationFunction> validationFunctions = new List<ValidationFunction>
{
    ValidationManager.IsValidItemName,
    ValidationManager.IsValidQuantity,
    ValidationManager.IsValidPackSize,
    ValidationManager.IsValidUnit,
    ValidationManager.IsValidDateOrEmpty  // <-- This function is to check if the optional date is either valid or empty.
};



    void Start()
    {
        SetDateText(); //set dates to system date
        SetInputLocationField(); //set location text
        successPanel.SetActive(false); // always star tif deisabled panel
        if (submitButton.interactable == true)
        {
            submitButton.interactable = false;
        }
        
        //UI listeners for InputFields to Validate on
        foreach (TMP_InputField field in inputFields) 
        {
            field.onValueChanged.AddListener(delegate { ValidateFields(); });
        }

        submitButton.onClick.AddListener(OnSubmit);
    }

    private void ValidateFields()
    {
        bool allFieldsValid = true;

        for (int i = 0; i < inputFields.Length; i++)
        {
            if(i == 4 && string.IsNullOrWhiteSpace(inputFields[i].text)) //optional expiration field
            {
                continue;
            }
            if (!ValidationManager.IsNotEmpty(inputFields[i].text) ||
                !validationFunctions[i](inputFields[i].text))
            {
                allFieldsValid = false;
                break;  // If you find an invalid field, break out of the loop.
            }
        }

        submitButton.interactable = allFieldsValid;
    }


    private void SetInputLocationField()
    {
        //Set the deactivated field to the selected location
        if (inputLocationField == null)
        {
            Debug.LogError("InputField reference is missing!");
            return;
        }
        string locationName = SceneController.Instance.LocationName;
        if (string.IsNullOrEmpty(locationName))
        {
            locationName = "Loading...";
            // Log an error and perhaps return to previosus scene (Bakcbutton)?
            Debug.LogError("Location name is null or empty from SceneController.cs");
        }
        //Set scene Title
        inputLocationField.text = locationName;

        // Determine locationId based on locationName to use for insertion
        switch (locationName.ToLower())
        {
            case "dry":
                locationId = 1;
                break;
            case "cooler":
                locationId = 2;
                break;
            case "freezer":
                locationId = 3;
                break;
            default:
                Debug.LogError("Unknown location name: " + locationName);
                locationId = -1; // Indicates an unknown location
                break;
        }
        Debug.Log(locationId + " saved location");
    }

    private void SetDateText()
    {
        //use system date to set the text in the form fro insertion
        string currentDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
        inputDateAdded.text = currentDate;
        Debug.Log("date" + currentDate);

    }

    //InputFields are listed in order in the Editor, this logfic isnt noticed here.
    public void OnSubmit()
    {
        // Mapping UI input to PackSizeDTO
        PackSizeDTO packSize = new PackSizeDTO
        {
            Unit = inputFields[3].text,  //  4rd input field is for Unit
            PackSize = int.Parse(inputFields[2].text)  //  3th input field is for PackSize
            
    };

        // Insert into PackSize and get the ID
        int packSizeID = databaseManager.InsertPackSize(packSize);
        Debug.Log("ID:" + packSizeID);
        Debug.Log($"Attempting to parse date: {inputDateAdded.text}");
        Debug.Log($"Attempting to parse date: {inputExpirationDate.text}");


        // Mapping UI input to ItemDTO
        ItemDTO item = new ItemDTO
        {
            ItemName = inputFields[0].text,  // 1st input field is for Item Name
            Quantity = int.Parse(inputFields[1].text),  //  2nd input field is for Quantity
            StorageLocation = locationId,
            DateAdded = inputDateAdded.text,
            Expiry = inputExpirationDate.text, //these dates are validated already
            PackSizeID = packSizeID
            
        };

        // Insert item
        databaseManager.InsertItem(item);
        Debug.Log("Submitted successfully!");

        ShowSuccessAndReturn(); // display notice adn call back button
    }

    private void ShowSuccessAndReturn()
    {
        //deactivate Submit button
        submitButton.interactable = false;

        successPanel.SetActive(true);

        Invoke("Return", 2.0f);
    }


    private void Return()
    {
        backButton.onClick.Invoke();
    }
}

