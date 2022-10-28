using System;
using Cysharp.Threading.Tasks;
using RequestParamClasses;
using ClassesForJsonDeserialize;
using UnityEngine;

[System.Serializable]
public class DBServices : JsonRequestSender {
    private const string CP_DROP_DOWN_CODE = "CP_DROP_DOWN";
    
    private const string GET_ASSET_TAG_VALUES_INFO = "GetAssetTagValuesInfo";
    private const string GET_ASSET_TAG_VALUES = "GetAssetTagValues";
    private const string GET_CURRENT_USER_INFO = "GetCurrentUserInfo";
    private const string GET_OPERATION_STATUSES = "GetOperationStatuses";

    private const string REQUEST_AND_WORK_LOG_GET_VIEW = "RequestAndWorkLog_GetView";

    private const string WORK_LOG_APPEND_OPERATIONS_FROM_WORK_INSTRUCTION = "WorkLog_AppendOperationsFromWorkInstruction";
    private const string WORK_LOG_CREATE_FROM_REQUEST = "WorkLog_CreateFromRequest";
    private const string WORK_LOG_GET_INFO = "WorkLog_GetInfo";
    private const string WORK_LOG_GET_OPERATION = "WorkLog_GetOperations";
    private const string WORK_LOG_GET_SELECTED_CP_FROM_LAST_OPERATION = "WorkLog_GetSelectedCPFromLastOperation";
    private const string WORK_LOG_SAVE_COMMENT = "WorkLog_SaveComment";

    private const string WORK_LOG_OPERATION_SAVE_FOTO = "WorkLogOperation_SaveFoto";
    private const string WORK_LOG_OPERATION_SET_PAUSED = "WorkLogOperation_SetPaused";
    private const string WORK_LOG_OPERATION_SET_COMPLETED = "WorkLogOperation_SetCompleted";
    private const string WORK_LOG_OPERATION_GET_CONTROL_PARAMS = "WorkLogOperation_GetControlParams";
    private const string WORK_LOG_OPERATION_GET_DROPDOWN_ROWS = "WorkLogOperation_GetDropDownRowsForControlParam";
    private const string WORK_LOG_OPERATION_SAVE_CONTROL_PARAM = "WorkLogOperation_SaveControlParam";
    private const string WORK_LOG_OPERATION_CHECK_CONTROL_PARAMS_AND_FOTO = "WorkLogOperation_CheckControlParamsAndFoto";

    private const string WORK_INSTRUCTION_GET_INFO = "WorkInstruction_GetInfo";
    private const string WORK_INSTRUCTION_OPERATION_GET_CONTROL_PARAMS = "WorkInstructionOperation_GetControlParams";
    private const string WORK_INSTRUCTION_OPERATION_GET_DROPDOWN_ROWS = "WorkInstructionOperation_GetDropDownRowsForControlParam";

    [SerializeField] private ConnectionSettings settings;
    public static DBServices instance{ get; private set; }  
    
    private RequestParamsCreator _paramCreator;
    private string _thingUrl, _authorKey;
    
    public void Initialize(){
        instance = this;

        _thingUrl = settings.hostUrl + settings.servicesUrl;
        _authorKey = settings.authorizationKey;

        _paramCreator = new RequestParamsCreator();
    }

    public async UniTask<OperationStatusesArray> GetOperationStatuses(){
        return await GetServiceResult<OperationStatusesArray>(_thingUrl + GET_OPERATION_STATUSES, _authorKey);
    }

    public async UniTask<Operation[]> WorkLogGetOperations(int workLogId){
        var workLogIdClass = _paramCreator.WorkLogIdCreate(workLogId);
        var operations = await GetServiceResult<Operations, WorkLogIdParam>(_thingUrl + WORK_LOG_GET_OPERATION, _authorKey, workLogIdClass);
        return operations != null ? operations.rows : new Operation[] { new Operation() };
    }

    public async UniTask<ControlParam[]> WorkInstructionOperationGetControlParams(int workInstructionOperationId){
        var workLogOperationIdClass = _paramCreator.WorkInstructionOperationIdCreate(workInstructionOperationId);
        var parameters = await GetServiceResult<ControlParams, WorkInstructionOperationIdParam>(_thingUrl + WORK_INSTRUCTION_OPERATION_GET_CONTROL_PARAMS, _authorKey, workLogOperationIdClass);
        return parameters != null ? parameters.rows : throw new ArgumentException("params cannot be null");
    }

