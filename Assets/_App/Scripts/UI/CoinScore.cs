using UnityEngine;
using UnityEngine.UIElements;

public class CoinScore : MonoBehaviour
{
    private UIDocument uiDocument;
    private Label scoreLabel;
    public int Score { get; set; }


    void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
        scoreLabel = root.Query<Label>("CoinLabel").First();
    }

    void Start()
    {
        Score = 0;
        scoreLabel.text = Score.ToString();
    }
    void Update()
    {
        scoreLabel.text = Score.ToString();
    }
}
