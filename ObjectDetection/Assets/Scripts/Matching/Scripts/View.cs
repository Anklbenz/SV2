using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Animator))]
public class View : MonoBehaviour {
   private CanvasGroup _canvasGroup;
   private Animator _animator;
   public bool isActive => gameObject.activeInHierarchy;
   private float _canvasAlpha;

   private void Awake(){
      _canvasGroup = GetComponent<CanvasGroup>();
      _animator = GetComponent<Animator>();
   }

   public void Open() =>
      gameObject.SetActive(true);

   public void Close() =>
      gameObject.SetActive(false);

}
