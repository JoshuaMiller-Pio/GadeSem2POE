using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class MeshGenV2 : MonoBehaviour
{
    public int gridSizeX = 25;
    public int gridSizeY = 25;
    public float squareSize = 5f;
    public float varianceMultiplier = 1.0f; // Controls frequency of turns
    public int x = 0, y = 0;
    private Material greenMaterial;
    private Material brownMaterial;
    private GameObject[,] gridSquares;
    public TileScriptable pathTile, sumTile;
    [SerializeField]
    private List<PathData> pathPositions = new List<PathData>();
    
    private NavMeshSurface navMeshSurface;  
     
    private void Update()
    {
        Debug.Log(pathPositions.Count);
    }

    private void Start()
    {
        // Initialize materials
        InitializeMaterials();

        gridSquares = new GameObject[gridSizeX, gridSizeY];
        GenerateGrid();

        GenerateDistinctPaths(3);

        navMeshSurface.BuildNavMesh();
    }

    void InitializeMaterials()
    {
        greenMaterial = new Material(Shader.Find("Standard"));
        greenMaterial.color = Color.green;

        brownMaterial = new Material(Shader.Find("Standard"));
        brownMaterial.color = new Color(0.6f, 0.3f, 0f);
    }

    void GenerateGrid()
    {
        GameObject gridParent = new GameObject("Grid");

        navMeshSurface = gridParent.AddComponent<NavMeshSurface>();

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                gridSquares[x, y] = CreateSquare(x, y, gridParent.transform, false);
            }
        }

        gridParent.transform.parent = this.transform;
    }

    GameObject CreateSquare(int x, int y, Transform parent, bool isPath)
    {
        GameObject square = GameObject.CreatePrimitive(PrimitiveType.Quad);
        square.transform.position = new Vector3(x * squareSize, 0, y * squareSize);
        square.transform.localScale = Vector3.one * squareSize;
        square.transform.rotation = Quaternion.Euler(90, 0, 0);
        Tile tile = square.AddComponent<Tile>();

        if (isPath)
        {
            square.GetComponent<Renderer>().material = brownMaterial;
            tile.tileScriptable = pathTile;
        }
        else
        {
            square.GetComponent<Renderer>().material = greenMaterial;
            tile.tileScriptable = sumTile;
        }

        square.transform.parent = parent;

        // Move the game object position to the center of the grid square
        Vector3 offset = new Vector3(squareSize / 2, 0, squareSize / 2);
        square.transform.position += offset;

        return square;
    }

    void GenerateDistinctPaths(int numberOfPaths)
    {
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        Vector2Int centerPosition = new Vector2Int(gridSizeX / 2, gridSizeY / 2);

        // Mark center position as visited and set its color
        visited.Add(centerPosition);
        SetSquareColor(centerPosition, brownMaterial);

        // Get random unique starting directions
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
            pathPositions.Add(pathData); // Add the path's positions to the pathPositions list
        }
        ReverseLists();
    }

    List<Vector3> CreatePath(Vector2Int startPosition, Vector2Int primaryDirection, HashSet<Vector2Int> visited)
    {
        List<Vector3> path = new List<Vector3>();
        Vector2Int currentPosition = startPosition;
        Vector2Int currentDirection = primaryDirection;
        int stepsSinceLastTurn = 0;
        int minStepsBetweenTurns = 2; // Prevents immediate consecutive turns

        while (true)
        {
            Vector2Int nextPosition = currentPosition + currentDirection;

            if (!IsPositionValid(nextPosition, visited))
            {
                break;
            }

            currentPosition = nextPosition;
            visited.Add(currentPosition);

            // Create and track the path object at this position
            GameObject pathObject = CreateSquare(currentPosition.x, currentPosition.y, this.transform, true);
            path.Add(pathObject.transform.position); // Add the object's position to the path list
            SetSquareColor(currentPosition, brownMaterial);

            stepsSinceLastTurn++;

            if (IsAtEdge(currentPosition))
            {
                break;
            }

            // Determine if a turn should occur
            if (stepsSinceLastTurn >= minStepsBetweenTurns && Random.value < 0.4f * varianceMultiplier)
            {
                Vector2Int turnDirection = GetPerpendicularDirection(currentDirection);
                Vector2Int turnPosition = currentPosition + turnDirection;

                if (IsPositionValid(turnPosition, visited))
                {
                    // Make the turn
                    currentPosition = turnPosition;
                    visited.Add(currentPosition);

                    // Create and track the path object at this position
                    GameObject turnObject = CreateSquare(currentPosition.x, currentPosition.y, this.transform, true);
                    path.Add(turnObject.transform.position); // Add the object's position to the path list
                    SetSquareColor(currentPosition, brownMaterial);

                    stepsSinceLastTurn = 0; // Reset step counter after turn
                }
            }
        }

        return path;
    }

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

        // Randomly choose between the two perpendicular directions
        return perpendicularDirections[Random.Range(0, perpendicularDirections.Count)];
    }

    bool IsPositionValid(Vector2Int position, HashSet<Vector2Int> visited)
    {
        return position.x >= 0 && position.x < gridSizeX &&
               position.y >= 0 && position.y < gridSizeY &&
               !visited.Contains(position);
    }

    bool IsAtEdge(Vector2Int position)
    {
        return position.x == 0 || position.x == gridSizeX - 1 ||
               position.y == 0 || position.y == gridSizeY - 1;
    }

    void SetSquareColor(Vector2Int position, Material material)
    {
        Renderer renderer = gridSquares[position.x, position.y].GetComponent<Renderer>();
        renderer.material = material;
    }

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

    void ReverseLists()
    {
        for (int i = 0; i < pathPositions.Count; i++)
        {
            pathPositions[i].positions.Reverse();
        }
    }
}

[System.Serializable]
public class PathData
{
    public List<Vector3> positions = new List<Vector3>();
}
