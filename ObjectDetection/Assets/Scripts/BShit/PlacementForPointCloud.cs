using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementForPointCloud : MonoBehaviour {
    [SerializeField] private GameObject prefab;
    [SerializeField] private Text log;
    [SerializeField] private Camera _Camera;
    private Vector2 _touchPosition = default;
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> _hits = new();

    private void Awake(){
        arRaycastManager = GetComponent<ARRaycastManager>();

    }

    private void Update(){
        Touch touch = UnityEngine.Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began){
            _touchPosition = touch.position;

            if (arRaycastManager.Raycast(_Camera.transform.forward, _hits, TrackableType.FeaturePoint)){
        //log.text = $"pos{_Camera.transform.position} rot{_Camera.transform.rotation}"; //hitPose.position.ToString();
        var hitPose = _hits[0].pose;
        log.text = "tap" + hitPose.position;        
                Instantiate(prefab, hitPose.position, Quaternion.identity);
            }
        }
    }
}
