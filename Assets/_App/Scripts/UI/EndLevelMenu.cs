using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class EndLevelMenu : MonoBehaviour
{
    private UIDocument uiDocument;
    private Timer timer;
    private CoinScore coinScore;
    private Label coinValueLabel;
    private Label timeValueLabel;
    private Label scoreValue;
    private bool isScoreSaved = false;

    private void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        coinValueLabel = root.Q<Label>("CoinValue");
        timeValueLabel = root.Q<Label>("TimeValue");
        scoreValue = root.Q<Label>("ScoreValue");

        RegisterButtonClickEvents(root);
    }

    private void RegisterButtonClickEvents(VisualElement root)
    {
        AddEventClickToButton(root, "Next", OnClickNext);
        AddEventClickToButton(root, "Home", OnClickHome);
        AddEventClickToButton(root, "Replay", OnClickReplay);
    }

    private void AddEventClickToButton(VisualElement root, string buttonName, EventCallback<ClickEvent> clickEvent)
    {
        var button = root.Q<Button>(buttonName);
        button?.RegisterCallback(clickEvent);
    }

    private void Start()
    {
        var playTimeUI = GameObject.FindGameObjectWithTag("PlayTimeUI");
        coinScore = playTimeUI?.GetComponent<CoinScore>();
        timer = playTimeUI?.GetComponent<Timer>();

        ShowPanel(uiDocument.rootVisualElement.Q<VisualElement>("Panel"));
    }

    private void ShowPanel(VisualElement panel)
    {
        float duration = 1f;
        panel.style.translate = new Translate(new Length(0, LengthUnit.Pixel), new Length(640, LengthUnit.Pixel));
        panel.style.opacity = 0;

        DOTween.To(() => panel.style.translate.value.y.value,
          y => panel.style.translate = new Translate(new Length(0, LengthUnit.Pixel), new Length(y, LengthUnit.Pixel)), 0, duration)
          .SetUpdate(true);

        DOTween.To(() =>
            panel.style.opacity.value, o =>
            panel.style.opacity = o, 1, duration).SetUpdate(true);
    }

    private void Update()
    {

        PauseGame();

        UpdateUI();

        if (!isScoreSaved)
        {
            SaveScore();
            isScoreSaved = true;
        }
    }

    private void PauseGame() => Time.timeScale = 0;

    private void UpdateUI()
    {
        coinValueLabel.text = coinScore.Score.ToString();
        timeValueLabel.text = timer.TimerToString();
        scoreValue.text = CalculateScore(coinScore.Score, (float)timer.time.TotalSeconds).ToString();
    }

    private void SaveScore()
    {
        var gameManager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GameManager>();
        gameManager?.dbManager.ScoreService.Insert(new ScoreModel
        {
            CoinsCollected = coinScore.Score,
            Level = SceneController.CurrentScene,
            TimeTaken = (float)timer.time.TotalSeconds
        });
    }

    private int CalculateScore(int coinsCollected, float timePassed)
    {
        const int baseScore = 100;
        float score = (coinsCollected / timePassed) * baseScore;
        return Mathf.RoundToInt(score);
    }

    private void OnClickNext(ClickEvent _)
    {
        ResumeGame();
        SceneController.LoadNextScene();
    }

    private void OnClickReplay(ClickEvent _)
    {
        ResumeGame();
        SceneController.LoadCurrentScene();
    }

    private void OnClickHome(ClickEvent _)
    {
        ResumeGame();
        SceneController.LoadFirstScene();
    }

    private void ResumeGame() => Time.timeScale = 1;



}
