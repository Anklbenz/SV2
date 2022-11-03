using UnityEngine;
using UnityEngine.UI;

public class FrameView {
    private RawImage _templateImage;
    public void SetView(RawImage image, Texture2D texture, float uiScale){
        _templateImage = image;
        image.rectTransform.localScale = new Vector3(uiScale, uiScale, 0);
        image.rectTransform.sizeDelta = new Vector2Int(texture.width, texture.height);
        image.texture = SetBlackTransparent(texture);
        image.enabled = true;
    }
    
    public void Show(){
        _templateImage.enabled = true;
    }

    public void Hide(){
        _templateImage.enabled = false;
    }
    
    private Texture2D SetBlackTransparent(Texture2D texture){
        var pixels = texture.GetPixels();

        for (var i = 0; i < pixels.Length; ++i)
            if (pixels[i] == Color.black)
                pixels[i] = Color.clear;

        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
    
}
