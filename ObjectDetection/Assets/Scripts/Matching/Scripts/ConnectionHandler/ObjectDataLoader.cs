using ClassesForJson;
using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class ObjectDataLoader {
    [SerializeField] private ConnectionSettings connectionSettings;
    private JsonRequestSender _jsonRequestSender = new();
    private DataConverter _dataConverter = new();

    /*public async UniTask<Data> Load(){

       // var result = await _jsonRequestSender.GetServiceResult<ServerData, Id>(connectionSettings, new Id() { id = 1 });
        return result == null ? null : _dataConverter.ServerDataToData(result);
    }*/
}
