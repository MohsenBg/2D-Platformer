using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GameManager>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Player")
        {
            gameManager.audioManager.Play("coin");
            GameObject.FindGameObjectWithTag("PlayTimeUI").GetComponent<CoinScore>().Score += coinValue;
            Destroy(gameObject);
        }
    }

}
