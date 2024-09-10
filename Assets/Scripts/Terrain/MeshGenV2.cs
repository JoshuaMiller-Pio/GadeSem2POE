using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MeshGenV2 : MonoBehaviour
{
    public int gridSizeX = 25;
    public int gridSizeY = 25;
    public float squareSize = 5f;
    public GameObject Base;
    public float varianceMultiplier = 1.0f; // Controls frequency of turns
    public int x = 0, y = 0;
    private Material greenMaterial;
    private Material inLandgreenMaterial;
    private Material brownMaterial;
    private GameObject[,] gridSquares;
    public TileScriptable pathTile, sumTile, inlandTile;
    [SerializeField]
    public List<PathData> pathPositions = new List<PathData>();

    public EnemySpawnManager _enemySpawnManager;
    private NavMeshSurface navMeshSurface;

    private void Start()
    {
        _enemySpawnManager = GameObject.FindGameObjectWithTag("EnemySpawnManager").GetComponent<EnemySpawnManager>();
        // Initialize materials
        InitializeMaterials();

        gridSquares = new GameObject[gridSizeX, gridSizeY];
        GenerateGrid();
        PlaceCenterObject();
        GenerateDistinctPaths(3);
        addSpawnableArea();
        navMeshSurface.BuildNavMesh();
        _enemySpawnManager.FindSpawnTiles();
    }

    //divides the gridsize to find the center and places the original base(defense target)
    void PlaceCenterObject()
    {
        if (Base != null)
        {
            int centerX = gridSizeX / 2;
            int centerY = gridSizeY / 2;
            Vector3 centerPosition = new Vector3(centerX * squareSize, 0, centerY * squareSize);
            centerPosition += new Vector3(squareSize / 2, 0, squareSize / 2);
            Instantiate(Base, new Vector3(centerPosition.x, centerPosition.y + 3.66f, centerPosition.z), Quaternion.identity);
        }
    }

    void InitializeMaterials()
    {
        inLandgreenMaterial = new Material(Shader.Find("Standard"));
        inLandgreenMaterial.color = new Color(0.0f, .6f, 0f);

        greenMaterial = new Material(Shader.Find("Standard"));
        greenMaterial.color = new Color(0.0f, 1f, 0f);

        brownMaterial = new Material(Shader.Find("Standard"));
        brownMaterial.color = new Color(0.6f, 0.3f, 0f);
    }

    void GenerateGrid()
    {
        GameObject gridParent = new GameObject("Grid");
        navMeshSurface = gridParent.AddComponent<NavMeshSurface>();

        int midX = gridSizeX / 2;
        int midY = gridSizeY / 2;
        float startX = midX - 2.5f;
        float startY = midY - 2.5f;
        float endX = midX + 2.5f;
        float endY = midY + 2.5f;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                bool isPath = (x >= startX && x < endX && y >= startY && y < endY);
                gridSquares[x, y] = meshCreation(x, y, gridParent.transform, isPath);

                if (isPath)
                {
                    gridSquares[x, y].GetComponent<Renderer>().material = brownMaterial;
                    gridSquares[x, y].GetComponent<Tile>().tileScriptable = pathTile;
                }
            }
        }

        gridParent.transform.parent = this.transform;
    }
    
