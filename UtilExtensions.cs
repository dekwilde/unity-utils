using UnityEngine;

public static class UtilsExtensions
{
    
    // HOW TO USE
    // just calling anywhere
    // Quaternion savedRotation = Quaternion.Euler(UtilsExtensions.GetVector3FromString(PlayerPrefs.GetString("CalibrationArco")));
    
    public static Vector3 GetVector3FromString(string key)
    {
        string[] values = PlayerPrefs.GetString(key).Split(',');
        if (values.Length != 3)
        {
            Debug.LogError("Error retrieving Vector3 from PlayerPrefs. Invalid string format.");
            return Vector3.zero;
        }
        float x = float.Parse(values[0]);
        float y = float.Parse(values[1]);
        float z = float.Parse(values[2]);
        return new Vector3(x, y, z);
    }

    public static void SetVector3AsString(string key, Vector3 value)
    {
        string valueString = string.Format("{0},{1},{2}", value.x, value.y, value.z);
        PlayerPrefs.SetString(key, valueString);
    }
}
