using System;
using System.Collections.Generic;
using ClassesForJsonDeserialize;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ControlParamsView : View {
    [Header("ControlParams")]
    [SerializeField] private RectTransform itemContainer;
    [SerializeField] private Button acceptButton, closeButton;
    [SerializeField] private ItemFactory itemFactory;

    public event Action OnAcceptClickEvent, OnCloseClickEvent, ValueChangedEvent;

    private List<ControlParamItem> _controlParamItems = new();

    public void AddItems(ControlParam[] controlParams){
        foreach (var controlParam in controlParams){
            var item = itemFactory.MenuItemCreate(controlParam.operation_cp_type_code, itemContainer);
            item.Initialize(controlParam);
            item.ValueChangedEvent += OnValueChangedNotify;
            _controlParamItems.Add(item);
        }
    }

    public void ItemsClear(){
        foreach (var item in _controlParamItems){
            item.ValueChangedEvent -= OnValueChangedNotify;
            item.Delete();
        }

        _controlParamItems.Clear();
    }

    public void AcceptInteractable(bool isActive) =>
        acceptButton.interactable = isActive;

    public void AcceptSetActive(bool isActive) =>
        acceptButton.gameObject.SetActive(isActive);

    private void OnValueChangedNotify() =>
        ValueChangedEvent?.Invoke();

    private void OnEnable(){
        acceptButton.onClick.AddListener(delegate { OnAcceptClickEvent?.Invoke(); });
        closeButton.onClick.AddListener(delegate { OnCloseClickEvent?.Invoke(); });
  
    }

    private void OnDisable(){
        acceptButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
   
    }
}