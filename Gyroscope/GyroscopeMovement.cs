/* Gyroscope Control v0.1
 * Simply add to a gameobject and it will turn with your device.
 * --> TO DO: add method for set/reset camera orientation;
*/

using UnityEngine;

public class GyroscopeMovement : MonoBehaviour
{
    [Tooltip("Shows OnGUI gyroscope values")]
    public bool debugValues = false;

    private GameObject cameraContainer;
    private Gyroscope gyro;
    private bool gyroEnabled;
    private Quaternion rot;

    void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnableGyro();
    }

    /** TODO: add method for set/reset camera orientation */

    private bool EnableGyro() {
        if (SystemInfo.supportsGyroscope) {
            gyro = Input.gyro;
            gyro.enabled = true;
            
            cameraContainer.transform.rotation = Quaternion.Euler(90,90,0);
            rot = new Quaternion(0,0,1,0);

            return true;
        }
        return false;
    }

    private void Update() {
        if (gyroEnabled) {
            transform.localRotation = gyro.attitude * rot;
        }
    }

    private void OnGUI()
    {
        if (debugValues) {
            GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
        }
    }
}
