using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsRunning { get; private set; }
    [SerializeField] private bool patrol = false;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField][Range(0f, 10f)] private float speed = 1f;
    [SerializeField][Range(0, 10f)] private float accuracy = 1f;
    [SerializeField][Range(0f, 10f)] private float idleTime = 2f; // Time to idle at each point

    private const string IS_RUNNING = "IsRunning";

    private Transform currentTarget;
    private Animator animator;
    private bool isIdling = false;

    void Start()
    {
        currentTarget = pointA;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol && !isIdling)
        {
            Vector3 goal = currentTarget.position;
            goal.y = transform.position.y;

            int direction = goal.x > transform.position.x ? 1 : -1;

            if (Vector2.Distance(goal, transform.position) < accuracy)
            {
                StartCoroutine(IdleAtPoint());
            }
            else
            {
                transform.right = new Vector3(direction, 0, 0);
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                IsRunning = true;
            }
        }
        else
        {
            IsRunning = false;
        }

        if (animator != null)
        {
            animator.SetBool(IS_RUNNING, IsRunning);
        }
    }

    private IEnumerator IdleAtPoint()
    {


        isIdling = true;
        IsRunning = false;

        yield return new WaitForSeconds(idleTime);
        currentTarget = currentTarget == pointA ? pointB : pointA;

        isIdling = false;
        IsRunning = true;
    }
}
