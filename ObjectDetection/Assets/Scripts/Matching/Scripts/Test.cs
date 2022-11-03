using UnityEngine;

public class Test : MonoBehaviour {
    [SerializeField] private Popup popup;
    [SerializeField] private DBServices dbServices;
    [SerializeField] private TaskHandler taskHandler;

    [SerializeField] private OperationsHandler operationsHandler;

    private void Awake(){
        popup.Initialize();
        dbServices.Initialize();
    }

    private async void Start(){
        var workLogId = await taskHandler.SelectWorkLogIdProcess("Asset_PU21VAC");
        await operationsHandler.InputProcessCycle(workLogId);
    }
}
