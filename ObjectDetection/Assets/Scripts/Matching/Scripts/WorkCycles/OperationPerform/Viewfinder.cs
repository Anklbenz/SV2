using Cysharp.Threading.Tasks;
using UnityEngine;

public class Viewfinder {
    private readonly ViewfinderView _view;
    private readonly MonoBehaviour _coroutineRunner;
    private UniTaskCompletionSource<Texture2D> _completionSource;

    public Viewfinder(ViewfinderView viewfinderView, MonoBehaviour coroutineRunner){
        _view = viewfinderView;
        _coroutineRunner = coroutineRunner;
    }

    public async UniTask<Texture2D> TakePhotoProcess(){
        _completionSource = new UniTaskCompletionSource<Texture2D>();
        _view.Open();
        _view.TakePhotoEvent += OnTakePhotoClick;

        var result = await _completionSource.Task;

        _view.TakePhotoEvent -= OnTakePhotoClick;
        _view.Close();
        return result;
    }

    private async void OnTakePhotoClick(){
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