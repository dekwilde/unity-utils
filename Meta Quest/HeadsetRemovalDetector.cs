using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

public class HeadsetRemovalDetector : MonoBehaviour
{
    private bool isHeadsetOn;
    private InputDevice device;

    public UnityEvent OnHeadsetWorn;
    public UnityEvent OnHeadsetRemoved;

    void Start()
    {
        // Obtém o dispositivo HMD
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, devices);

        if (devices.Count > 0)
        {
            device = devices[0];
        }
    }

    void Update()
    {
        if (device.isValid)
        {
            bool userPresent = false;
            if (device.TryGetFeatureValue(CommonUsages.userPresence, out userPresent))
            {
                if (userPresent && !isHeadsetOn)
                {
                    isHeadsetOn = true;
                    OnHeadsetWorn.Invoke();
                }
                else if (!userPresent && isHeadsetOn)
                {
                    isHeadsetOn = false;
                    OnHeadsetRemoved.Invoke();
                }
            }
        }
    }
}
