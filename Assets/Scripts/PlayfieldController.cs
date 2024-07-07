using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public struct Tile {
    public byte red;
    public byte green;
    public byte blue;
    public bool solid;
}

public class PlayfieldController : MonoBehaviour
{

    // tile image
    public TextAsset tileImageBytes;

    // Texture2D holding tile image
    private Texture2D tileTexture;

    public RenderTexture renderTexture;

    private Tile[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tileTexture = new Texture2D(2,2);
        ImageConversion.LoadImage(tileTexture, tileImageBytes.bytes);

        tiles = new Tile[24,10];
        InitTiles();
    }

    private void InitTiles() {
        for (int y = 0; y < tiles.GetLength(0); y++) {
            for (int x = 0; x < tiles.GetLength(1); x++)
            {
                tiles[y,x].red = 255;
                tiles[y,x].green = 80;
                tiles[y,x].blue = 80;
                tiles[y,x].solid = ((x + y) % 2 == 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Render the tiles
        for (int y = 0; y < tiles.GetLength(0); y++) {
            for (int x = 0; x < tiles.GetLength(1); x++)
            {
                if(tiles[y,x].solid) {
                    Graphics.Blit(tileTexture, renderTexture, new Vector2(16.0f, 32.0f), new Vector2(x * 16, y * 16));
                }
            }
        }
    }
}
