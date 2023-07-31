using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * Nicholas Chase
 * Script for handling Login Operation
 * 
 * 
 */

public class Button_Login : MonoBehaviour
{
    //Varuiables for manageing names
    public string sceneName;
    //testing use
    private string tester;

    // Components
    public TextMeshProUGUI textPlacerHolderName;
    private Button button;
   

    private void Start()
    {
        tester = "Tester";
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    //Loads scene with assigned sceneName in editor
    private void OnClick()
    {
        if(string.IsNullOrEmpty(SceneController.Instance.UserName))
        {
            SceneController.Instance.UserName = tester;
        }
        SceneManager.LoadScene(sceneName);
    }
}

