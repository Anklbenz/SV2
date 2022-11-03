using UnityEngine;
using Cysharp.Threading.Tasks;

public class JsonRequestSender : DatabaseClient {

    //T request result, TU param class
    public async UniTask<T> GetServiceResult<T, TU>(string url, string authorKey, TU param = null) where T : class where TU : class{
        var operationIdParamInJson = JsonUtility.ToJson(param);
        var json = await GetResponse(url, authorKey, operationIdParamInJson);

        return json != null ? JsonUtility.FromJson<T>(json) : default;
    }

    protected async UniTask<T> GetServiceResult<T>(string url, string authorKey) where T : class{
        var json = await GetResponse(url, authorKey);
        return json != null ? JsonUtility.FromJson<T>(json) : default;
    }

    public async UniTask<bool> GetService<TU>(string url, string authorKey, TU param = null) where TU : class{
        var operationIdParamInJson = JsonUtility.ToJson(param);
        var json = await GetResponse(url, authorKey, operationIdParamInJson);

        return json != null;
    }
}