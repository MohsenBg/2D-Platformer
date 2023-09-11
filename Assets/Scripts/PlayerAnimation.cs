using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;

    private const string IS_RUNNING = "IsRunning";
    private const string IS_JUMPING = "IsJumping";

    private void Awake()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
        animator = this.GetComponent<Animator>();
        playerJump = this.GetComponent<PlayerJump>();
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_RUNNING, playerMovement.IsRunning);
        animator.SetBool(IS_JUMPING, playerJump.IsJumping);
    }
}
