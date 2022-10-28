
using UnityEngine;

public class Popup : MonoBehaviour {
    [SerializeField] private PopupView popupView;
    
    public Dialog dialog;
    public Warning warning;
    public Comment comment;

    public static Popup instance{ get; private set; }
    public void Initialize(){
        instance = this;
        
        dialog.Initialize(popupView);
        warning.Initialize(popupView);
        comment.Initialize(popupView);
    }
}
