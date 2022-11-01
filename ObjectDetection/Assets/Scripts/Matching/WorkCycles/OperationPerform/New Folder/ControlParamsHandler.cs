using ClassesForJsonDeserialize;
using UnityEngine;


public class ControlParamsHandler : MonoBehaviour {
    [SerializeField] private ControlParamsView view;

    public ControlParamsWrite controlParamsWrite;
    public readonly ControlParamsShow controlParamsShow = new();

    public void Initialize(){
        controlParamsWrite = new ControlParamsWrite(view);
        controlParamsShow.Initialize(view);
    }
}