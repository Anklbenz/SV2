using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite mainSprite, changedSprite;
    [Ser]
    protected void SetSprite(Sprite sprite, Color color){
        statusImage.color = color;
        statusImage.sprite = sprite;
    }
}
