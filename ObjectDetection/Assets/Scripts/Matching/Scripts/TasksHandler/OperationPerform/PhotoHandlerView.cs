using System;
using UnityEngine;
using UnityEngine.UI;

public class PhotoHandlerView : View {
    [SerializeField] private Button shotButton, closeButton;
    
    public PhotoGalleryView galleryView;
    public event Action TakePhotoEvent, CloseEvent;

    public void OnEnable(){
        shotButton.onClick.AddListener(delegate { TakePhotoEvent?.Invoke(); });
        closeButton.onClick.AddListener(delegate { CloseEvent?.Invoke(); });
    }

    public void OnDisable(){
        shotButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
    }
}