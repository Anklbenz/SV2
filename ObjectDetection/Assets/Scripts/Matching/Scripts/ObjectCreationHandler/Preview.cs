using OpenCvSharp;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Preview {
    [SerializeField] private RawImage previewingImage;

    private Texture2D _previewTexture;
    private Mat _bitwiseOrMat = new();

    public void Initialize(Vector2Int previewTextureSize, float uiScale){
        _previewTexture = new Texture2D(previewTextureSize.x, previewTextureSize.y, TextureFormat.BGRA32, false);
        previewingImage.rectTransform.localScale = new Vector3(uiScale, uiScale, 0);
        previewingImage.rectTransform.sizeDelta = new Vector2(previewTextureSize.x, previewTextureSize.y);
        previewingImage.enabled = (true);
    }

    public void Play(Mat camera){
        previewingImage.texture = OpenCvSharp.Unity.MatToTexture(camera, _previewTexture);
    }

    public void PlayWithTemplate(Mat camera, Mat template){
        Cv2.BitwiseOr(camera, template, _bitwiseOrMat);
        previewingImage.texture = OpenCvSharp.Unity.MatToTexture(_bitwiseOrMat, _previewTexture);
    }
}