    public async UniTask<ControlParamDropDownRow[]> WorkInstructionOperationGetDropDownRowsForControlParams(int workInstructionOperationCpId){
        var workInstructionOperationCpIdClass = _paramCreator.WorkInstructionOperationCpIdCreate(workInstructionOperationCpId);
        var parameters = await GetServiceResult<ControlParamDropDownRows, WorkInstructionOperationCpIdParam>(_thingUrl + WORK_INSTRUCTION_OPERATION_GET_DROPDOWN_ROWS, _authorKey,
            workInstructionOperationCpIdClass);
        return parameters != null ? parameters.rows : throw new ArgumentException("Drop down params cannot be null");
    }

    public async UniTask<TaskData[]> RequestAndWorkLogGetView(string assetName){
        var assetNameParam = _paramCreator.ThingNameCreate(assetName);
        var tasksList = await GetServiceResult<ClassesForJsonDeserialize.TasksList, ThingParam>(_thingUrl + REQUEST_AND_WORK_LOG_GET_VIEW, _authorKey, assetNameParam);
        return tasksList != null ? tasksList.rows : throw new ArgumentException("TasksList cannot be null");
    }

    public async UniTask<TagValues> GetAssetTagValues(string assetName){
        var assetNameParam = _paramCreator.ThingNameCreate(assetName);
        return await GetServiceResult<TagValues, ThingParam>(_thingUrl + GET_ASSET_TAG_VALUES, _authorKey, assetNameParam);
    }

    public async UniTask<TagValue[]> GetAssetTagValuesInfo(string assetName){
        var assetNameParam = _paramCreator.ThingNameCreate(assetName);
        var sensorsData = await GetServiceResult<SensorsData, ThingParam>(_thingUrl + GET_ASSET_TAG_VALUES_INFO, _authorKey, assetNameParam);
        return sensorsData != null ? sensorsData.rows : throw new ArgumentException("Sensor Data cannot be null");
    }

    public async UniTask<CreatedWorkLog> WorkLogCreateFromRequest(int requestId){
        var requestIdParam = _paramCreator.RequestIdCreate(requestId);
        var createdWorkLogs = await GetServiceResult<CreatedWorkLogs, RequestId>(_thingUrl + WORK_LOG_CREATE_FROM_REQUEST, _authorKey, requestIdParam);
        return createdWorkLogs != null ? createdWorkLogs.rows[0] : throw new ArgumentException("Created WorkLog Data cannot be null");
    }

    public async UniTask<WorkLogInfo> WorkLogGetInfo(int workLogId){
        var workLogIdParam = _paramCreator.WorkLogIdCreate(workLogId);
        var getInfoArray = await GetServiceResult<GetInfoArray, WorkLogIdParam>(_thingUrl + WORK_LOG_GET_INFO, _authorKey, workLogIdParam);
        return getInfoArray != null ? getInfoArray.rows[0] : throw new ArgumentException("WorkLogGetInfo cannot be null");
    }

    public async UniTask<WorkInstructionInfo> WorkInstructionGetInfo(int workInstructionId){
        var workInstructionIdParam = _paramCreator.WorkInstructionIdCreate(workInstructionId);
        var getInfoArray = await GetServiceResult<WorkInstructionInfoRows, WorkInstructionIdParam>(_thingUrl + WORK_INSTRUCTION_GET_INFO, _authorKey, workInstructionIdParam);
        return getInfoArray != null ? getInfoArray.rows[0] : throw new ArgumentException("WorkInstructionGetInfo cannot be null");
    }

    public async UniTask<SelectedCPFromLastOperation> WorkLogGetSelectedCPFromLastOperation(int workLogId){
        var workLogIdParam = _paramCreator.WorkLogIdCreate(workLogId);
        var selectedCpFromLastOperationArray =
            await GetServiceResult<SelectedCPFromLastOperationArray, WorkLogIdParam>(_thingUrl + WORK_LOG_GET_SELECTED_CP_FROM_LAST_OPERATION, _authorKey, workLogIdParam);
        return selectedCpFromLastOperationArray.rows[0] ?? throw new ArgumentException("Selected CP from last operation is NULL");
    }

    public async UniTask<Status> WorkLogAppendOperationsFromWorkInstruction(int workLogId, int workInstructionId){
        var appendOperationFromWorkInstructionParam = _paramCreator.WorkLogIdWorkInstructionIdCreate(workLogId, workInstructionId);
        var status = await GetServiceResult<Status, WorkLogIdWorkInstructionIdParam>(_thingUrl + WORK_LOG_APPEND_OPERATIONS_FROM_WORK_INSTRUCTION, _authorKey, appendOperationFromWorkInstructionParam);
        return status;
    }

