using UnityEngine;

public class Finish : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            gameManager.audioManager.Play("win");
            SceneController.LoadNextScene();
        }

    }
}
