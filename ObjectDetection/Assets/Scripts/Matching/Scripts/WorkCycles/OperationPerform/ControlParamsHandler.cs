using UnityEngine;


public class ControlParamsHandler : MonoBehaviour {
    [SerializeField] private ControlParamsView view;

    public ControlParamsWrite controlParamsWrite;
    public readonly ControlParamsShow controlParamsShow = new();

    private void Awake(){
        controlParamsWrite = new ControlParamsWrite(view);
       
    }
}