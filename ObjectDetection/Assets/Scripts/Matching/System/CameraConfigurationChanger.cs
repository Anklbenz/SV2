using System;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CameraConfigurationChanger {
    public event Action ResolutionSuccessfullyChangedEvent;

    private readonly ARCameraManager _arCameraManager = null;
    private XRCameraConfiguration? _startConfig;
    private NativeArray<XRCameraConfiguration> _configurations;
    private bool _firstFrameReceived;

    public CameraConfigurationChanger(ARCameraManager arCameraManager){
        _arCameraManager = arCameraManager;
        _arCameraManager.frameReceived += OnFrameReceived;
    }

    public Vector2Int GetCurrentResolution(){
        var cameraConfiguration = _arCameraManager.currentConfiguration;
        Assert.IsTrue(cameraConfiguration.HasValue);
        return cameraConfiguration.Value.resolution;
    }

    private void OnFrameReceived(ARCameraFrameEventArgs args){
        if (!_firstFrameReceived){
            SetMaxResolutionOnFirstFrame();
            _firstFrameReceived = true;
        }
        else{
            if (!ResolutionChangedCheck()) return;

            _arCameraManager.frameReceived -= OnFrameReceived;
            ResolutionSuccessfullyChangedEvent?.Invoke();
        }
    }

    private void SetMaxResolutionOnFirstFrame(){
        if (!_arCameraManager.descriptor.supportsCameraConfigurations) return;

        using (_configurations = _arCameraManager.GetConfigurations(Allocator.Temp)){
            _startConfig = _arCameraManager.currentConfiguration;
            _arCameraManager.currentConfiguration = _configurations.Last();
        }
    }

    private bool ResolutionChangedCheck(){
        if (!_startConfig.HasValue || !_arCameraManager.currentConfiguration.HasValue) return false;
        if (_startConfig == _arCameraManager.currentConfiguration) return false;

        return true;
    }
}