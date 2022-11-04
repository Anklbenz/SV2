using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ViewfinderView : View {
    [Header("Viewfinder")]
    [SerializeField] private Button button;

    public event Action TakePhotoEvent;

    public override void Open(){
        button.onClick.AddListener(delegate { TakePhotoEvent?.Invoke(); });
        base.Open();
    }

    public override void Close(){
        button.onClick.RemoveAllListeners();
        base.Close();
    }
}