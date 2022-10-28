using UnityEngine.EventSystems;
using UnityEngine;

public class VanishingMenu : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler {
    private bool _mouseIsOver;

    public void OnDeselect(BaseEventData eventData){
        if (!_mouseIsOver)
            gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData){
        _mouseIsOver = true;
    }

    public void OnPointerExit(PointerEventData eventData){
        _mouseIsOver = false;
    }

    public void OnEnable(){
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    protected void Vanish(){
        gameObject.SetActive(false);
    }
}