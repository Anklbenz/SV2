using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewfinderView : View {
    [Header("Viewfinder")]
    [SerializeField] private Button button;

    public event Action TakePhotoEvent;

    private void OnEnable() =>
        button.onClick.AddListener(delegate { TakePhotoEvent?.Invoke(); });

    private void OnDisable() =>
        button.onClick.RemoveAllListeners();
}