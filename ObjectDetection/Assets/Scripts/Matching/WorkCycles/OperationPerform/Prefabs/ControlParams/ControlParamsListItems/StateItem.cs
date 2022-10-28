using System;
using ClassesForJsonDeserialize;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateItem :  ControlParamItem {
    [SerializeField] private TMP_Text description, nominalText;
    [SerializeField] private Toggle toggleInput, toggleValueNominal;
    
    public override event Action ValueChangedEvent;

    public override void Initialize(ControlParam controlParam, bool previewMode = false){
        if (previewMode){
            toggleInput.interactable = true;
            toggleInput.isOn = controlParam.state_fact;
        }

        toggleValueNominal.isOn = controlParam.state_nominal;
    }

    public float Read(){
        throw new NotImplementedException();
    }

    private void OnEnable() =>
        toggleInput.onValueChanged.AddListener(delegate{ValueChangedEvent?.Invoke();});


    private void OnDisable() =>
        toggleInput.onValueChanged.RemoveAllListeners();
}
