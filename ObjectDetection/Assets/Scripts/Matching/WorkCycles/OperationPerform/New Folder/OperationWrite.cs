using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;

public class OperationWrite {
    private ControlParamsInput _controlParamsInput;
    private UniTaskCompletionSource _completionSource;

    private OperationView _view;
    private Operation _operation;
    
    public void Initialize(OperationView view, ControlParamsInput controlParamsInput){
        _controlParamsInput = controlParamsInput;
       
        _view = view;
        _view.paramsMenu.ParamsClickedEvent += AddParams;
        _view.acceptButton.onClick.AddListener(OnAcceptClick);
    }

    public async UniTask Process(Operation operation){
        _operation = operation;
        _completionSource = new UniTaskCompletionSource();

        _view.paramsMenu.SetActiveButtons(operation.control_params_count > 0);
        _view.info.Show(_operation.name, _operation.description);

        await _completionSource.Task;
    }

    private void OnAcceptClick(){
        _completionSource.TrySetResult();
    }

    private async void AddParams(){
        await _controlParamsInput.InputProcess(_operation.userOperationData.ControlParams);
    }

    private async void AddNote(){
        
    }

    private async void AddPhoto(){
        
    }
    
}
