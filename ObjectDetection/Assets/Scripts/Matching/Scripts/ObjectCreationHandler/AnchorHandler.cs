using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class AnchorHandler : MonoBehaviour {
    [SerializeField] private ARAnchorManager anchorManager;
    [SerializeField] private Camera arCamera;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Text info;

    private void Update(){

        info.text = "Rot " + arCamera.transform.rotation.ToString() + " Pos" + arCamera.transform.position.ToString();

        if (UnityEngine.Input.touchCount > 0){
            var touch = UnityEngine.Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began){
                var camLocalPos = arCamera.transform.position + arCamera.transform.forward * 0.3f;
                CreateAnchor(prefab, camLocalPos);
            }
        }
    }

    private void CreateAnchor(GameObject go, Vector3 pos){
        var instantiatedObj = Instantiate(prefab, pos, Quaternion.identity);
     //   instantiatedObj.AddComponent<ARAnchor>();
        
    }
}
