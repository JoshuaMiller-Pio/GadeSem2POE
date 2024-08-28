using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenV2 : MonoBehaviour
{
    public int gridSizeX = 25;
    public int gridSizeY = 25;  // This is still for the grid size in the "Z" direction
    public float squareSize = 5f;

    private Material greenMaterial;
    private Material brownMaterial;

    private void Start()
    {
        // Create materials
        greenMaterial = new Material(Shader.Find("Standard"));
        greenMaterial.color = Color.green;

        brownMaterial = new Material(Shader.Find("Standard"));
        brownMaterial.color = new Color(0.6f, 0.3f, 0f);  // Brown color

        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Create a parent GameObject to hold the grid squares
        GameObject gridParent = new GameObject("Grid");

        // Calculate the center start positions for the 4x4 brown square
        int centerStartX = (gridSizeX / 2) - 2;
        int centerStartZ = (gridSizeY / 2) - 2;

        // Loop through grid dimensions and create squares
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeY; z++)  // This now loops through the Z axis
            {
                bool isInCenter = (x >= centerStartX && x < centerStartX + 4) && (z >= centerStartZ && z < centerStartZ + 4);
                CreateSquare(x, z, gridParent.transform, isInCenter);
            }
        }

        // Set the grid parent as a child of this GameObject
        gridParent.transform.parent = this.transform;
    }

    void CreateSquare(int x, int z, Transform parent, bool isInCenter)
    {
        // Create a new GameObject for the square
        GameObject square = new GameObject($"Square_{x}_{z}");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = square.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = square.AddComponent<MeshRenderer>();
        BoxCollider boxCollider = square.AddComponent<BoxCollider>();
        // Generate the mesh for the square
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[6];
        Vector2[] uv = new Vector2[4];

        // Define the four vertices of the square (in XZ plane)
        vertices[0] = new Vector3(x * squareSize, 0, z * squareSize);
        vertices[1] = new Vector3((x + 1) * squareSize, 0, z * squareSize);
        vertices[2] = new Vector3(x * squareSize, 0, (z + 1) * squareSize);
        vertices[3] = new Vector3((x + 1) * squareSize, 0, (z + 1) * squareSize);

        // Define the two triangles
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;

        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;

        // Define UVs
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);

        // Assign the data to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // Recalculate normals for lighting
        mesh.RecalculateNormals();

        // Assign the mesh to the MeshFilter
        meshFilter.mesh = mesh;

        // Set the material based on whether the square is in the center
        meshRenderer.material = isInCenter ? brownMaterial : greenMaterial;

        // Parent the square under the grid parent
        square.transform.parent = parent;
    }
}
