using UnityEngine;
using TMPro;

/*
 * Nicholas Chase
 * Code Refactored 7/18
 * Removed extra references to fields
 */
public class LocationController : MonoBehaviour
{
    //Modifiable Object in Scene
    public TextMeshProUGUI locationText; // Chaneges per location selected
    
    //call at start to set the location title
    void Start()
    {
        SetLocationTitle();
    }

    private void SetLocationTitle()
    {
        //get saved string -> from dashboard button that was clicked
        string locationName = SceneController.Instance.LocationName;

        if (string.IsNullOrEmpty(locationName) )
        {
            locationName = "Loading...";
            // Log an error and perhaps return to previosus scene (Bakcbutton)?
            Debug.LogError("Location name is null or empty from SceneController.cs");
        }
        //Set scene Title
        locationText.text = locationName;
    }



}
