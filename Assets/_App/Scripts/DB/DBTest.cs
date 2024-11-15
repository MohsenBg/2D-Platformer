using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBTest : MonoBehaviour
{
    private ScoreService _scoreService;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the ScoreService with the name of the database
        _scoreService = new ScoreService();

        // Create the database table if it doesn't exist
        _scoreService.CreateDB();

        // Insert a new score
        var newScore = new ScoreModel
        {
            Level = 1,
            TimeTaken = 112.25f,
            CoinsCollected = 30
        };
        _scoreService.Insert(newScore);

        // Retrieve and log all scores
        var allScores = _scoreService.GetAll();
        Debug.Log("All Scores:");
        foreach (var score in allScores)
        {
            Debug.Log($"ID: {score.Id}, Level: {score.Level}, TimeTaken: {FormatTime(score.TimeTaken)}, CoinsCollected: {score.CoinsCollected}");
        }

        // Retrieve and log scores for a specific level
        var levelScores = _scoreService.GetAllByLevel(1);
        Debug.Log("Scores for Level 1:");
        foreach (var score in levelScores)
        {
            Debug.Log($"ID: {score.Id}, Level: {score.Level}, TimeTaken: {FormatTime(score.TimeTaken)}, CoinsCollected: {score.CoinsCollected}");
        }

        // Remove the score
        // _scoreService.Remove(newScore);

        // Drop the table if it exists
        // _scoreService.DropTable();
    }

    // Update is called once per frame
    void Update()
    {
        // Optional: Add update logic if needed
    }

    // Helper method to format time as mm:ss.xx
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        float fraction = timeInSeconds - Mathf.Floor(timeInSeconds);
        return $"{minutes:D2}:{seconds:D2}.{fraction:00}";
    }
}
