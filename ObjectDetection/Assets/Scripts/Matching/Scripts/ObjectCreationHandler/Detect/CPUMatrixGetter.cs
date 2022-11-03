using System;
using OpenCvSharp;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CPUMatrixGetter : IDisposable {
    public event Action OnMatReadyEvent;
    public Mat cpuMat{ get; private set; }

    private readonly ARCameraManager _arCameraManager;
    private XRCpuImage.ConversionParams _conversionParams;
    private Vector2Int _cameraResolution;

    public CPUMatrixGetter(ARCameraManager arCameraManager){
        _arCameraManager = arCameraManager;
    }

    public void Initialize(Vector2Int screenSize, Vector2Int templateSize){
        cpuMat = new Mat(templateSize.y, templateSize.x, MatType.CV_8UC4);

        _conversionParams = GetConversionParams(screenSize, templateSize);
    }

    public void Start(){
        _arCameraManager.frameReceived += OnCameraFrameReceived;
    }

    public void Stop(){
        _arCameraManager.frameReceived -= OnCameraFrameReceived;
    }

    public void Dispose(){
        Stop();
    }

    private unsafe void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs){
        if (!_arCameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            return;

        image.Convert(_conversionParams, (IntPtr)cpuMat.DataPointer, (int)cpuMat.Total() * (int)cpuMat.ElemSize());
        image.Dispose();
        
        RotateMatDisplayDepend();
        OnMatReadyEvent?.Invoke();
    }

    private XRCpuImage.ConversionParams GetConversionParams(Vector2Int sourceRect, Vector2Int inputRect, TextureFormat format = TextureFormat.BGRA32){
        var rect = GetRect(sourceRect, inputRect);

        return new XRCpuImage.ConversionParams
        {
            inputRect = rect,
            outputDimensions = new Vector2Int(inputRect.x, inputRect.y),
            outputFormat = format,
        };
    }

    private RectInt GetRect(Vector2Int sourceSize, Vector2Int inputSize){
        var x = sourceSize.x / 2 - inputSize.x / 2;
        var y = sourceSize.y / 2 - inputSize.y / 2;
        return new RectInt(x, y, inputSize.x, inputSize.y);
    }

    private void RotateMatDisplayDepend(){
        switch (UnityEngine.Input.deviceOrientation){
            case (DeviceOrientation.Portrait):
                Cv2.Rotate(cpuMat, cpuMat, RotateFlags.Rotate90Clockwise);
                break;
            case (DeviceOrientation.LandscapeRight):
                Cv2.Rotate(cpuMat, cpuMat, RotateFlags.Rotate180);
                break;
            case (DeviceOrientation.PortraitUpsideDown):
                Cv2.Rotate(cpuMat, cpuMat, RotateFlags.Rotate90CounterClockwise);
                break;
        }
    }
}