using ClassesForJsonDeserialize;
using UnityEngine;


public class ControlParamsHandler : MonoBehaviour {
    [SerializeField] private ControlParamsView view;

    public readonly ControlParamsInput controlParamsInput = new();
    public readonly ControlParamsShow controlParamsShow = new();

    public void Initialize(){
        controlParamsInput.Initialize(view);
        controlParamsShow.Initialize(view);
    }
}