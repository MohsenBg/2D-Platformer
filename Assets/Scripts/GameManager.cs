using UnityEngine;
public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance { get; set; }
    public AudioManager audioManager;
    public int Score { get; private set; }


    public void SetScore(int score) { Score = score; }
    public void AddScore(int score) { Score += score; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
            audioManager = GetComponent<AudioManager>();
            audioManager.Play("music");
        }
    }

    private void Update()
    {
        if (Score != 0 && SceneController.CurrentScene == 0)
        {
            Score = 0;
        }
    }
}
