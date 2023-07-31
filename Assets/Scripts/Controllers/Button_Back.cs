using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Nicholas Chase
 * Reusable Back Button for jumping back in scene progression
 * 
 */

public class Button_Back : MonoBehaviour
{

    // Component
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    //Load scene index and return 1 form current scene
    private void OnClick()
    {
        //Build scene index - This works specifically because of linear scene flow
        //If linear flow is chaneged then this code will need refactoring
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        
        //load the scene based on the index
        SceneManager.LoadScene(previousSceneIndex);
    }

}
