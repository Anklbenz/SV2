using System;

using UnityEngine;

public static class DeviceOrientationListener {
    public static DeviceOrientation currentDeviceOrientation{ get; private set; } = UnityEngine.Input.deviceOrientation;
    public static event Action OrientationChangedEvent;
    
    public static void Listen(){
        if (UnityEngine.Input.deviceOrientation == currentDeviceOrientation || UnityEngine.Input.deviceOrientation is DeviceOrientation.FaceUp or DeviceOrientation.FaceDown or DeviceOrientation.Unknown) return;
        currentDeviceOrientation = UnityEngine.Input.deviceOrientation;
        OrientationChangedEvent?.Invoke();
    }
}
