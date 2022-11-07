using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PhotoGalleryView {
    [SerializeField] private RectTransform imageParent;
    [SerializeField] private PhotoItem photoItemPrefab;
    
    public event Action<Texture2D> PhotoSelectedEvent;

    private List<PhotoItem> _items = new();
    public void Add(Texture2D texture){
        var item = UnityEngine.Object.Instantiate(photoItemPrefab, imageParent);
        item.Initialize(texture);
        item.ImageClickEvent += OnItemClicked;
        _items.Add(item);
    }

    public void Clear(){

    }

    private void OnItemClicked(Texture2D texture){
        PhotoSelectedEvent?.Invoke(texture);
    }
}
