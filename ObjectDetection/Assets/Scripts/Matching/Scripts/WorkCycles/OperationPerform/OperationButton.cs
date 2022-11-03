using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OperationButton : MonoBehaviour {
    [SerializeField] private TMP_Text labelText;
    [SerializeField] private Button button;

    private int _index;
    
    public void Initialize(string label, int arrayIndex){
        _index = arrayIndex;
        labelText.text = label;
    }

    public void SetActive(){
        
    }

    public void SetCompleted(){
        
    }
}
