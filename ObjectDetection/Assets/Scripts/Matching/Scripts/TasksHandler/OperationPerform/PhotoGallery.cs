using UnityEngine;

public class PhotoGallery : MonoBehaviour {
    [SerializeField] private PhotoGalleryView photoGalleryView;
    [SerializeField] private P
    public void Add(Texture2D photoTexture){
        _photoGalleryView.Add(photoTexture);
        
    }

    public void Remove(){
        
    }

    public void ApplyChanges(){
        
    }
    
    private void OnImageSelect(){}
}