using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class OperationsHandler : MonoBehaviour {
    [SerializeField] private ControlParamsHandler controlParamsHandler;
    [SerializeField] private PhotoHandler photoHandler;
    [SerializeField] private OperationView operationView;

    private readonly OperationShow _operationShow = new();

    private OperationWrite _operationWrite;

    public async UniTask InputProcessCycle(int workLogId){
        _operationWrite = new OperationWrite(operationView, controlParamsHandler.controlParamsWrite, photoHandler);
        var operations = await DBServices.instance.GetOperationsWithControlParams(workLogId);

        operationView.operationList.AddOperations(operations);
        operationView.Open();

        foreach (var operation in operations)
            await _operationWrite.Process(operation);

        operationView.Close();
    }
}