//this shit pissing me off 
//creates on start up 
//REMEMBER CLOCKWISE ANTICLOCKWISE FFS
    GameObject meshCreation(int x, int y, Transform parent, bool isPath)
    {
        GameObject square = new GameObject("Square_" + x + "_" + y);
    
        // Add MeshFilter and MeshRenderer
        MeshFilter meshFilter = square.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = square.AddComponent<MeshRenderer>();

        // Generate vertices and triangles for the square
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[6];
        Vector2[] uv = new Vector2[4];

        float halfSize = squareSize / 2;
        vertices[0] = new Vector3(-halfSize, 0, -halfSize); // Bottom left
        vertices[1] = new Vector3(-halfSize, 0, halfSize);  // Top left
        vertices[2] = new Vector3(halfSize, 0, -halfSize);  // Bottom right
        vertices[3] = new Vector3(halfSize, 0, halfSize);   // Top right

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 0);
        uv[3] = new Vector2(1, 1);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        //makes sure normals are correct way up
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

        // Add MeshCollider to enable mouse interactions
        MeshCollider meshCollider = square.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh; // Assign the generated mesh to the collider

        // Assign the correct material and tileScriptable
        Tile tile = square.AddComponent<Tile>();

        if (isPath)
        {
            meshRenderer.material = brownMaterial;
            tile.tileScriptable = pathTile;
        }
        else
        {
            meshRenderer.material = inLandgreenMaterial;
            tile.tileScriptable = inlandTile;
        }

        square.transform.position = new Vector3(x * squareSize, 0, y * squareSize);
        square.transform.parent = parent;

        return square;
    }

    void GenerateDistinctPaths(int numberOfPaths)
    {
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        Vector2Int centerPosition = new Vector2Int(gridSizeX / 2, gridSizeY / 2);

        visited.Add(centerPosition);
        SetColor(centerPosition, brownMaterial);

        List<Vector2Int> availableDirections = new List<Vector2Int>
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };
        ShuffleList(availableDirections);

        for (int i = 0; i < numberOfPaths; i++)
        {
            if (i >= availableDirections.Count)
            {
                Debug.LogWarning("Not enough available directions for all paths.");
                break;
            }

            Vector2Int primaryDirection = availableDirections[i];
            PathData pathData = new PathData();
            pathData.positions = CreatePath(centerPosition, primaryDirection, visited);
            pathPositions.Add(pathData);
        }
        ReverseLists();
    }

    List<Vector3> CreatePath(Vector2Int startPosition, Vector2Int primaryDirection, HashSet<Vector2Int> visited)
    {
        List<Vector3> path = new List<Vector3>();
        Vector2Int currentPosition = startPosition;
        Vector2Int currentDirection = primaryDirection;
        int stepsSinceLastTurn = 0;
        int minStepsBetweenTurns = 2;

        while (true)
        {
            Vector2Int nextPosition = currentPosition + currentDirection;

            if (!Validposition(nextPosition, visited))
            {
                break;
            }

            currentPosition = nextPosition;
            visited.Add(currentPosition);

            GameObject pathObject = meshCreation(currentPosition.x, currentPosition.y, this.transform, true);
            path.Add(pathObject.transform.position);
            SetColor(currentPosition, brownMaterial);

            stepsSinceLastTurn++;
            
            //self explanitory
            if (IsAtEdge(currentPosition))
            {
                break;
            }
            //adds the curve mutliplied by the multiplier which can be changed in the inspector
            if (stepsSinceLastTurn >= minStepsBetweenTurns && Random.value < 0.4f * varianceMultiplier)
            {
                Vector2Int turnDirection = GetPerpendicularDirection(currentDirection);
                Vector2Int turnPosition = currentPosition + turnDirection;
                //if valid progress with creating the path
                if (Validposition(turnPosition, visited))
                {
                    currentPosition = turnPosition;
                    visited.Add(currentPosition);

                    GameObject turnObject = meshCreation(currentPosition.x, currentPosition.y, this.transform, true);
                    path.Add(turnObject.transform.position);
                    SetColor(currentPosition, brownMaterial);
                    stepsSinceLastTurn = 0;
                }
            }
        }

        return path;
    }
    
    //gets the direction that the path needs to streach out towards. else it curves and doesnt reach the end
    Vector2Int GetPerpendicularDirection(Vector2Int direction)
    {
        List<Vector2Int> perpendicularDirections = new List<Vector2Int>();

        if (direction == Vector2Int.up || direction == Vector2Int.down)
        {
            perpendicularDirections.Add(Vector2Int.left);
            perpendicularDirections.Add(Vector2Int.right);
        }
        else
        {
            perpendicularDirections.Add(Vector2Int.up);
            perpendicularDirections.Add(Vector2Int.down);
        }

        return perpendicularDirections[Random.Range(0, perpendicularDirections.Count)];
    }
    
    bool Validposition(Vector2Int position, HashSet<Vector2Int> visited)
    {
        return position.x >= 0 && position.x < gridSizeX && position.y >= 0 && position.y < gridSizeY && !visited.Contains(position);
    }

    bool IsAtEdge(Vector2Int position)
    {
        return position.x == 0 || position.x == gridSizeX - 1 || position.y == 0 || position.y == gridSizeY - 1;
    }

    //sets path tile colour and scriptable
    void SetColor(Vector2Int position, Material material)
    {
        Renderer renderer = gridSquares[position.x, position.y].GetComponent<Renderer>();
        renderer.material = material;
        gridSquares[position.x, position.y].GetComponent<Tile>().tileScriptable = pathTile;
    }

    //finds path tiles and assigns light green tiles next to it and assigns it the defenders tile and changes from inland tile to sumTile
    void addSpawnableArea()
    {
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                if (gridSquares[i, j].GetComponent<Tile>().tileScriptable != pathTile)
                {
                    if (i > 0 && gridSquares[i - 1, j].GetComponent<Tile>().tileScriptable == pathTile)
                    {
                        gridSquares[i, j].GetComponent<Tile>().tileScriptable = sumTile;
                        gridSquares[i, j].AddComponent<DefenderTiles>();
                        gridSquares[i, j].GetComponent<Renderer>().material = greenMaterial;
                    }

                    if (i < gridSizeX - 1 && gridSquares[i + 1, j].GetComponent<Tile>().tileScriptable == pathTile)
                    {
                        gridSquares[i, j].GetComponent<Tile>().tileScriptable = sumTile;
                        gridSquares[i, j].AddComponent<DefenderTiles>();
                        gridSquares[i, j].GetComponent<Renderer>().material = greenMaterial;
                    }

                    if (j > 0 && gridSquares[i, j - 1].GetComponent<Tile>().tileScriptable == pathTile)
                    {
                        gridSquares[i, j].GetComponent<Tile>().tileScriptable = sumTile;
                        gridSquares[i, j].AddComponent<DefenderTiles>();
                        gridSquares[i, j].GetComponent<Renderer>().material = greenMaterial;
                    }

                    if (j < gridSizeY - 1 && gridSquares[i, j + 1].GetComponent<Tile>().tileScriptable == pathTile)
                    {
                        gridSquares[i, j].GetComponent<Tile>().tileScriptable = sumTile;
                        gridSquares[i, j].AddComponent<DefenderTiles>();
                        gridSquares[i, j].GetComponent<Renderer>().material = greenMaterial;
                    }
                }
            }
        }
    }

    //this creates a list of the paths and adds the curve
    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
//reverses the tile list so that the enemies can be spawned from the outside in
    void ReverseLists()
    {
        for (int i = 0; i < pathPositions.Count; i++)
        {
            pathPositions[i].positions.Reverse();
        }

        for (int i = 0; i < pathPositions.Count; i++)
        {
            for (int j = 0; j < pathPositions[i].positions.Count; j++)
            {
                Vector3 temp = new Vector3(pathPositions[i].positions[j].x, pathPositions[i].positions[j].y + 1, pathPositions[i].positions[j].z);
                if (j == 0)
                {
                    _enemySpawnManager.spawnPoints.Add(temp);
                }
                if (j == 17)
                {
                    _enemySpawnManager.spawnPoints.Add(temp);
                }
                if (j == 34)
                {
                    _enemySpawnManager.spawnPoints.Add(temp);
                }
                GameManager.Instance.pathWaypoints = pathPositions;
            }
        }
    }
}

//class for storing a 2D list essentially so that we can hold the list with coordinates and easilly reverse them

[System.Serializable]
public class PathData
{
    public List<Vector3> positions = new List<Vector3>();
}
