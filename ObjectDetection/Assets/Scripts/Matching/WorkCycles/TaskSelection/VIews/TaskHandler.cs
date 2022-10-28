using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TaskHandler : MonoBehaviour {
    private const string REQUEST_TYPE = "REQUEST";
    private const string WORK_LOG_TYPE = "WORK_LOG";
    private const string WORK_LOG_MSG_4 = "Приступить к выполнению обхода";
    private const string WORK_LOG_MSG_3 = "На основании заявки создан обход:";
    private const string WORK_LOG_CREATE_MSG = "Создать обход на основании заявки";
   
    [SerializeField] private TasksView taskView;
    private UniTaskCompletionSource<int> _selectTaskCompletionSource;
    private TaskData[] _tasksData;

    public async UniTask<int> SelectWorkLogIdProcess(string assetName){
        _tasksData = await GetTasksByAsset(assetName);

        taskView.Initialize(_tasksData);
        taskView.OnCloseEvent += OnClose;
        taskView.OnSelectEvent += OnSelect;
        taskView.Open();

        _selectTaskCompletionSource = new UniTaskCompletionSource<int>();
        var result = await _selectTaskCompletionSource.Task;

        taskView.OnCloseEvent -= OnClose;
        taskView.OnSelectEvent -= OnSelect;
        taskView.Close();

        return result;
    }

    private async UniTask<TaskData[]> GetTasksByAsset(string asset){
        return await DBServices.instance.RequestAndWorkLogGetView(asset);
    }

    private async void OnSelect(int index){
        var task = _tasksData[index];

        switch (task.row_type_code){
            case WORK_LOG_TYPE:{
                var performDecision = await Popup.instance.dialog.AwaitResult($"{WORK_LOG_MSG_4}\n\"{task.row_code}\"");

                if (performDecision) _selectTaskCompletionSource.TrySetResult(task.work_log_id);
                break;
            }
            case REQUEST_TYPE:{
                var createWokLogDecision = await Popup.instance.dialog.AwaitResult($"{WORK_LOG_CREATE_MSG} \n{task.row_code}-{task.row_comment}?");
                if (!createWokLogDecision) return;
          
                var createdWorkLogData = await DBServices.instance.WorkLogCreateFromRequest(task.request_id);
                var performDecision = await Popup.instance.dialog.AwaitResult($"{WORK_LOG_MSG_3}:\n\"{createdWorkLogData.code}\"\n{WORK_LOG_MSG_4}?\n");
                if (performDecision)
                    _selectTaskCompletionSource.TrySetResult(task.work_log_id);
                break;
            }
        }
    }

    private void OnClose(){
        _selectTaskCompletionSource.TrySetCanceled();
    }
}