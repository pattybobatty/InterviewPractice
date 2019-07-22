using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour {

    Texture2D texture;
    public int radius = 64;
	// Use this for initialization
	void Start ()
    {
        texture = new Texture2D(2048, 2048, TextureFormat.ARGB32, false);

        for ( int i = 0; i < texture.width; i++ )
        {
            for ( int j = 0; j < texture.height; j++ )
            {
                texture.SetPixel(i, j, Color.green);
            }
        }
        // Apply all SetPixel calls
        texture.Apply();

        GetComponent<Renderer>().material.mainTexture = texture;

        DrawFlerp();
    }
	
    void Draw()
    {
        int y = radius;
        int x = 0;
        int e = 0;

        int xCenter = texture.width / 2;
        int yCenter = texture.height / 2;

        while (y >= x)
        {
            if (e > 0)
            {
                e -= (y + y - 1);
                y--;
            }

            e += (x + x + 1);
            x++;

            texture.SetPixel(xCenter + x, yCenter + y,  Color.black);
            texture.SetPixel(xCenter + x, yCenter - y, Color.black);
            texture.SetPixel(xCenter - x, yCenter + y, Color.black);
            texture.SetPixel(xCenter - x, yCenter - y, Color.black);
        }
        texture.Apply();
    }

	void DrawFlerp()
    {
        int centerX = texture.width / 2;
        int centerY = texture.height / 2;

        int x = 0;
        int y = 45;

        while(x <= (radius + 1))
        {
            y = (int)Mathf.Sqrt((radius * radius) - (x * x));
            texture.SetPixel(centerX + x, centerY + y, Color.black);
            texture.SetPixel(centerX + x, centerY - y, Color.black);
            texture.SetPixel(centerX - x, centerY + y, Color.black);
            texture.SetPixel(centerX - x, centerY - y, Color.black);
            x++;


        }

        texture.Apply();
    }
}
