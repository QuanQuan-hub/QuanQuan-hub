using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPainter : MonoBehaviour
{
    Texture2D paintTexture = null;
    public Color paintColor = Color.red;
    public int Radis;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
        Material colorMaterial = mesh.materials[1];

        colorMaterial.mainTexture = new Texture2D(1024, 1024, TextureFormat.ARGB32, false);

        paintTexture = colorMaterial.mainTexture as Texture2D;
        Color color = new Color(0, 0, 0, 0);
        for (int i = 0; i < this.paintTexture.height; i++)
        {
            for (int j = 0; j < this.paintTexture.width; j++)
            {
                paintTexture.SetPixel(i, j, color);
            }
        }
        this.paintTexture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                Vector2 uv = raycastHit.textureCoord;
                int x = ((int)(uv.x * this.paintTexture.width)) % this.paintTexture.width;
                int y = ((int)(uv.y * this.paintTexture.height)) % this.paintTexture.height;
                Debug.Log($"{x},{y}点发生碰撞");

                DrawCircle(x, y);
            }
        }
    }
    private void DrawCircle(int x,int y)
    {
        for (int i = -this.Radis; i < this.Radis; i++)
        {
            for (int j = -this.Radis; j < this.Radis; j++)
            {
                if (i * i + j * j > this.Radis * this.Radis) 
                {
                    continue;
                }
                paintTexture.SetPixel(x + i, y + j, paintColor);
            }
        }
        this.paintTexture.Apply();
    }
}
