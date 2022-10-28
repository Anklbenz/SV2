using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class ObjectCreationHandler : MonoBehaviour {
    [SerializeField] private Template template;
    [SerializeField] private Detector detector;
   
    public event Action ObjectDetectedEvent;
    public void Initialize(ARCameraManager arCameraManager, Vector2Int arCameraResolution, Data data){
        var uiScale = new UIScaler().GetScale(arCameraResolution);

        template.Initialize(data.Texture, uiScale);
        detector.Initialize(arCameraManager, arCameraResolution, template.mat, uiScale);

        detector.MatchEvent += delegate { ObjectDetectedEvent?.Invoke(); };
    }

    public void Start(){
        if (template.previewEnabled)
            template.view.Show();
 
        detector.Start();
    }

    public void Stop(){
        detector.Stop();
        
        if (template.previewEnabled)
            template.view.Hide();
    }
}
