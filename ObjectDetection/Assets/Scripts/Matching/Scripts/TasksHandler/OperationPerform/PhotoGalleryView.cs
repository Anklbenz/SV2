using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PhotoGalleryView  {
    [Header("Gallery")]
    [SerializeField] private RectTransform imageParent;
    [SerializeField] private PhotoItem photoItemPrefab;
    
    public void Add(Texture2D texture){
        var item = UnityEngine.Object.Instantiate(photoItemPrefab, imageParent);
        item.Initialize(texture);
        item.ImageClickEvent += OnItemClicked;
    }
    
    public void Clear(){
        
    }

    private void OnItemClicked(){
        Debug.Log("Clicked");
    }
}
