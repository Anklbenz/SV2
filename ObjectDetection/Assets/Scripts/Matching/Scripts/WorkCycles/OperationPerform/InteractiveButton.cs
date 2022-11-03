using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveButton : MonoBehaviour {
    [SerializeField] private Image backgroundImage, image;
    [SerializeField] private TMP_Text text;

    public Button button;

    public void SetActive(bool state) =>
        gameObject.SetActive(state);

    public Color backgroundColor{
        set => backgroundImage.color = value;
    }

    public Color spriteColor{
        set => image.color = value;
    }

    public Color textColor{
        set => text.color = value;
    }

    public Sprite spriteImage{
        set => image.sprite = value;
    }
}