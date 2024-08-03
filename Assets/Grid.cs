
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;
using Random = UnityEngine.Random;

public class Grid : MonoBehaviour
{
    public Material ground, edgeMaterial;
        
    public int size = 100;
    public float scale = 0.1f, waterlevel = 0.4f;

    private void Start()
    {
         Cells[,] grid;

        float[,] noiseMap = new float[size, size];
        float offsetx = Random.Range(-10000, 10000);
        float offsety = Random.Range(-10000, 10000);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noisevalue = Mathf.PerlinNoise(x * scale + offsetx, y * scale + offsety);
                noiseMap[x, y] = noisevalue;

            }
        }

        float[,] falloffmap = new float[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float xv = x / (float)size * 2 - 1;
                float yv = y / (float)size * 2 - 1;
                float v = Mathf.Max(Mathf.Abs(xv), Mathf.Abs((yv)));
                falloffmap[x, y] = Mathf.Pow(v, 3f) / (Mathf.Pow(v, 3f) + Mathf.Pow(2.2f - 2.2f * v, 3f));
            }
        }

        grid = new Cells[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noisevalue = noiseMap[x, y];
                noisevalue -= falloffmap[x, y];
                Cells cell = new Cells();
                cell.Iswater = noisevalue < waterlevel;
                grid[x, y] = cell;

            }
        }

        createTerrainMesh(grid);
        DrawEdgeMesh(grid);
        AddTexture(grid);
    }
void DrawEdgeMesh(Cells[,] grid) {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        for(int y = 0; y < size; y++) {
            for(int x = 0; x < size; x++) {
                Cells cell = grid[x, y];
                if(!cell.Iswater) {
                    if(x > 0) {
                        Cells left = grid[x - 1, y];
                        if(left.Iswater) {
                            Vector3 a = new Vector3(x - .5f, 0, y + .5f);
                            Vector3 b = new Vector3(x - .5f, 0, y - .5f);
                            Vector3 c = new Vector3(x - .5f, -1, y + .5f);
                            Vector3 d = new Vector3(x - .5f, -1, y - .5f);
                            Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                            for(int k = 0; k < 6; k++) {
                                vertices.Add(v[k]);
                                triangles.Add(triangles.Count);
                            }
                        }
                    }
                    if(x < size - 1) {
                        Cells right = grid[x + 1, y];
                        if(right.Iswater) {
                            Vector3 a = new Vector3(x + .5f, 0, y - .5f);
                            Vector3 b = new Vector3(x + .5f, 0, y + .5f);
                            Vector3 c = new Vector3(x + .5f, -1, y - .5f);
                            Vector3 d = new Vector3(x + .5f, -1, y + .5f);
                            Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                            for(int k = 0; k < 6; k++) {
                                vertices.Add(v[k]);
                                triangles.Add(triangles.Count);
                            }
                        }
                    }
                    if(y > 0) {
                        Cells down = grid[x, y - 1];
                        if(down.Iswater) {
                            Vector3 a = new Vector3(x - .5f, 0, y - .5f);
                            Vector3 b = new Vector3(x + .5f, 0, y - .5f);
                            Vector3 c = new Vector3(x - .5f, -1, y - .5f);
                            Vector3 d = new Vector3(x + .5f, -1, y - .5f);
                            Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                            for(int k = 0; k < 6; k++) {
                                vertices.Add(v[k]);
                                triangles.Add(triangles.Count);
                            }
                        }
                    }
                    if(y < size - 1) {
                        Cells up = grid[x, y + 1];
                        if(up.Iswater) {
                            Vector3 a = new Vector3(x + .5f, 0, y + .5f);
                            Vector3 b = new Vector3(x - .5f, 0, y + .5f);
                            Vector3 c = new Vector3(x + .5f, -1, y + .5f);
                            Vector3 d = new Vector3(x - .5f, -1, y + .5f);
                            Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                            for(int k = 0; k < 6; k++) {
                                vertices.Add(v[k]);
                                triangles.Add(triangles.Count);
                            }
                        }
                    }
                }
            }
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        GameObject edgeObj = new GameObject("Edge");
        edgeObj.transform.SetParent(transform);

        MeshFilter meshFilter = edgeObj.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = edgeObj.AddComponent<MeshRenderer>();
        meshRenderer.material = edgeMaterial;
    }

    void createTerrainMesh(Cells[,] grid)
    { 
        Mesh mesh = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
               
                Cells cell = grid[x, y];
                
                    Vector3 a = new Vector3(x - .5f, 0, y + 0.5f);
                    Vector3 b = new Vector3(x + .5f, 0, y + 0.5f);
                    Vector3 c = new Vector3(x - .5f, 0, y - .5f);
                    Vector3 d = new Vector3(x + .5f, 0, y - 0.5f);
                    Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                    Vector2 uvA = new Vector2(x / (float)size, y / (float)size);
                    Vector2 uvB = new Vector2((x + 1)/ (float)size, y / (float)size);
                    Vector2 uvC = new Vector2(x / (float)size, (y + 1) / (float)size);
                    Vector2 uvD = new Vector2((x + 1)/ (float)size, (y + 1) / (float)size);
                    Vector2[] uv = new Vector2[]{ uvA, uvB, uvC, uvB, uvD, uvC };
                    for (int k = 0; k < 6; k++)
                    {
                        verts.Add(v[k]);
                        tris.Add(tris.Count);
                        uvs.Add(uv[k]);
                    }
                
            }
        }

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    void AddTexture(Cells[,] grid)
    {
        Texture2D texture = new Texture2D(size, size);
        Color[] colourmap = new Color[size * size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cells cell = grid[x, y];
                if (cell.Iswater)
                {
                    colourmap[y * size + x] = new Color(0.55f, 0.27f, 0.07f, 1f);

                }
                else
                {
                    colourmap[y * size + x] = Color.green;

                }
            }

            texture.filterMode = FilterMode.Point;
            texture.SetPixels(colourmap);
            texture.Apply();

            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = ground;
            meshRenderer.material.mainTexture = texture;
        }
    }
}
/*
private void OnDrawGizmos()
  {
      if (!Application.isPlaying) return;
      for (int y = 0; y < size; y++)
      {
          for (int x = 0; x < size; x++)
          {
              Cells cell = grid[x, y];
              if (cell.Iswater)
              {
                  Gizmos.color =Color.blue  ;
              }
              else
              {
                  Gizmos.color = Color.green;
              }
              Vector3 pos = new Vector3(x, 0, y);
              Gizmos.DrawCube(pos,Vector3.one);
          }
      }
  }
}*/
