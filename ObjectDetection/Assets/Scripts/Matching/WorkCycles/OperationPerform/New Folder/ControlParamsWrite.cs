using System.Linq;
using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;
using Enums;
using OpenCvSharp.Util;

public class ControlParamsWrite {
    public event Action DataChangedEvent;

    private readonly ControlParamsView _controlParamsView;
    private ControlParam[] _controlParams;
    private UniTaskCompletionSource _completionSource;
    private bool isReady{ get; set; }

    public ControlParamsWrite(ControlParamsView view) =>
        _controlParamsView = view;

    public void Initialize(ControlParam[] controlParams){
        _controlParams = controlParams;
        _controlParamsView.ItemsClear();
        _controlParamsView.AddItems(controlParams);
        _controlParamsView.AcceptInteractable(false);
    }

    public async UniTask WriteProcess(){
        _completionSource = new UniTaskCompletionSource();
        
        _controlParamsView.Open();
        _controlParamsView.ValueChangedEvent += OnDataChanged;
        _controlParamsView.OnAcceptClickEvent += OnComplete;
        _controlParamsView.OnCloseClickEvent += OnClose;

        await _completionSource.Task;

        _controlParamsView.OnCloseClickEvent -= OnClose;
        _controlParamsView.OnAcceptClickEvent -= OnComplete;
        _controlParamsView.ValueChangedEvent -= OnDataChanged;
        _controlParamsView.Close();
    }
    public ParamState GetState(){
        return isReady ? ParamState.Complete : ParamState.Warning;
    }

    private void OnDataChanged(){
        var isReadyCheckResult = IsReadyCheck();
        var readyStateChanged = isReady != isReadyCheckResult;

        isReady = isReadyCheckResult;
        _controlParamsView.AcceptInteractable(isReadyCheckResult);

        if (readyStateChanged)
            DataChangedEvent?.Invoke();
    }

    private bool IsReadyCheck() =>
        _controlParams.All(param => param.is_complete != false);

    private void OnComplete() =>
        _completionSource.TrySetResult();

    private void OnClose() =>
        _completionSource.TrySetCanceled();
}