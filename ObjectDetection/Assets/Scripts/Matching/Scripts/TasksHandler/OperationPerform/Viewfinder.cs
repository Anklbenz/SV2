using Cysharp.Threading.Tasks;
using UnityEngine;

public class Viewfinder {
    private readonly ViewfinderView _viewfinderView;
    private readonly MonoBehaviour _coroutineRunner;
    private UniTaskCompletionSource<Texture2D> _completionSource;

    public Viewfinder(ViewfinderView viewfinderViewfinderView, MonoBehaviour coroutineRunner){
        _viewfinderView = viewfinderViewfinderView;
        _coroutineRunner = coroutineRunner;
    }

    public async UniTask<Texture2D> TakePhotoProcess(){
        _completionSource = new UniTaskCompletionSource<Texture2D>();
        _viewfinderView.Open();
        _viewfinderView.TakePhotoEvent += OnTakePhotoClick;
        ShowHideHandler.instance.HideEverything(_viewfinderView);

        var result = await _completionSource.Task;

        ShowHideHandler.instance.ShowEverything();
        _viewfinderView.TakePhotoEvent -= OnTakePhotoClick;
        _viewfinderView.Close();
        return result;
    }

    private async void OnTakePhotoClick(){
        _viewfinderView.Close();
        var screenshotTexture = await TakePhoto(_coroutineRunner);
        if (screenshotTexture != null)
            _completionSource.TrySetResult(screenshotTexture);
    }


    private async UniTask<Texture2D> TakePhoto(MonoBehaviour coroutineRunner){
        var screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        await UniTask.WaitForEndOfFrame(coroutineRunner);

        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();
        return screenShot;
    }
}