using UnityEngine;

namespace Assets
{
    public class PerlinNoise : MonoBehaviour
    {
        public int width = 256;
        public int height = 256;

        public float scale = 20;

        public float offsetX = 100f;
        public float offsetY = 100f;

        void Start()
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = GenerateTexture();
        }

        private Texture GenerateTexture()
        {
            Texture2D texture = new Texture2D(width,height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color color = CalculateColor(x, y);
                    texture.SetPixel(x,y,color);
                }
            }
            texture.Apply();
            return texture;
        }

        Color CalculateColor(int x, int y)
        {
            float xCoord = (float) x / width * scale;
            float yCoord = (float) y / height * scale;
            
            float sample = Mathf.PerlinNoise(x, y);
            return new Color(sample, sample, sample);
        }
    }
}