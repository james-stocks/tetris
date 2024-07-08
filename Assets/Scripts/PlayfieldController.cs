using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public struct Tile {
    public byte red;
    public byte green;
    public byte blue;
    public bool solid;
    public Mesh mesh;
}

public class PlayfieldController : MonoBehaviour
{
    // Texture2D holding tile image
    public Material tileMaterial;
    public Texture2D tileTexture;

    private Tile[,] tiles;

    // Start is called before the first frame update
    void Start()
    {

        tileMaterial.SetTexture("_MainTex", tileTexture);
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
                tiles[y,x].mesh = TileQuadMesh();
            }
        }
    }

    private Mesh TileQuadMesh() {
        float width = 0.32f;
        float height = 0.32f; 
        MeshFilter mf = GetComponent<MeshFilter>();
        var mesh = new Mesh();
        mf.mesh = mesh;
        
        Vector3[] vertices = new Vector3[4];
        
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(width, 0, 0);
        vertices[2] = new Vector3(0, height, 0);
        vertices[3] = new Vector3(width, height, 0);
        
        mesh.vertices = vertices;
        
        int[] tri = new int[6];

        tri[0] = 0;
        tri[1] = 2;
        tri[2] = 1;
        
        tri[3] = 2;
        tri[4] = 3;
        tri[5] = 1;
        
        mesh.triangles = tri;
        
        Vector3[] normals = new Vector3[4];
        
        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;
        
        mesh.normals = normals;
        
        Vector2[] uv = new Vector2[4];

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);
        
        mesh.uv = uv;

        return mesh;
    }

    // Update is called once per frame
    void Update()
    {
        RenderParams rp = new RenderParams(tileMaterial);
        // Render the tiles
        for (int y = 0; y < tiles.GetLength(0); y++) {
            for (int x = 0; x < tiles.GetLength(1); x++)
            {
                if(tiles[y,x].solid) {
                    Graphics.RenderMesh(rp, tiles[y,x].mesh, 0,  Matrix4x4.Translate(new Vector3(-2.5f + 0.32f * x, -3.5f + 0.32f * y, 0.1f)));
                }
            }
        }
    }
}
