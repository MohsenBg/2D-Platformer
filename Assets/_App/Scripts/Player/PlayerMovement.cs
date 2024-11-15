using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public bool IsRunning { get; private set; }
    public bool IsDead { get; set; }

    [SerializeField][Range(0, 10f)] private float speed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
            return;


        int direction = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.right = new Vector3(1, 0, 0);
            direction = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.right = new Vector3(-1, 0, 0);
            direction = 1;
        }

        if (direction > 0)
            IsRunning = true;
        else
            IsRunning = false;
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }
}