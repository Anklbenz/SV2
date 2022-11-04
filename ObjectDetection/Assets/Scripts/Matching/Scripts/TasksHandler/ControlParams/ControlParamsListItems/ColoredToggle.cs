using UnityEngine;
using UnityEngine.UI;
using Action = OpenCvSharp.Util.Action;

public class ColoredToggle : MonoBehaviour {
    [SerializeField] private Color isOnColor, isOffColor, indefiniteColor;
    [SerializeField] private Image circleImage;
    public event Action ValueChangedEvent;
    public Toggle toggle;
    
    private void Awake(){
        circleImage.color = indefiniteColor;
        toggle.onValueChanged.AddListener(OnValueChange);
    }

    public void SetState(bool isOn){
        circleImage.color = isOn ? isOnColor : isOffColor;
    }
    
    private void OnValueChange(bool isOn){
        SetState(toggle.isOn);
        ValueChangedEvent?.Invoke();
    }
}
