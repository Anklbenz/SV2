using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PhotoHandler : MonoBehaviour {
   
    [SerializeField] private ViewfinderView viewfinderView;
    [SerializeField] private PhotoHandlerView photoHandlerView;

    private Viewfinder _viewFinder;
    private PhotoGallery _photoGallery;
    private UniTaskCompletionSource _completionSource;

    private void Awake(){
        _viewFinder = new Viewfinder(viewfinderView, this);
       // _photoGallery = new PhotoGallery(photoGalleryView);
    }

    public async UniTask TakePhotosProcess(Texture2D[] texturesContainer){
        _completionSource = new UniTaskCompletionSource();

      //  photoGalleryView.Open();

        await _completionSource.Task;

      //  photoGalleryView.Close();
    }

    private async void OnTakePhoto(){
        await _viewFinder.TakePhotoProcess();
    }

    private void OnClose() =>
        _completionSource.TrySetResult();
}
