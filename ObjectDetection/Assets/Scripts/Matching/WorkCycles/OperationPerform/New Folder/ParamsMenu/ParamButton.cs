using UnityEngine;
using UnityEngine.UI;

public class ParamButton : MonoBehaviour {
    [SerializeField] private Image statusImage;
    [SerializeField] private Sprite warningSprite, completedSprite;
    [SerializeField] private Color waringColor, completeColor;
    
    public Button button;
    
    public ParamButton SetActive(bool state){
        gameObject.SetActive(state);
        return this;
    }

    public void SetWarning(){
        SetSprite(warningSprite, waringColor);
    }

    public void SetComplete(){
        SetSprite(completedSprite, completeColor);
    }

    public void SetClear(){
        SetSprite(null, Color.clear);
    }

    private void SetSprite(Sprite sprite, Color color){
        statusImage.color = color;
        statusImage.sprite = sprite;
    }
}