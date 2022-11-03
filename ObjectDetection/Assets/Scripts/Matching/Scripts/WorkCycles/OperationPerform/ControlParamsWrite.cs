using System;
using System.Linq;
using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;
using Enums;
using UnityEngine;

public class ControlParamsWrite {
    public event Action OnAcceptParams;

    private readonly ControlParamsView _controlParamsView;
    private UniTaskCompletionSource _completionSource;
    private ControlParam[] _controlParams;
    private bool _isReady;

    public ControlParamsWrite(ControlParamsView view) =>
        _controlParamsView = view;

    private void Initialize(ControlParam[] controlParams){
        _isReady = false;
        _controlParams = controlParams;
        _controlParamsView.ItemsClear();
        _controlParamsView.AddItems(controlParams);
        _controlParamsView.AcceptInteractable(false);
    }

    public async UniTask WriteProcess(ControlParam[] controlParams){
        Initialize(controlParams);
        _completionSource = new UniTaskCompletionSource();
        
        _controlParamsView.Open();
        _controlParamsView.ValueChangedEvent += OnDataChanged;
        _controlParamsView.OnAcceptClickEvent += OnAccept;
        _controlParamsView.OnCloseClickEvent += OnClose;
       
        try{
            await _completionSource.Task;
        }
        catch(OperationCanceledException oe){
            Debug.Log("Params Enter Canceled");
        }

        _controlParamsView.OnCloseClickEvent -= OnClose;
        _controlParamsView.OnAcceptClickEvent -= OnAccept;
        _controlParamsView.ValueChangedEvent -= OnDataChanged;
        _controlParamsView.Close();
    }

    public ParamState GetState(){
        return _isReady ? ParamState.Complete : ParamState.Warning;
    }

    private void OnDataChanged(){
        _isReady = IsReadyCheck();
        _controlParamsView.AcceptInteractable(_isReady);
    }

    private bool IsReadyCheck() =>
        _controlParams.All(param => param.is_complete != false);

    private void OnAccept(){
        _completionSource.TrySetResult();
        OnAcceptParams?.Invoke();
    }

    private void OnClose() =>
        _completionSource.TrySetCanceled();
}