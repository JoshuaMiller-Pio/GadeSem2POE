using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeshGenTest : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    public int paths = 3;
    
    
    public int xSize = 25;
    public int zSize = 25;
    
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        paths = Random.Range(0, 4);
        GetComponent<MeshFilter>().mesh = mesh;
        
        createShape();
        updateMesh();
    }

    void updateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    void createShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for ( int i=0,  z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }


        triangles = new int[xSize * zSize * 6];
            int vert = 0, tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris +0] = vert + 0;
                triangles[tris +1] = vert +xSize + 1;
                triangles[tris +2] = vert +1;
                triangles[tris +3] = vert +1;
                triangles[tris +4] = vert +xSize + 1;
                triangles[tris +5] = vert +xSize + 2;
                vert++;
                tris += 6; 
            }
                vert++;
        }
      
       

    }

     private void OnDrawGizmos()
  {
      if (vertices == null)
      {
          return;
      }
      for (int i = 0; i < vertices.Length; i++)
      {
          Gizmos.DrawSphere(vertices[i],.1f);
      }
  }
 /*void createShape()
  {
      vertices = new Vector3[]
      {
          new Vector3(0,0,0),
          new Vector3(0,0,1),
          new Vector3(1,0,0)
      };

      triangles = new int[]
      {
          0,1,2
      };
  }*/
}
