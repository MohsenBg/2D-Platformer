using UnityEngine;

public class Finish : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    GameManager gameManager;
    private GameObject endLevelMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GameManager>();
        endLevelMenu = GameObject.FindGameObjectWithTag("EndLevelMenu");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            gameManager.audioManager.Play("win");
            endLevelMenu.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
