using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class Dialog {
    [SerializeField] private PopupSettings settings;
    private PopupView _popupView;

    private UniTaskCompletionSource<bool> _dialogCompletionSource;

    public void Initialize(PopupView popupView){
        _popupView = popupView;
    }

    public async UniTask<bool> AwaitResult(string msg = ""){
        _dialogCompletionSource = new UniTaskCompletionSource<bool>();

        if (msg != null)
            settings.contentText = msg;

        _popupView.Show(settings);
        _popupView.AcceptEvent += OnAccept;
        _popupView.OnRejectEvent += OnReject;

        var result = await _dialogCompletionSource.Task;

        _popupView.AcceptEvent -= OnAccept;
        _popupView.OnRejectEvent -= OnReject;
        _popupView.Hide();

        return result;
    }

    private void OnAccept() =>
        _dialogCompletionSource.TrySetResult(true);


    private void OnReject() =>
        _dialogCompletionSource.TrySetResult(false);
}