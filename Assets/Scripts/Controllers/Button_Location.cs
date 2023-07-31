using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Nicholas Chase
 * Reusable Button for adding and managing Scenes
 * 
 */

public class Button_Location : MonoBehaviour
{
    //Varuiable for manageing Scene names
    public string sceneName;

    // Component
    private Button button;
    private Text buttonText;

    private void Start()
    {

        button = GetComponent<Button>();
        buttonText = button.GetComponentInChildren<Text>();
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

        SceneController.Instance.LocationName = buttonText.text;
        SceneManager.LoadScene(sceneName);
    }
}

