using ClassesForJsonDeserialize;
using UnityEngine;

[System.Serializable]
public class OperationShow {
    [SerializeField] private InfoView infoView;
    [SerializeField] private ParamButton paramsButton;

    private ControlParamsShow _controlParamsShow;
    private Operation _operation;

    public void Initialize(ControlParamsShow controlParamsShow){
        _controlParamsShow = controlParamsShow;
        infoView.Show(_operation.name, _operation.description);
        paramsButton.button.onClick.AddListener(ShowParams);
    }
    
    private async void ShowParams(){
        await _controlParamsShow.Process(_operation.userOperationData.ControlParams);
    }

    private async void ShowNote(){

    }

    private async void ShowPhoto(){

    }
}
