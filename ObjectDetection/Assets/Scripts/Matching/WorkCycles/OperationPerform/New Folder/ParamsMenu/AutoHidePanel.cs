
using UnityEngine;
using UnityEngine.UIElements;

public class AutoHidePanel : MonoBehaviour {
    private bool _isCollapse;

    private void Update(){
        if (!_isCollapse || !Input.GetMouseButtonDown(0)) return;
        var position = (Vector2)Input.mousePosition;

        if (!RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, position))
            Hide();
    }

    public void Show(){
        gameObject.SetActive(true);
        _isCollapse = true;
    }

    public void Hide(){
        gameObject.SetActive(false);
        _isCollapse = false;
    }
}