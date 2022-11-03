using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class Comment {
    [SerializeField] private PopupSettings settings;

    private PopupView _popupView;
    private UniTaskCompletionSource<string> _dialogCompletionSource;

    public void Initialize(PopupView popupView) =>
        _popupView = popupView;

    public async UniTask<string> AwaitResult(string context = ""){
        _dialogCompletionSource = new UniTaskCompletionSource<string>();
        _popupView.AcceptEvent += OnAccept;

        if (context != string.Empty) settings.contentText = context;

        _popupView.Show(settings);

        var result = await _dialogCompletionSource.Task;

        _popupView.AcceptEvent -= OnAccept;
        _popupView.Hide();

        return result;
    }

    private void OnAccept() =>
        _dialogCompletionSource.TrySetResult(_popupView.inputField.text);
}
