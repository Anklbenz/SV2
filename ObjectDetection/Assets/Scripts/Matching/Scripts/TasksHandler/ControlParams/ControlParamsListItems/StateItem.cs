using System;
using ClassesForJsonDeserialize;
using TMPro;
using UnityEngine;

public class StateItem :  ControlParamItem {
    [SerializeField] private TMP_Text description, nominalText;
    [SerializeField] private ColoredToggle toggleInput, toggleValueNominal;
    
    public override event Action ValueChangedEvent;

    public override void Initialize(ControlParam controlParam, bool previewMode = false){
        if (previewMode){
            toggleInput.toggle.interactable = true;
            toggleInput.toggle.isOn = controlParam.state_fact;
        }

        toggleValueNominal.toggle.isOn = controlParam.state_nominal;
    }
    

    private void OnEnable() =>
        toggleInput.toggle.onValueChanged.AddListener(delegate{ValueChangedEvent?.Invoke();});


    private void OnDisable() =>
        toggleInput.toggle.onValueChanged.RemoveAllListeners();
}
