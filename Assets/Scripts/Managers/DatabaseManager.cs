using System.Data;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using Mono.Data.Sqlite;  //Sqlite3 wrapper for .Net
using System;
using InventoryDTOs;
using TMPro;

/*
 * DatabaseManager.cs
 * Nick Chase
 * Namespaces: Mono.Data.Sqlite required for using Sqlite db, this is specific to Wiondwos Develpoment
 */

public class DatabaseManager : MonoBehaviour
{
    //create conenction and maintain filepath here for future name changes
    private IDbConnection _connection;
    private string _databaseName = "InventrackDBv3.db"; // coded name for project 

    public Transform contentPanel; // Drag and drop the Content transform of the ScrollRect in the inspector
    public TMP_Text itemTemplate; // Drag and drop your Text template from the scene
    /*
     * Test for data base integrity
     * Filename is hardocded for initial testing and prototyping
     * Mono is a wrapper(.NET) over Sqlite3 it requires use of URI for file path
     * Application.dataPath() is specific to "/StreamingAssets/" folder - added to Assets folder in project.
     */
    private void Awake()
    {
        
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
    }


    /*
     * Class TestDB()
     * Here the database connection is tested to verify  A) db file works, B) file is readable and has data.
     */
    public void TestDB()
    {
        ClearContentPanel(); // destroy content in panel
        using (IDbCommand command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT ItemName, Quantity FROM Item";
            using (IDataReader reader = command.ExecuteReader())
            {
                // Iterate over the result and create UI Text for each row
                while (reader.Read())
                {
                    string itemName = reader[0].ToString();  // This is now the 0th index since we aren't retrieving id
                    int quantity = Convert.ToInt32(reader[1]); // This is now the 1st index

                    string displayText = $"{itemName}, {quantity} Qty.";
                    // Instantiate a new text object inside the content panel
                    TMP_Text newItem = Instantiate(itemTemplate, contentPanel);
                    newItem.text = displayText; // shows the id + Item name in db
                    newItem.gameObject.SetActive(true);
                }
            }
        }
    }

    private void ClearContentPanel()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    }

    //Insert using DTO's from created  namespace
    public void InsertItem(ItemDTO item)
    {
        using (IDbCommand command = _connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Item (ItemName, Quantity, StorageLocation, DateAdded, Expiry, PackSizeID) VALUES (@itemName, @quantity, @storageLocation, @dateAdded, @expiry, @packSizeID);";

            // Add parameters
            command.Parameters.Add(new SqliteParameter("@itemName", item.ItemName));
            command.Parameters.Add(new SqliteParameter("@quantity", item.Quantity));
            command.Parameters.Add(new SqliteParameter("@storageLocation", item.StorageLocation));
            command.Parameters.Add(new SqliteParameter("@dateAdded", item.DateAdded));

            // Check if Expiry is provided
            if (!string.IsNullOrEmpty(item.Expiry))
                command.Parameters.Add(new SqliteParameter("@expiry", item.Expiry));
            else
                command.Parameters.Add(new SqliteParameter("@expiry", DBNull.Value)); // If expiry is null or empty, insert DBNull.

            command.Parameters.Add(new SqliteParameter("@packSizeID", item.PackSizeID));

            // Execute the command
            command.ExecuteNonQuery();
        }
    }

    public IDbCommand GetCommand()
    {
        return _connection.CreateCommand();
    }

    public int InsertPackSize(PackSizeDTO packSize)
    {
        using (IDbCommand command = _connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO PackSize (Unit, PackSize) VALUES (@unit, @packSize);";

            // Add parameters
            command.Parameters.Add(new SqliteParameter("@unit", packSize.Unit));
            command.Parameters.Add(new SqliteParameter("@packSize", packSize.PackSize));

            // Execute the command
            command.ExecuteNonQuery();

            // Retrieve the ID of the inserted PackSize (useful for when you need to insert into Item table)
            command.CommandText = "SELECT last_insert_rowid()";
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }



    //Concept for mutiple databases in the future
    public void SetDatabaseName(string name)
    {
        _databaseName = name;
    }

    
    private void OnDisable()
    {
        _connection?.Close();
    }


}
