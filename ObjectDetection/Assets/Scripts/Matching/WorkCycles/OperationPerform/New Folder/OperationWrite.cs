using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;
using Enums;

public class OperationWrite {
    private ControlParamsWrite _controlParamsWrite;
    private OperationView _operationView;
    private Operation _operation;
    private UniTaskCompletionSource _completionSource;
    private bool controlParamsRequired => _operation.control_params_count > 0;
    private bool photoRequired => _operation.is_foto_required;

    public OperationWrite (OperationView operationView, ControlParamsWrite controlParamsWrite){
        _operationView = operationView;
        _operationView.Initialize();
        _operationView.ParamsClickedEvent += AddParams;
        _operationView.AcceptClickedEvent += OnAccept;
        
        _controlParamsWrite = controlParamsWrite;
        _controlParamsWrite.DataChangedEvent += OnParamChanged;
    }

    public async UniTask Process(Operation operation){
        _operation = operation;
        
        if(controlParamsRequired)
            _controlParamsWrite.Initialize(_operation.userOperationData.ControlParams);
        
        _operationView.Show(operation);
        
        _completionSource = new UniTaskCompletionSource();
        await _completionSource.Task;
    }
    
    private void OnParamChanged(){
       _operationView.ParamUpdate(_controlParamsWrite.GetState());
       _operationView.AcceptAvailable(_controlParamsWrite.GetState());
    }
    
    private void OnAccept() =>
        _completionSource.TrySetResult();


    private async void AddParams(){
        await _controlParamsWrite.WriteProcess();
    }

    private async void AddNote(){

    }

    private async void AddPhoto(){

    }
}