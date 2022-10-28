using System;
using System.Collections.Generic;
using ClassesForJsonDeserialize;
using TMPro;
using UnityEngine;

public class DropDownItem :  ControlParamItem {
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Dropdown dropdownInput;

    public override event Action ValueChangedEvent;
    private readonly List<int> _dropdownRowsValues = new();
    private ControlParam _controlParam;
    public override void Initialize(ControlParam controlParam, bool previewMode = false){
        _controlParam = controlParam;
        descriptionText.text = _controlParam.name;
        DropDownItemsAdd(_controlParam.DropDownRows);
    }
    
    private void DropDownItemsAdd(WorkLogOperationDropDownRow[] rows){
        foreach (var dropdownRow in rows){
            var rowId = dropdownRow.id;
            var rowName = dropdownRow.name;
            dropdownInput.options.Add(new TMP_Dropdown.OptionData($"{rowName}"));

            _dropdownRowsValues.Add(rowId);
        }

        dropdownInput.SetValueWithoutNotify(-1);
    }

    private void OnValueChanged(int index){
        _controlParam.drop_down_selected = dropdownInput.options[index].text;
        _controlParam.selected_id = _dropdownRowsValues[index];
        _controlParam.is_complete = true;
        
        ValueChangedEvent?.Invoke();
    }
    
    private void OnEnable(){
        dropdownInput.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable() => dropdownInput.onValueChanged.RemoveAllListeners();
}