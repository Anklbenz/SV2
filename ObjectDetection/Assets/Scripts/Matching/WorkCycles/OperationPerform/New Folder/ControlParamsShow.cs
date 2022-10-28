using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;

public class ControlParamsShow {
    private ControlParamsView _controlParamsView;
    private UniTaskCompletionSource _completionSource;

    public void Initialize(ControlParamsView view){
        _controlParamsView = view;
    }

    public async UniTask Process(ControlParam[] controlParams){
        _completionSource = new UniTaskCompletionSource();
        _controlParamsView.Open();
        _controlParamsView.AddItems(controlParams);
        _controlParamsView.OnAcceptClickEvent += OnClose;

        await _completionSource.Task;

        _controlParamsView.OnAcceptClickEvent -= OnClose;
        _controlParamsView.ItemsClear();
        _controlParamsView.Close();
    }

    private void OnClose() =>
        _completionSource.TrySetResult();
}