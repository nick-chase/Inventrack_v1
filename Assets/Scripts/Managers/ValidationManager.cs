using System;
using System.Globalization;
using System.Text.RegularExpressions;

/*
 * ValidationManager.cs
 * Nick Chase
 * Validation for fields enforced in 3 ways
 * First,String notnull/whitespace for required fields will disable the submit button
 * Second,Regex per each field based of off lsit In Edito
 * Thrid, legnth requirements enforced from InputFields specification in Editor. 
 * Modification to the Input requiremnetns will need to be reflected here. 
 */

public static class ValidationManager
{
    public static bool IsValidItemName(string input)
    {
        var regex = new Regex("^[a-zA-Z0-9 .-]+$");
        bool isValid = regex.IsMatch(input);
        //if (!isValid) Debug.Log($"Invalid Item Name: {input}");
        return isValid;
    }

    public static bool IsValidQuantity(string input)
    {
        if (int.TryParse(input, out int quantity) && quantity > 0)
            return true;

        //Debug.Log($"Invalid Quantity: {input}");
        return false;
    }

    public static bool IsValidUnit(string input)
    {
        string[] validUnits = { "lbs", "oz", "gal", "each", "case" };
        bool isValid = Array.Exists(validUnits, unit => unit.Equals(input, StringComparison.OrdinalIgnoreCase));
        //if (!isValid) Debug.Log($"Invalid Unit: {input}");
        return isValid;
    }

    public static bool IsValidPackSize(string input)
    {
        if (int.TryParse(input, out int packSize) && packSize > 0)
            return true;

        //Debug.Log($"Invalid Pack Size: {input}");
        return false;
    }

    public static bool IsValidDate(string dateInput)
    {
        bool isValid = DateTime.TryParseExact(dateInput, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime _);
        //if (!isValid) Debug.Log($"Invalid Date: {dateInput}");
        return isValid;
    }

    public static bool IsValidDateOrEmpty(string dateInput)
    {
        if (string.IsNullOrWhiteSpace(dateInput))
            return true;

        return IsValidDate(dateInput);
    }

    public static bool IsNotEmpty(string input)
    {
        return !string.IsNullOrWhiteSpace(input);
    }
}

