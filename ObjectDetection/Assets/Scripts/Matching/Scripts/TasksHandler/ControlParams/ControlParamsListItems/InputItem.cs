using TMPro;
using System;
using UnityEngine;
using System.Globalization;
using ClassesForJsonDeserialize;

public class InputItem : ControlParamItem {
    [SerializeField] private TMP_Text descriptionText, nominalText, unitsText;
    [SerializeField] private TMP_InputField valueInput;

    public override event Action ValueChangedEvent;
    private ControlParam _controlParam;

    public override void Initialize(ControlParam controlParam, bool previewMode = false){
        _controlParam = controlParam;

        if (previewMode){
            valueInput.readOnly = true;
            valueInput.text = _controlParam.value_fact.ToString(CultureInfo.InvariantCulture);
        }

        var units = _controlParam.value_unit;

        descriptionText.text = _controlParam.name;
        nominalText.text = $"{_controlParam.value_nominal}  {units}";
        unitsText.text = units;
    }

    private void OnValueChanged(string valueText){
        if (float.TryParse(valueText, out var result)){
            _controlParam.value_fact = result;
            _controlParam.is_complete = true;
        }
        else{
            _controlParam.is_complete = false;
        }

        ValueChangedEvent?.Invoke();
    }

    private void OnEnable() =>
        valueInput.onValueChanged.AddListener(OnValueChanged);


    private void OnDisable() =>
        valueInput.onValueChanged.RemoveAllListeners();
}