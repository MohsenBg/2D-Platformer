using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsRunning { get; private set; }
    [SerializeField] private bool patrol = false;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField][Range(0f, 10f)] private float speed = 1f;
    [SerializeField][Range(0, 10f)] private float accuracy = 1f;

    private Transform currentTarget;
    void Start()
    {
        currentTarget = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol)
        {
            Vector3 goal = currentTarget.position;
            goal.y = transform.position.y;

            int direction = goal.x > transform.position.x ? 1 : -1;

            if (Vector2.Distance(goal, transform.position) < accuracy)
                currentTarget = currentTarget == pointA ? pointB : pointA;

            transform.right = new Vector3(direction, 0, 0);

            transform.Translate(Vector2.right * speed * Time.deltaTime);
            IsRunning = true;
        }
        else
            IsRunning = false;
    }
}
