using System.Data;
using System.IO;
using UnityEngine;
using Mono.Data.Sqlite;  //Sqlite3 wrapper for .Net

/*
 * DatabaseManager.cs
 * Nick Chase
 * Namespaces: Mono.Data.Sqlite required for using Sqlite db, this is specific to Wiondwos Develpoment
 */

public class DatabaseManager : MonoBehaviour
{
    //create conenction and maintain filepath here for future name changes
    private IDbConnection _connection;
    private string _databaseName; 

    /*
     * Test for data base integrity
     * Filename is hardocded for initial testing and prototyping
     * Mono is a wrapper(.NET) over Sqlite3 it requires use of URI for file path
     * Application.dataPath() is specific to "/StreamingAssets/" folder - added to Assets folder in project.
     */
    private void Awake()
    {
        SetDatabaseName("InventrackDBv3.db"); //This name
        string pathToDatabase = "URI=file:" + Application.dataPath + "/StreamingAssets/" + _databaseName;
        if(File.Exists(pathToDatabase))
        {
            Debug.Log(" Dtaabase file path not found");

        }

        OpenDatabaseConnection();
        //pull data fgorm db
        //TestDB();
    }


    /*
    * To connect to databnase requires the sqlite3.dll 
    * Mono.Data.Sqlite.dll from the Unity isntall folder
    */
    private void OpenDatabaseConnection()
    {
        string connect = "URI=file:" + Application.dataPath + "/StreamingAssets/" + _databaseName;
        _connection = new SqliteConnection(connect);
        _connection.Open();

        TestDB();
        //throw new NotImplementedException();
    }


    /*
     * Class TestDB()
     * Here the database connection is tested to verify  A) db file works, B) file is readable and has data.
     */
    private void TestDB()
    {
        IDbCommand command = _connection.CreateCommand();
        command.CommandText = "SELECT Name FROM Item";
        IDataReader reader = command.ExecuteReader();

        // Iterate over the result and log each row
        while (reader.Read())
        {
            string rowData = "";
            for (int i = 0; i < reader.FieldCount; i++)
            {
                rowData += reader[i].ToString() + " ";
            }
            Debug.Log(rowData);
        }

        reader.Close();
    
        /*
         * 
         * 
         */
        /*using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM sqlite_master WHERE type='table' AND name='Location' ;";
            object result = command.ExecuteScalar();

            if (result != null)
            {
                // The table exists
                Debug.Log("Item table exists.");
            }
            else
            {
                // The table does not exist
                Debug.Log("Item table does not exist.");
            }
        }*/

    }


    

    /*
     * SetDatabaseName()
     * For rebuilding db purposes, in the event that file is corrupted(?).
     */
    public void SetDatabaseName(string name)
    {
        _databaseName = name; 
    }


 


}
