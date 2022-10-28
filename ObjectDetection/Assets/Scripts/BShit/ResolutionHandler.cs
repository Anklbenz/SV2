using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ResolutionHandler : MonoBehaviour {
    private const int SAMSUNG_AR_CAM_1920_1080 = 0;
    public event Action<Vector2Int> OnResolutionChangedEvent;


    [SerializeField] private ARCameraManager arCameraManager;
    [SerializeField] private Button setButton;

    private void Awake(){
        setButton.onClick.AddListener(SetARResolution);
    }

    public Vector2Int GetARResolution(){
        if ((arCameraManager == null) || (arCameraManager.subsystem == null) || !arCameraManager.subsystem.running){
            return Vector2Int.zero;
        }

        var configuration = arCameraManager.currentConfiguration;

        if (configuration.HasValue)
            return new Vector2Int(configuration.Value.width, configuration.Value.height);

        return Vector2Int.zero;
    }

    private void SetARResolution(){
        if ((arCameraManager == null) || (arCameraManager.subsystem == null) || !arCameraManager.subsystem.running)
            return;

        using (var configurations = arCameraManager.GetConfigurations(Allocator.Temp)){
            if (SAMSUNG_AR_CAM_1920_1080 >= configurations.Length)
                return;

//            var configuration = configurations[SAMSUNG_AR_CAM_1920_1080];

            arCameraManager.currentConfiguration = configurations.Last();
            
            var configuration = arCameraManager.currentConfiguration;

            if (configuration.HasValue)
                Debug.Log(configuration.Value.width+" "+ configuration.Value.height);
            
            OnResolutionChangedEvent?.Invoke(new Vector2Int(configuration.Value.width, configuration.Value.width));
        }
    }
}
