using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DeviceSupport : MonoBehaviour {

    [SerializeField] ARSession m_Session;

    IEnumerator Start(){
        if (ARSession.state is ARSessionState.None or ARSessionState.CheckingAvailability){
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state == ARSessionState.Unsupported){
            Debug.Log("Unsupported");
        }
        else{
            // Start the AR session
            m_Session.enabled = true;
        }
    }
}


