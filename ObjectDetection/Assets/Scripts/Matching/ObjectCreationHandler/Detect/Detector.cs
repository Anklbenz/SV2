using OpenCvSharp;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[System.Serializable]
public class Detector {
    [SerializeField] private Preview preview;
    [SerializeField] private EdgeSettings cameraEdgeSettings;
    [SerializeField] private float firstMatchAcc;
    [SerializeField] private float secondMatchAcc;
    [SerializeField] private bool previewEnabled;

    public event System.Action MatchEvent;
    public Vector2 matches{ get; private set; }

    private CPUMatrixGetter _cpuMatrixGetter;
    private Mat _templateMat;

    public void Initialize(ARCameraManager arCameraManager, Vector2Int arCameraResolution, Mat templateMat, float uiScale =1){
   
        var templateSize = new Vector2Int(templateMat.Height, templateMat.Width);
        _templateMat = templateMat;

        if (previewEnabled)
            preview.Initialize(templateSize, uiScale);

        _cpuMatrixGetter = new CPUMatrixGetter(arCameraManager);
        _cpuMatrixGetter.Initialize(arCameraResolution, templateSize);
        _cpuMatrixGetter.OnMatReadyEvent += OnFrameReady;
    }

    public void Start(){
        _cpuMatrixGetter.Start();
    }

    public void Stop(){
        _cpuMatrixGetter.Stop();
    }

    private void OnFrameReady(){
        var cameraEdges = CvFeatures.GetEdges(_cpuMatrixGetter.cpuMat, cameraEdgeSettings);
        
        if (previewEnabled)
            preview.PlayWithTemplate(cameraEdges, _templateMat);

        Match(cameraEdges);
    }

    private void Match(Mat cameraEdges){
        if (CvFeatures.TryMatch(_templateMat, cameraEdges, firstMatchAcc, out var firstMatch)){

            var maskDilate = new Mat();
            Cv2.BitwiseAnd(cameraEdges, _templateMat, maskDilate);
            
            if (CvFeatures.TryMatch(_templateMat, maskDilate, secondMatchAcc, out var secondMatch)){
                MatchEvent?.Invoke();
                matches = new Vector2(firstMatch, secondMatch);
            }

            Debug.Log("First Match" + firstMatch + "  " + "Second Match" + secondMatch);
        }
    }
}
