using UnityEngine;

[CreateAssetMenu]
public class PopupSettings : ScriptableObject {
    public string headerText, contentText, placeholderText, button1Text, button2Text, button3Text;
    public bool acceptButtonEnable, rejectButtonEnable, additionalButtonEnable, inputVisible;
    public float canvasWidth, canvasHeight;
    
}
