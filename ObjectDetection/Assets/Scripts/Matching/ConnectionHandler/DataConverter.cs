using System;
using ClassesForJson;
using UnityEngine;

public class DataConverter {
    public Data ServerDataToData(ServerData serverData){
        return new Data
        {
            Texture = Base64ToTexture2D(serverData.frame),
            Position = serverData.origin,
            Rotation = serverData.rotation,
        };
    }
    
    private Texture2D Base64ToTexture2D(string base64String){
        var texture = new Texture2D(2, 2);
        
        var bytes = Convert.FromBase64String(base64String);
        texture.LoadImage(bytes);
 
        return texture;
    }
}
