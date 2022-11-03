using UnityEngine;
using UnityEngine.UI;
using Action = OpenCvSharp.Util.Action;

public class PhotoItem : MonoBehaviour {
   [SerializeField] private RawImage image;
   [SerializeField] private Button button;

   public event Action ImageClickEvent;

   public void Initialize(Texture2D imageTexture){
      image.texture = imageTexture;
   }

   private void OnEnable(){
      button.onClick.AddListener(delegate { ImageClickEvent?.Invoke(); });
   }

   private void OnDisable(){
      button.onClick.RemoveAllListeners();
   }
}
