using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * Nicholas Chase
 * Reusable Button for adding and managing Scenes
 * 
 */

public class ButtonPrefab : MonoBehaviour
{
    //Varuiable for manageing Scene names
    public string sceneName;
    
    // Component
    private Button button;
    private TextMeshProUGUI buttonText;

    private void Start()
    {
        
        button = GetComponent<Button>();
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>(); 
        button.onClick.AddListener(OnClick);
 
    }

    //Loads scene with assigned sceneName in editor
    private void OnClick()
    {
        if (buttonText == null)
        {
            Debug.LogError("buttonText is null");
            return;
        }

        if (SceneController.Instance == null)
        {
            Debug.LogError("SceneController.Instance is null");
            return;
        }

        //Set String from button
        SceneController.Instance.LocationName = buttonText.text;
        //test
        Debug.Log(SceneController.Instance.LocationName);
        SceneManager.LoadScene(sceneName);
    }
}
