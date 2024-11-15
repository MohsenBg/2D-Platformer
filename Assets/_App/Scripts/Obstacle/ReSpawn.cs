using UnityEngine;
using UnityEngine.SceneManagement;

public class ReSpawn : MonoBehaviour
{
    private GameManager gameManager;
    private CoinScore coinScore;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GameManager>();
        coinScore = GameObject.FindGameObjectWithTag("PlayTimeUI")?.GetComponent<CoinScore>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Player")
        {
            onHitPlayer(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Player")
        {
            onHitPlayer(collision.gameObject);
        }
    }

    private void onHitPlayer(GameObject player)
    {
        coinScore.Score = 0;
        gameManager.audioManager.Play("hit");
        var playerAnimation = player.GetComponent<PlayerAnimation>();

        // Debug.Log(this.gameObject.transform.parent.transform.parent.name);
    
        playerAnimation.PlayDeadAndThen(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }
}
