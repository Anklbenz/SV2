using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PhotoHandler : MonoBehaviour {
    [SerializeField] private ViewfinderView viewfinderView;
    [SerializeField] private PhotoHandlerView photoHandlerView;
    [SerializeField] private PreviewView previewView;

    private Viewfinder _viewFinder;
    private PhotoGallery _photoGallery;
    private PreviewHandler _previewHandler;

    private List<Texture2D> _images = new();

    private UniTaskCompletionSource _completionSource;

    private void Awake(){
        _viewFinder = new Viewfinder(viewfinderView, this);
        
        _photoGallery = new PhotoGallery(photoHandlerView.galleryView);

        photoHandlerView.galleryView.PhotoSelectedEvent += OnSelect;
        photoHandlerView.TakePhotoEvent += TakePhoto;
        photoHandlerView.CloseEvent += OnClose;
    }

    public async UniTask TakePhotosProcess(){
        _completionSource = new UniTaskCompletionSource();

        photoHandlerView.Open();

        await _completionSource.Task;

        photoHandlerView.Close();
    }

    private async void TakePhoto(){
        var photoTexture = await _viewFinder.TakePhotoProcess();
        
        _images.Add(photoTexture);
        _photoGallery.Add(photoTexture);
    }

    private void OnSelect(Texture2D texture){
        previewView.Show(texture);
    }

    private void OnDelete(Texture2D texture){
        _images.Remove(texture);
    }

    private void OnClose() =>
        _completionSource.TrySetResult();
}
