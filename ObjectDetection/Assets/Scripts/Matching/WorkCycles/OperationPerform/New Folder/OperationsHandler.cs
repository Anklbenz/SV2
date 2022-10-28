using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class OperationsHandler : MonoBehaviour {
    [SerializeField] private ControlParamsHandler controlParamsHandler;
    [SerializeField] private OperationView view;

    private readonly OperationShow _operationShow = new();
    private readonly OperationWrite _operationWrite = new();

    private Operation[] _operations;

    public async UniTask InputProcessCycle(int workLogId){
        _operations = await DBServices.instance.GetOperationsWithControlParams(workLogId);

        controlParamsHandler.Initialize();
        view.operationList.Initialize(_operations);
        
        _operationWrite.Initialize(view, controlParamsHandler.controlParamsInput);
        
        view.Open();
        
        foreach (var operation in _operations)
            await _operationWrite.Process(operation);
    }
}
