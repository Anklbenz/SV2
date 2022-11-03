using UnityEngine;
using UnityEngine.XR.ARFoundation;

[System.Serializable]
public class Instantiator {
    [SerializeField] private Camera camera;
    [SerializeField] private ARAnchorManager anchorManager;
    [SerializeField] private GameObject prefab;

    private Data _data;

    public void Initialize(Data data){
        _data = data;
    }

    public GameObject Create(){
        var cameraTransform = camera.transform;
        var invertedPosition = cameraTransform.TransformPoint(_data.Position);

        var invertedRotation = cameraTransform.rotation * Quaternion.Euler(_data.Rotation);
        var instance = UnityEngine.Object.Instantiate(prefab, invertedPosition, invertedRotation);
        instance.AddComponent<ARAnchor>();

        return instance;
    }
}