    public async UniTask<ControlParam[]> WorkLogOperationGetControlParams(int workLogOperationId){
        var workLogOperationIdClass = _paramCreator.WorkLogOperationIdCreate(workLogOperationId);
        var parameters =
            await GetServiceResult<ControlParams, WorkLogOperationIdParam>(_thingUrl + WORK_LOG_OPERATION_GET_CONTROL_PARAMS, _authorKey, workLogOperationIdClass);
        return parameters != null ? parameters.rows : throw new ArgumentException("params cannot be null");
    }

    public async UniTask<WorkLogOperationDropDownRow[]> WorkLogOperationGetDropDownRowsForControlParam(int workLogOperationCpId){
        var workLogOperationIdClass = _paramCreator.WorkLogOperationCpIdCreate(workLogOperationCpId);
        var parameters =
            await GetServiceResult<WorkLogOperationDropDownRows, WorkLogOperationCpIdParam>(_thingUrl + WORK_LOG_OPERATION_GET_DROPDOWN_ROWS, _authorKey, workLogOperationIdClass);
        return parameters != null ? parameters.rows : throw new ArgumentException("params cannot be null");
    }

    public async UniTask<Status> WorkLogOperationSetCompleted(int workLogOperationId){
        var workLogOperationIdClass = _paramCreator.WorkLogOperationIdCreate(workLogOperationId);
        return await GetServiceResult<Status, WorkLogOperationIdParam>(_thingUrl + WORK_LOG_OPERATION_SET_COMPLETED, _authorKey, workLogOperationIdClass);
    }

    public async UniTask<Status> WorkLogOperationSetPaused(int workLogOperationId){
        var workLogOperationIdClass = _paramCreator.WorkLogOperationIdCreate(workLogOperationId);
        return await GetServiceResult<Status, WorkLogOperationIdParam>(_thingUrl + WORK_LOG_OPERATION_SET_PAUSED, _authorKey, workLogOperationIdClass);
    }

    public async UniTask<Status> WorkLogOperationSaveControlParam(ControlParamForSave saveParamSavedData){
        return await GetServiceResult<Status, ControlParamForSave>(_thingUrl + WORK_LOG_OPERATION_SAVE_CONTROL_PARAM, _authorKey, saveParamSavedData);
    }

    public async UniTask<Status> WorkLogOperationSaveFoto(int workLogOperationId, string base64String){
        var workLogOperationSaveFotoParam = _paramCreator.WorkLogOperationSaveFotoCreate(workLogOperationId, base64String);
        var status = await GetServiceResult<Status, WorkLogOperationSaveFotoParam>(_thingUrl + WORK_LOG_OPERATION_SAVE_FOTO, _authorKey, workLogOperationSaveFotoParam);
        return status;
    }

    public async UniTask<SavePhotoStatus> WorkLogOperationCheckControlParamsAndFoto(int workLogOperationId){
        var workLogOperationIdParam = _paramCreator.WorkLogOperationIdCreate(workLogOperationId);
        var status = await GetServiceResult<SavePhotoStatus, WorkLogOperationIdParam>(_thingUrl + WORK_LOG_OPERATION_CHECK_CONTROL_PARAMS_AND_FOTO, _authorKey, workLogOperationIdParam);
        return status;
    }

    public async UniTask WorkLogSaveComment(int workLogId, string comment){
        var saveCommentParam = _paramCreator.WorkLogSaveCommentCreate(workLogId, comment);
        await GetServiceResult<Status, WorkLogSaveCommentParam>(_thingUrl + WORK_LOG_SAVE_COMMENT, _authorKey, saveCommentParam);
    }

    public async UniTask<UserInfo> GetUserInfo(){
        return await GetServiceResult<UserInfo>(_thingUrl + GET_CURRENT_USER_INFO, _authorKey);
    }
    
    public async UniTask<Operation[]> GetOperationsWithControlParams(int workLogId){
        var operations = await WorkLogGetOperations(workLogId);
        foreach (var operation in operations){
            if (operation.control_params_count > 0)
                await InitializeControlParams(operation);
        }

        return operations;
    }

    private async UniTask InitializeControlParams(Operation operation){
        operation.userOperationData.ControlParams = await WorkLogOperationGetControlParams(operation.id);
        if (operation.userOperationData.ControlParams != null)
            await InitializeDropDown(operation.userOperationData.ControlParams);
    }

    private async UniTask InitializeDropDown(ControlParam[] controlParams){
        foreach (var controlParam in controlParams)
            if (controlParam.operation_cp_type_code == CP_DROP_DOWN_CODE)
                controlParam.DropDownRows = await WorkLogOperationGetDropDownRowsForControlParam(controlParam.id);
    }
}