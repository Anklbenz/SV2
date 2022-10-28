using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class Warning {
    [SerializeField] private PopupSettings settings;
    private PopupView _popupView;

    private UniTaskCompletionSource _dialogCompletionSource;

    public void Initialize(PopupView popupView){
        _popupView = popupView;
    }

    public async UniTask AwaitResult(string context = ""){
        _dialogCompletionSource = new UniTaskCompletionSource();
        _popupView.AcceptEvent += OnAccept;

        if (context != string.Empty) settings.contentText = context;

        _popupView.Show(settings);

        await _dialogCompletionSource.Task;

        _popupView.AcceptEvent -= OnAccept;
        _popupView.Hide();
    }

    private void OnAccept() =>
        _dialogCompletionSource.TrySetResult();
}