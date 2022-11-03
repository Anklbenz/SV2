using OpenCvSharp;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Template {
    [SerializeField] private RawImage frameImage;
    public Mat mat{ get; private set; }
    public bool previewEnabled;

    public FrameView view = new();
    private TemplateMat _templateMat = new();

    public void Initialize(Texture2D templateTexture, float uiScale = 1){
        view.SetView(frameImage, templateTexture, uiScale);
        mat = _templateMat.Get(templateTexture);
    }


}
