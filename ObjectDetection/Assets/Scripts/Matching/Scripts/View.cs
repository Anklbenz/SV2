using UnityEngine;

[System.Serializable]
public class View {
   [Header("View")]
   [SerializeField] private GameObject canvas;
   [SerializeField] private CanvasGroup canvasGroup;
   [SerializeField] private Animator animator;
   public virtual void Open() =>
      canvas.SetActive(true);

   public virtual void Close() =>
      canvas.SetActive(false);
}
