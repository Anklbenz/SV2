using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Raycasting : MonoBehaviour {
       [SerializeField] private ARRaycastManager aRRaycastManager;
       [SerializeField] private ARAnchorManager ARAnchorManager;
       [SerializeField] private GameObject prefab;
       [SerializeField] private Text log; 

       private List<ARRaycastHit> hits = new List<ARRaycastHit>();

       private void Update(){

              if (UnityEngine.Input.touchCount == 0){
                     return;

              }

              Touch touch = UnityEngine.Input.GetTouch(0);

              if (touch.phase != TouchPhase.Began){
                     return;
              }

              if (aRRaycastManager.Raycast(touch.position, hits, TrackableType.FeaturePoint)){
                     foreach (var hit in hits){
                            var obj =Instantiate(prefab, hit.pose.position, Quaternion.identity);
                            obj.AddComponent<ARAnchor>();
                            log.text += hit.distance.ToString() + "\n";
                     }
              }
       }
}
