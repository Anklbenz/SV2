using System.Linq;
using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;

public class ControlParamsInput {
    private ControlParamsView _controlParamsView;
    private ControlParam[] _controlParams;

    private UniTaskCompletionSource _completionSource;

    public void Initialize(ControlParamsView view) =>
        _controlParamsView = view;

    public async UniTask InputProcess(ControlParam[] controlParams){
        _controlParams = controlParams;

        _completionSource = new UniTaskCompletionSource();
        _controlParamsView.Open();
        _controlParamsView.AddItems(controlParams);
        _controlParamsView.ValueChangedEvent += ValuesValidation;
        _controlParamsView.OnAcceptClickEvent += OnComplete;
        _controlParamsView.OnCloseClickEvent += OnClose;

        await _completionSource.Task;

        _controlParamsView.OnCloseClickEvent -= OnClose;
        _controlParamsView.OnAcceptClickEvent -= OnComplete;
        _controlParamsView.ValueChangedEvent -= ValuesValidation;
        _controlParamsView.ItemsClear();
        _controlParamsView.Close();
    }

    private void ValuesValidation() =>
        _controlParamsView.ButtonInteractible(_controlParams.All(param => param.is_complete != false));

    private void OnComplete() =>
        _completionSource.TrySetResult();

    private void OnClose() =>
        _completionSource.TrySetCanceled();

}