using System.Collections;
using System.Collections.Generic;
using OpenCvSharp;
using UnityEngine;

public class TemplateMat {
    

    public Mat Get(Texture2D texture){
        var mat = OpenCvSharp.Unity.TextureToMat(texture);
        Cv2.CvtColor(mat, mat, ColorConversionCodes.BGR2GRAY);
        return mat;
    }
}
