using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PopupView :View {
   
    [Header("Labels")]
    public TMP_Text headerText;
    public TMP_Text contentText;
    
    [Header("Input")]
    public TMP_InputField inputField;
    public TMP_Text placeholder;
    
    [Header("Buttons")]
    public TMP_Text acceptText;

    public TMP_Text rejectText, additionalText;
    public Button acceptButton, cancelButton, additionalButton;
    
    public event Action AcceptEvent, OnRejectEvent;

    private void Prepare(PopupSettings settings){
        headerText.text = settings.headerText;
        contentText.text = settings.contentText;
        
        acceptButton.gameObject.SetActive(settings.acceptButtonEnable);
        cancelButton.gameObject.SetActive(settings.rejectButtonEnable);
        additionalButton.gameObject.SetActive(settings.additionalButtonEnable);
        
        acceptText.text = settings.button1Text;
        rejectText.text = settings.button2Text;
        additionalText.text = settings.button3Text;
        
        inputField.gameObject.SetActive(settings.inputVisible);
    }

    public void Show(PopupSettings settings){
        Prepare(settings);

        SubscribeButtonEvents();
        Open();
    }

    public void Hide(){
        UnsubscribeButtonEvents();
        Close();
    }

    private void AcceptNotify(){
        AcceptEvent?.Invoke();
    }

    private void RejectNotify(){
        OnRejectEvent?.Invoke();
    }

    private void SubscribeButtonEvents(){
        acceptButton.onClick.AddListener(AcceptNotify);
        cancelButton.onClick.AddListener(RejectNotify);
    }

    private void UnsubscribeButtonEvents(){
        acceptButton.onClick.RemoveListener(AcceptNotify);
        cancelButton.onClick.RemoveListener(RejectNotify);
    }
}