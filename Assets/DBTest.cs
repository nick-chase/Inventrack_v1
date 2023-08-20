using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;  //Sqlite3 wrapper for .Net
using System;
using InventoryDTOs;

/*
 * DBTest.cs
 * Nick Chase
 * Namespaces: Mono.Data.Sqlite required for using Sqlite db, this is specific to Wiondwos Develpoment
 */

public class DBTest : MonoBehaviour
{
    public DatabaseManager databaseManager;
    public Text resultText; // Reference to your UI Text component.

    public Transform contentPanel; // Drag and drop the Content transform of the ScrollRect in the inspector
    public Text itemTemplate; // Drag and drop your Text template from the scene


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void TestDB()
    {
        using (IDbCommand command = databaseManager.GetCommand())
        {
            command.CommandText = "SELECT ItemName FROM Item";
            using (IDataReader reader = command.ExecuteReader())
            {
                string allResults = "";
                while (reader.Read())
                {
                    string rowData = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        rowData += reader[i].ToString() + " ";
                    }
                    allResults += rowData + "\n"; // New line for each row.
                }
                resultText.text = allResults; // Update the UI Text component.
            }
        }
    }

}
