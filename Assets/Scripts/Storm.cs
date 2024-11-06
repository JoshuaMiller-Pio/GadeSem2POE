using System.Collections;
using UnityEngine;

public class Storm : MonoBehaviour
{
    public float speed = 5f; // Speed of the storm’s movement
    private Vector3 targetPosition;
    private Vector3 borderPosition;
    private bool movingToBorder = false;

    // Initialize the storm with its target tile
    public void Initialize(Vector3 targetTile)
    {
        targetPosition = targetTile;
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        // Move toward the target tile
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        // Once at the target, set a new random border position
        SetRandomBorderPosition();
        movingToBorder = true;

        // Move toward the border position
        while (Vector3.Distance(transform.position, borderPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, borderPosition, speed * Time.deltaTime);
            yield return null;
        }

        // Destroy the storm once it reaches the border
        Destroy(gameObject);
    }

    private void SetRandomBorderPosition()
    {
        // Select a random position from the GameManager's border tiles
        int randomIndex = Random.Range(0, GameManager.Instance.Border.Count);
        GameObject borderTile = GameManager.Instance.Border[randomIndex];
        borderPosition = borderTile.transform.position;
    }
}

