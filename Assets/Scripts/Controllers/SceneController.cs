using UnityEngine.SceneManagement;
using UnityEngine;

/*
 * Nicholas Chase 
 * Scene management script that handles all scene chanegs loading/unloading
 * This is a persistant scipt that will resid ein Login Scene and check for a single instance
 * of itself when returning to login scene
 * 
 */

public class SceneController : MonoBehaviour
{
    //This instance to maintain single
    public static SceneController Instance { get; private set; }
    
    //Used to feed location name to location scene - inventory form
    public string LocationName { get; set; }

    //hold user name
    public string UserName { get; set; }    
    

    private void Awake()
    {
        //adds persistance, references the object it is attached to in scene.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("SceneController is set to DontDestroyOnLoad.");
           
        }
        else
        {
            //On awake creates new instance tus destroy previous
            Destroy(gameObject);
        }
    }

    /*
     *  
     */
    public void LoadLoginScene()
    {
        
        SceneManager.LoadScene("Login");
    }

    /*
     *  
     */
    public void LoadDashboardScene()
    {
        
        SceneManager.LoadScene("Dashboard");
    }

    /*
     *  
     */
    public void LoadInventoryAddScene()
    {
        
        SceneManager.LoadScene("InventoryAdd");
    }


}
