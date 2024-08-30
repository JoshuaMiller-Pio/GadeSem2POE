using UnityEngine;
using System.Collections.Generic;

public class MeshGenV2 : MonoBehaviour
{
    public int gridSizeX = 25;
    public int gridSizeY = 25;
    public float squareSize = 5f;

    private Material greenMaterial;
    private Material brownMaterial;
    private GameObject[,] gridSquares;

    private void Start()
    {
        // Create materials
        greenMaterial = new Material(Shader.Find("Standard"));
        greenMaterial.color = Color.green;

        brownMaterial = new Material(Shader.Find("Standard"));
        brownMaterial.color = new Color(0.6f, 0.3f, 0f);  

        gridSquares = new GameObject[gridSizeX, gridSizeY];

        GenerateGrid();    
        GeneratePaths(3);  
    }

    void GenerateGrid()
    {
        GameObject gridParent = new GameObject("Grid");

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeY; z++)
            {
                gridSquares[x, z] = CreateSquare(x, z, gridParent.transform, false);
            }
        }

        gridParent.transform.parent = this.transform;
    }

    GameObject CreateSquare(int x, int z, Transform parent, bool isPath)
    {
        GameObject square = new GameObject($"Square_{x}_{z}");

        MeshFilter meshFilter = square.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = square.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[6];
        Vector2[] uv = new Vector2[4];

        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(squareSize, 0, 0);
        vertices[2] = new Vector3(0, 0, squareSize);
        vertices[3] = new Vector3(squareSize, 0, squareSize);

        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;

        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;

        meshRenderer.material = isPath ? brownMaterial : greenMaterial;

        BoxCollider boxCollider = square.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(squareSize, 0.1f, squareSize);
        boxCollider.center = new Vector3(squareSize / 2, 0, squareSize / 2);

        square.transform.position = new Vector3(x * squareSize, 0, z * squareSize);

        square.transform.parent = parent;

        return square;
    }

    void GeneratePaths(int numberOfPaths)
    {
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        for (int i = 0; i < numberOfPaths; i++)
        {
            Vector2Int currentPosition = new Vector2Int(gridSizeX / 2, gridSizeY / 2);

            while (true)
            {
                visited.Add(currentPosition);
                SetSquareColor(currentPosition, brownMaterial);

                int direction = Random.Range(0, 4);
                Vector2Int offset = Vector2Int.zero;

                switch (direction)
                {
                    case 0: offset = Vector2Int.up; break;   
                    case 1: offset = Vector2Int.right; break; 
                    case 2: offset = Vector2Int.down; break;  
                    case 3: offset = Vector2Int.left; break;  
                }
                Vector2Int nextPosition1 = currentPosition + offset;
                Vector2Int nextPosition2 = currentPosition + 2 * offset;

                nextPosition1.x = Mathf.Clamp(nextPosition1.x, 0, gridSizeX - 1);
                nextPosition1.y = Mathf.Clamp(nextPosition1.y, 0, gridSizeY - 1);
                nextPosition2.x = Mathf.Clamp(nextPosition2.x, 0, gridSizeX - 1);
                nextPosition2.y = Mathf.Clamp(nextPosition2.y, 0, gridSizeY - 1);

                if (visited.Contains(nextPosition1) || visited.Contains(nextPosition2))
                {
                    continue;
                }

                SetSquareColor(nextPosition1, brownMaterial);
                visited.Add(nextPosition1);

                SetSquareColor(nextPosition2, brownMaterial);
                visited.Add(nextPosition2);

                currentPosition = nextPosition2;

                if (currentPosition.x == 0 || currentPosition.x == gridSizeX - 1 ||
                    currentPosition.y == 0 || currentPosition.y == gridSizeY - 1)
                {
                    break;
                }
            }
        }
    }

    void SetSquareColor(Vector2Int position, Material material)
    {
        MeshRenderer renderer = gridSquares[position.x, position.y].GetComponent<MeshRenderer>();
        renderer.material = material;
    }
}
