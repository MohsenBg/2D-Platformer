using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Table : MonoBehaviour
{
    [SerializeField] private List<ScoreModel> scores = new();

    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
        var tableElement = root.Query<ScrollView>().First();

        scores = CreateSampleScores();

        CreateTable(tableElement);
    }

    private void CreateTable(ScrollView tableElement)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            var score = scores[i];
            var rowBody = new VisualElement();
            rowBody.AddToClassList(i % 2 == 0 ? "RowBodyOdd" : "RowBodyEven");

            if (i == scores.Count - 1)
            {
                rowBody.AddToClassList("RowEnd");
            }

            rowBody.Add(MakeColumn((i + 1).ToString()));
            rowBody.Add(MakeColumn(FormatTime(score.TimeTaken)));
            rowBody.Add(MakeColumn(score.CoinsCollected.ToString()));
            rowBody.Add(MakeColumn("100")); // Assuming "100" is a placeholder or static value


            tableElement.Add(rowBody);
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        float fraction = timeInSeconds - Mathf.Floor(timeInSeconds);
        return $"{minutes:D2}:{seconds:D2}.{fraction:00}";
    }

    private VisualElement MakeColumn(string text)
    {
        var baseCol = new VisualElement();
        var rowLabel = new Label { text = text };
        baseCol.AddToClassList("ColumnBody");
        baseCol.Add(rowLabel);

        return baseCol;
    }


    private List<ScoreModel> CreateSampleScores()
    {
        return new List<ScoreModel>
        {
            new ScoreModel { Id = 1, Level = 1, TimeTaken = 120.5f, CoinsCollected = 10 },
            new ScoreModel { Id = 2, Level = 2, TimeTaken = 95.3f, CoinsCollected = 15 },
            new ScoreModel { Id = 3, Level = 3, TimeTaken = 200.0f, CoinsCollected = 20 },
            new ScoreModel { Id = 4, Level = 4, TimeTaken = 150.1f, CoinsCollected = 25 },
            new ScoreModel { Id = 5, Level = 5, TimeTaken = 180.2f, CoinsCollected = 30 }
        };
    }

}
