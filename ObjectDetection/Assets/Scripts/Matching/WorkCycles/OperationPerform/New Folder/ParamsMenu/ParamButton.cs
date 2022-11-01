using System;
using Enums;
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
    
    public void SetParamButtonState(ParamState state){
        switch (state){
            case ParamState.Clear:
                SetClear();
                break;
            case ParamState.Warning:
                SetWarning();
                break;
            case ParamState.Complete:
                SetComplete();
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, "Param button state dont exist");
        }
    }

    private void SetSprite(Sprite sprite, Color color){
        statusImage.color = color;
        statusImage.sprite = sprite;
    }
}