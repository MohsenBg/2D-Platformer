using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(UIDocument))]
public class ScoreMenu : MonoBehaviour
{
    private UIDocument uiDocument;

    private List<ScoreModel> scores;
    private ScoreService scoreService;

    private List<VisualElement> rows = new();

    [SerializeField] private GameObject LevelMenu;


    void OnEnable()
    {
        scoreService = GameManager.Instance.dbManager.ScoreService;
        uiDocument = gameObject.GetComponent<UIDocument>();

        var root = uiDocument.rootVisualElement;
        var tableElement = root.Query<ScrollView>().First();
        var dropdown = root.Query<DropdownField>("Levels").First();


        AddEventClickToButton("Back", OnBackButtonClick);

        scores = GetAllScore();
        InitializeDropdown(dropdown);

        CreateTable(tableElement);

    }

    private void OnBackButtonClick(ClickEvent _)
    {
        LevelMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    void Update()
    {

    }

    private List<ScoreModel> GetAllScore()
    {
        return scoreService.GetAll()
        .OrderBy(e => e.Level)
        .OrderByDescending(e => CalculateScore(e.CoinsCollected, e.TimeTaken))
        .ToList();
    }
    private List<ScoreModel> GetAllByLevel(int level)
    {
        return scoreService.GetAllByLevel(level)
        .OrderByDescending(e => CalculateScore(e.CoinsCollected, e.TimeTaken))
        .ToList();
    }
    private List<ScoreModel> GetScoreFilterByLevel(int level)
    {
        return scores.AsQueryable().Where(score => score.Level == level).ToList();
    }


    private void CreateTable(ScrollView tableElement)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            var score = scores[i];
            var rowBody = new VisualElement();
            rowBody.AddToClassList(i % 2 == 0 ? "RowBodyOdd" : "RowBodyEven");
            rows.Add(rowBody);

            if (i == scores.Count - 1)
            {
                rowBody.AddToClassList("RowEnd");
            }

            rowBody.Add(MakeColumn((i + 1).ToString()));
            rowBody.Add(MakeColumn(score.Level.ToString()));
            rowBody.Add(MakeColumn(FormatTime(score.TimeTaken), 30));
            rowBody.Add(MakeColumn(score.CoinsCollected.ToString()));
            rowBody.Add(MakeColumn(CalculateScore(score.CoinsCollected, score.TimeTaken).ToString()));

            tableElement.Add(rowBody);
        }
    }

    private void ClearTable()
    {
        foreach (var row in rows)
            row.parent.Remove(row);

        rows.Clear();
    }
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        float fraction = timeInSeconds - Mathf.Floor(timeInSeconds);
        return $"{minutes:D2}:{seconds:D2}.{fraction:00}";
    }

    private VisualElement MakeColumn(string text, int width = 20)
    {
        var baseCol = new VisualElement();
        var rowLabel = new Label { text = text };
        rowLabel.AddToClassList("LabelStyle");
        baseCol.AddToClassList("ColumnBody");
        baseCol.style.width = new StyleLength(new Length(width, LengthUnit.Percent));

        baseCol.Add(rowLabel);

        return baseCol;
    }

    int CalculateScore(int coinsCollected, float timePassed)
    {
        const int baseScore = 100;
        float score = coinsCollected / timePassed * baseScore;
        return Mathf.RoundToInt(score);
    }


    private void AddEventClickToButton(string btnName, EventCallback<ClickEvent> clickEvent)
    {
        if (uiDocument == null) return;

        var root = uiDocument.rootVisualElement;
        var button = root.Query<Button>(btnName).First();
        button?.RegisterCallback(clickEvent);
    }

    private void InitializeDropdown(DropdownField dropdown)
    {
        dropdown.choices.AddRange(new List<string>() { "All", "1", "2", "3", "4", "5" });
        dropdown.index = 0;
        dropdown.RegisterValueChangedCallback(OnDropdownFiledValueChanged);
    }


    private void OnDropdownFiledValueChanged(ChangeEvent<string> evt)
    {
        ClearTable();

        uiDocument = gameObject.GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
        var tableElement = root.Query<ScrollView>().First();

        scores = evt.newValue switch
        {

            "1" => GetAllByLevel(1),
            "2" => GetAllByLevel(2),
            "3" => GetAllByLevel(3),
            "4" => GetAllByLevel(4),
            "5" => GetAllByLevel(5),
            _ => GetAllScore()
        };

        CreateTable(tableElement);

    }
}
