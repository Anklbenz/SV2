using System;
using UnityEngine;
using UnityEngine.UI;

public class PhotoItem : MonoBehaviour {
   [SerializeField] private RawImage image;
   [SerializeField] private Button button;
   
   public event Action<Texture2D> ImageClickEvent;
   private Texture2D _texture;
   public void Initialize(Texture2D imageTexture){
      _texture = imageTexture;
      SetExpandedSize(imageTexture.width, imageTexture.height);

      image.texture = imageTexture;
   }

   private void SetExpandedSize(int width, int height){
      var parentSize = image.rectTransform.sizeDelta;

      if (width > height){
         var aspectRatio = (float)width / height;
         parentSize = new Vector2(parentSize.x * aspectRatio, parentSize.y);
      }
      else{
         var aspectRatio = (float)height / width;
         parentSize = new Vector2(parentSize.x, parentSize.y * aspectRatio);
      }

      image.rectTransform.sizeDelta = parentSize;
   }

   private void OnEnable() =>
      button.onClick.AddListener(delegate { ImageClickEvent?.Invoke(_texture); });

   private void OnDisable() =>
      button.onClick.RemoveAllListeners();
}