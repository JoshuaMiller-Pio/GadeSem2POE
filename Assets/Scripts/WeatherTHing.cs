using System;
using UnityEngine;
using System.Linq;
using Characters.Defenders;
using Random = UnityEngine.Random;

public class WeatherThing : MonoBehaviour
{
    public GameObject stormPrefab; // Prefab for the Storm object

    private void Start()
    {
        GameManager.Instance.WeatherWarning += OnWeatherWarning;
    }

    private void OnDisable()
    {
        GameManager.Instance.WeatherWarning -= OnWeatherWarning;
    }

    private void OnWeatherWarning()
    {
        // Check if we are in an active round
        if (!GameManager.Instance.roundActive)
        {
            Debug.Log("WeatherWarning ignored: Not currently in a round.");
            return; // Do not spawn storms if not in a round
        }

        // Instantiate the Storm object when the event goes off
        GameObject stormInstance = Instantiate(stormPrefab, GetRandomStartPosition(), Quaternion.identity);
        Storm stormScript = stormInstance.GetComponent<Storm>();
        
        // Set the highest-level or lowest-rating tower tile as the target
        Vector3 targetTile = GetTargetTowerTile();
        stormScript.Initialize(targetTile);
    }

    private Vector3 GetRandomStartPosition()
    {
        // Select a random border tile from the GameManager's Border list
        int randomIndex = Random.Range(0, GameManager.Instance.Border.Count);
        GameObject borderTile = GameManager.Instance.Border[randomIndex];
        return borderTile.transform.position;
    }

    private Vector3 GetTargetTowerTile()
    {
        // Access the list of towers from the GameManager
        var towers = GameManager.Instance.spawnedDefenders;

        // Find the highest-level tower 
        var highestLevelTower = towers
            .Where(t => t.GetComponent<MeleeDefender>() != null || t.GetComponent<BuffTower>() != null)
            .OrderByDescending(t => t.GetComponent<MeleeDefender>()?.defenderScript.Level ?? t.GetComponent<BuffTower>()?.defenderScript.Level)
            .FirstOrDefault();

        // Find the lowest-rating tower 
        var lowestRatingTower = towers
            .Where(t => t.GetComponent<MeleeDefender>() != null || t.GetComponent<BuffTower>() != null)
            .OrderBy(t => t.GetComponent<MeleeDefender>()?.Rrating ?? t.GetComponent<BuffTower>()?.Rrating).FirstOrDefault();

        // Define priority to target the highest-level tower first, then lowest-rating if none found
        GameObject targetTower = highestLevelTower != null ? highestLevelTower : lowestRatingTower;

        // Return the position of the selected target tower tile
        return targetTower != null ? targetTower.transform.position : Vector3.zero;
    }
}
