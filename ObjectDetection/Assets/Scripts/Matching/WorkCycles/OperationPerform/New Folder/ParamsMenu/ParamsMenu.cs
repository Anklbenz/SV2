using System;
using UnityEngine;

using UnityEngine.UI;

public class ParamsMenu : VanishingMenu {

    [SerializeField] private ParamButton noteButton, paramsButton, photoButton;
    public event Action NoteClickedEvent, ParamsClickedEvent, PhotoClickedEvent;
    private bool _mouseIsOver;

    public void SetActiveButtons(bool paramIsActive, bool photoIsActive = true, bool noteIsActive = true){
        paramsButton.SetActive(paramIsActive);
        photoButton.SetActive(photoIsActive);
        noteButton.SetActive(noteIsActive);
    }

    private void PhotoClick(){
        PhotoClickedEvent?.Invoke();
        Vanish();
    }

    private void ParamClick(){
        ParamsClickedEvent?.Invoke();
        Vanish();
    }

    private void NoteClick(){
        NoteClickedEvent?.Invoke();
        Vanish();
    }

    private void Awake(){
        noteButton.button.onClick.AddListener(NoteClick);
        paramsButton.button.onClick.AddListener(ParamClick);
        photoButton.button.onClick.AddListener(PhotoClick);
    }

    private void OnDestroy(){
        noteButton.button.onClick.RemoveAllListeners();
        paramsButton.button.onClick.RemoveAllListeners();
        photoButton.button.onClick.RemoveAllListeners();
    }
}