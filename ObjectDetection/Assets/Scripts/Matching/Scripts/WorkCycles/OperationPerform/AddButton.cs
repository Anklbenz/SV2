using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action = OpenCvSharp.Util.Action;

public class AddButton : InteractiveButton {
    [SerializeField] private Color active, standard, neutral;

    public event Action ClickEvent;

    private void OnEnable() =>
        button.onClick.AddListener(delegate { ClickEvent?.Invoke(); });

    private void OnDisable() =>
        button.onClick.RemoveAllListeners();

    public void SetWarning(){
        backgroundColor = active;
        textColor = standard;
        spriteColor = standard;
    }

    public void SetStandard(){
        backgroundColor = standard;
        textColor = neutral;
        spriteColor = neutral;
    }
}
