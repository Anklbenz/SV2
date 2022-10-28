using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Entry : MonoBehaviour {
    [SerializeField] private ARCameraManager arCameraManager;
    [SerializeField] private ObjectDataLoader objectDataLoader;
    [SerializeField] private ObjectCreationHandler objectCreationHandler;
    [SerializeField] private Instantiator instantiator;
    [SerializeField] private Popup popup;

    private CameraConfigurationChanger _cameraConfigurationChanger;
    private MainCycle _mainCycle;

    private void Awake(){
        popup.Initialize();
        _cameraConfigurationChanger = new CameraConfigurationChanger(arCameraManager);
  //      _cameraConfigurationChanger.ResolutionSuccessfullyChangedEvent += Initialize;
    }

    /*private async void Initialize(){
        await Popup.instance.dialog.AwaitResult("На основании заявки \"MR00000273 - Аврийный обход\" Успешно создан обход \"DO0000161\" Приступить к выполнению.");
        var data = await objectDataLoader.Load();
 
        if (data == null){
            await Popup.instance.warning.AwaitResult("Ошибка загрузки файла");
            return;
        }

        var arCameraResolution = _cameraConfigurationChanger.GetCurrentResolution();
        Debug.Log(arCameraResolution);
        objectCreationHandler.Initialize(arCameraManager, arCameraResolution, data);
        instantiator.Initialize(data);

        _mainCycle = new MainCycle(objectCreationHandler, instantiator);

        _mainCycle.DetectPhase();
    }*/
}
