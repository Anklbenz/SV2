using UnityEngine;

public class MainCycle {
    private readonly ObjectCreationHandler _objectCreationHandler;
    private readonly Instantiator _instantiator;
    
    public MainCycle(ObjectCreationHandler objectCreationHandler, Instantiator instantiator){
        _instantiator = instantiator;
        _objectCreationHandler = objectCreationHandler;
    }

    public void DetectPhase(){
        Debug.Log("Detect phase");
        _objectCreationHandler.Start();
        _objectCreationHandler.ObjectDetectedEvent += InstantiatePhase;
    }
    
    private void InstantiatePhase(){
        _objectCreationHandler.Stop();
        _instantiator.Create();
        TaskSelectPhase();
    }

    private void TaskSelectPhase(){
        Debug.Log("Task Select Phase ");
    }
}
