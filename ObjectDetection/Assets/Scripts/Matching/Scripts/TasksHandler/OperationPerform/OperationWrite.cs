using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;
using Enums;

public class OperationWrite {
    private readonly ControlParamsWrite _controlParamsWrite;
    private readonly PhotoHandler _photoHandler;
    private readonly OperationView _operationView;

    private Operation _operation;
    private UniTaskCompletionSource _completionSource;

    private bool controlParamsRequired => _operation.control_params_count > 0;
    private bool photoRequired => _operation.is_foto_required;

    public OperationWrite(OperationView operationView, ControlParamsWrite controlParamsWrite, PhotoHandler photoHandler){
        _operationView = operationView;

        _operationView.AcceptClickedEvent += OnOperationAccept;
        _operationView.ParamsClickedEvent += AddParams;
        _operationView.PhotoClickedEvent += AddPhoto;

        _controlParamsWrite = controlParamsWrite;
        _controlParamsWrite.OnAcceptParams += OnParamsAccepted;

        _photoHandler = photoHandler;
    }

    public async UniTask Process(Operation operation){
        _operation = operation;

        _operationView.ReadyToNext(IsOperationReady());
        _operationView.Show(operation);

        _completionSource = new UniTaskCompletionSource();
        await _completionSource.Task;
    }

    private void OnParamsAccepted(){
        _operationView.ParamUpdate(_controlParamsWrite.GetState());
        _operationView.ReadyToNext(IsOperationReady());
    }

    private void OnOperationAccept(){
        _completionSource.TrySetResult();
    }

    private bool IsOperationReady(){
        if (controlParamsRequired)
            if (_controlParamsWrite.GetState() != ParamState.Complete)
                return false;
        if (photoRequired)
            if (_operation.userOperationData.Textures == null)
                return false;

        return true;
    }

    private async void AddParams(){
        await _controlParamsWrite.WriteProcess(_operation.userOperationData.ControlParams);
    }

    private async void AddPhoto(){
        await _photoHandler.TakePhotosProcess(_operation.userOperationData.Textures);
    }
    
    private async void AddNote(){

    }
}