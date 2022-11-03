using UnityEngine;

public class UIScaler 
{
    public float GetScale(Vector2Int arCamResolution){
        var displayX = Screen.currentResolution.width;
        var displayY = Screen.currentResolution.height;
        var minScreenSide = displayX > displayY ? displayY : displayX;  

        return (float)  minScreenSide/ arCamResolution.y;
    }
}
