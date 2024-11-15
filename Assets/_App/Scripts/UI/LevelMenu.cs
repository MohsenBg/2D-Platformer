using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(UIDocument))]
public class LevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    [SerializeField]
    private GameObject scoreMenu;


    private UIDocument uiDocument;
    private ScoreService scoreService;

    private int openLevel = 1;

    private void OnEnable()
    {
        uiDocument = gameObject.GetComponent<UIDocument>();
        scoreService = GameManager.Instance.dbManager.ScoreService;
        openLevel = scoreService != null ? scoreService.GetMaxLevel() + 1 : 1;

        Debug.Log(scoreService.GetMaxLevel());

        SetupLevelButton("Level1", 1);
        SetupLevelButton("Level2", 2);
        SetupLevelButton("Level3", 3);
        SetupLevelButton("Level4", 4);
        SetupLevelButton("Level5", 5);

        AddEventClickToButton("Back", OnClickBackButton);
        AddEventClickToButton("Score", OnClickScoreButton);
    }

    private void OnClickScoreButton(ClickEvent _)
    {
        scoreMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    private void SetupLevelButton(string btnName, int level)
    {
        if (uiDocument == null) return;

        var root = uiDocument.rootVisualElement;
        var button = root.Query<Button>(btnName).First();

        if (button != null)
        {
            if (level > openLevel)
            {
                DisableButton(button);
            }
            else
            {
                button.RegisterCallback<ClickEvent>(evt => OnClickLevelButton(evt, level));
            }
        }
    }

    private void DisableButton(Button button)
    {
        button.SetEnabled(false);
        button.RemoveFromClassList("LevelButton");
        button.AddToClassList("DisabledButton");
    }

    private void AddEventClickToButton(string btnName, EventCallback<ClickEvent> clickEvent)
    {
        if (uiDocument == null) return;

        var root = uiDocument.rootVisualElement;
        var button = root.Query<Button>(btnName).First();
        button?.RegisterCallback(clickEvent);
    }

    private void OnClickBackButton(ClickEvent _)
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnClickLevelButton(ClickEvent _, int level)
    {
        SceneController.LoadLevelScene(level);
    }
}
