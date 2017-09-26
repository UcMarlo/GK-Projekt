using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class RadialGradient
{
    public int _texWidth = 512;
    public int _texHeight = 512;
    
    float _maskThreshold = 4.0f;

    public Texture2D mask;

    public RadialGradient()
    {
        GenerateTexture();
    }
    
    public void GenerateTexture()
    {
        Vector2 maskCenter = new Vector2(_texWidth * 0.5f, _texHeight * 0.5f);
        mask = new Texture2D(_texWidth, _texHeight, TextureFormat.RGBA32, true);

        for (int y = 0; y < _texHeight; y++)
        {
            for (int x = 0; x < _texWidth; x++)
            {
                float distFromCenter = Vector2.Distance(maskCenter, new Vector2(x, y));
                float maskPixel = (1.0f + (distFromCenter / _texWidth)) * _maskThreshold;
                mask.SetPixel(x,y, new Color(maskPixel,maskPixel,maskPixel));
            }
        }
        mask.Apply();
    }
}
