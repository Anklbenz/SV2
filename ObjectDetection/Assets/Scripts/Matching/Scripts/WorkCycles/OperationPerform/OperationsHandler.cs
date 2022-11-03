using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class OperationsHandler : MonoBehaviour {
    [SerializeField] private ControlParamsHandler controlParamsHandler;
    [SerializeField] private PhotoHandler photoHandler;
    [SerializeField] private OperationView view;

    private readonly OperationShow _operationShow = new();

    private OperationWrite _operationWrite;

    public async UniTask InputProcessCycle(int workLogId){
        _operationWrite = new OperationWrite(view, controlParamsHandler.controlParamsWrite, photoHandler);
        var operations = await DBServices.instance.GetOperationsWithControlParams(workLogId);

        view.operationList.AddOperations(operations);
        view.Open();

        foreach (var operation in operations)
            await _operationWrite.Process(operation);

        view.Close();
    }
}
