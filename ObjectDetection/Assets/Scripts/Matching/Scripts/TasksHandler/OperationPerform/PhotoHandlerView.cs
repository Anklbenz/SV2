using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PhotoHandlerView : View {
    [Header(("PhotoHandler"))]
    [SerializeField] private Button shotButton;
    [SerializeField] private Button closeButton;
    public PhotoGalleryView photoGalleryView;

    public event Action TakePhotoEvent, CloseEvent;

    public override void Open(){
        shotButton.onClick.AddListener(delegate { TakePhotoEvent?.Invoke(); });
        closeButton.onClick.AddListener(delegate { CloseEvent?.Invoke(); });
        base.Open();
    }

    public override void Close(){
        shotButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
        base.Close();
    }
}