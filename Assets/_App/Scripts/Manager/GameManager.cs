using UnityEngine;
public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance { get; set; }
    public AudioManager audioManager;
    public DBManager dbManager;


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
            dbManager = GetComponent<DBManager>();
            dbManager.ScoreService.CreateDB();
            audioManager = GetComponent<AudioManager>();
            audioManager.Play("music");
        }
    }
}
