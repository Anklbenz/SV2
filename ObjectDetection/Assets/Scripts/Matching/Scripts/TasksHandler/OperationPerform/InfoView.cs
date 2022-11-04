using TMPro;
using UnityEngine;

[System.Serializable]
public class InfoView  {
    [SerializeField] private TMP_Text infoText;

    public void Show(string label, string description){
        infoText.text = "<b>"+label+"</b> " + description;
    }
}
