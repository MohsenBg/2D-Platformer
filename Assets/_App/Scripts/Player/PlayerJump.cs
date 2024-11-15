using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField][Range(0, 10)] private float jumpPower = 5f;
    [SerializeField][Range(0, 10)] private float distanceRaycast = 1f;
    [SerializeField] private Transform playerFoot;
    [SerializeField] private LayerMask groundLayer;

    private new Rigidbody2D rigidbody2D;
    public bool IsJumping { get; private set; }
    public bool IsDead { get; set; }

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
            return;


        if (IsPlayerOnGround())
        {
            IsJumping = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                float basePower = 100;
                rigidbody2D.AddForce(transform.up * jumpPower * basePower, ForceMode2D.Impulse);
                gameManager.audioManager.Play("jump");
            }
        }
        else
            IsJumping = true;

    }
    bool IsPlayerOnGround()
    {
        Vector3 center = playerFoot.position;
        Vector3 left = center - new Vector3(-0.2f, 0, 0);
        Vector3 right = center - new Vector3(0.2f, 0, 0);

        Vector3[] raycastPostion = { left, right, center };

        bool hitGround = false;
        for (int i = 0; i < 3; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(raycastPostion[i], Vector2.down, distanceRaycast, groundLayer);
            Color colorRaycast = hit.collider != null ? Color.red : Color.green;
            Debug.DrawRay(raycastPostion[i], Vector3.down * distanceRaycast, colorRaycast);
            if (hit.collider != null)
                hitGround = true;
        }

        return hitGround;
    }

}