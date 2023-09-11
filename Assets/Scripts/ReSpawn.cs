using UnityEngine;
using UnityEngine.SceneManagement;

public class ReSpawn : MonoBehaviour
{
    GameManager gameManager;
    int tmpScore;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GameManager>();
        tmpScore = gameManager.Score;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Player")
        {
            gameManager.SetScore(tmpScore);
            gameManager.audioManager.Play("hit");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
