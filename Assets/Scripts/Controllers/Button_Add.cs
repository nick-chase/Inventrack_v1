using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/*
 * Nicholas Chase
 */

public class Button_Add : MonoBehaviour
{
    //Varuiable for manageing Scene names
    public string sceneName;

    // Component
    private Button button;

    // 
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {

        SceneManager.LoadScene(sceneName);
    }

}
