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

    private void OnEnable()
    {
        // Subscribe to the WeatherWarning event
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        GameManager.Instance.WeatherWarning -= OnWeatherWarning;
    }

    private void OnWeatherWarning()
    {
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

        // Filter for towers that are either MeleeDefender or BuffDefender and find the highest level or lowest rating
        var highestLevelTower = towers
            .Select(t => t.GetComponent<MeleeDefender>()?.defenderScript?.Level ?? t.GetComponent<BuffTower>()?.defenderScript?.Level)
            .Where(level => level.HasValue)
            .OrderByDescending(level => level)
            .Select(level => towers.First(t => (t.GetComponent<MeleeDefender>()?.defenderScript?.Level == level || t.GetComponent<BuffTower>()?.defenderScript?.Level == level))).FirstOrDefault();

        var lowestRatingTower = towers
            .Select(t => t.GetComponent<MeleeDefender>()?.Rrating ?? t.GetComponent<BuffTower>()?.Rrating)
            .Where(rating => rating.HasValue)
            .OrderBy(rating => rating)
            .Select(rating => towers.First(t => (t.GetComponent<MeleeDefender>()?.Rrating == rating || t.GetComponent<BuffTower>()?.Rrating == rating))).FirstOrDefault();

        // Define priority to target the highest-level tower first, then lowest-rating if none found
        GameObject targetTower = highestLevelTower != null ? highestLevelTower : lowestRatingTower;

        // Return the position of the selected target tower tile
        return targetTower != null ? targetTower.transform.position : Vector3.zero;
    }
}
