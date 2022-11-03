using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PhotoHandlerView : View {
    [SerializeField] private Button shotButton, closeButton;
    public PhotoGalleryView photoGalleryView;

    public event Action OnTakePhotoEvent, CloseEvent;

    public override void Open(){
        shotButton.onClick.AddListener(delegate { OnTakePhotoEvent?.Invoke(); });
        closeButton.onClick.AddListener(delegate { CloseEvent?.Invoke(); });
        base.Open();
    }

    public override void Close(){
        shotButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
        base.Close();
    }
}