using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDTO : MonoBehaviour
{
    public string Name { get; set; }
    public double Cost { get; set; }
    public string CostUnit {get; set;}
    public int Quantity { get; set;}
    public string ExpDate { get; set;}
    public string DateAdded { get; set;} //Business Logic
    public string Category { get; set;} //Business Logic 
    public string Location { get; set;} //Business Logic

}
