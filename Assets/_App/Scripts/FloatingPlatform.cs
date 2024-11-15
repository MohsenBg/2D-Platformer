using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{

    [SerializeField] private Vector2 pointA;

    [SerializeField] private Vector2 pointB;

    [SerializeField] private float speed = 2.0f;

    private Vector2 targetPoint;
    private bool movingToB = true;
    private Transform playerParent;

    private void Start()
    {
        targetPoint = (Vector2)transform.position + pointB;
    }

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        if (Vector2.Distance(transform.position, targetPoint) < 0.01f)
        {
            if (movingToB)
                targetPoint = (Vector2)transform.position + pointA;

            else
                targetPoint = (Vector2)transform.position + pointB;

            movingToB = !movingToB;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 posA = (Vector2)transform.position + pointA;
        Vector2 posB = (Vector2)transform.position + pointB;

        DrawCircle(posA, 0.5f, Color.red);
        DrawCircle(posB, 0.5f, Color.red);
        DrawLine(posA, posB, Color.blue);
    }

    private void DrawCircle(Vector2 position, float radius, Color gizmoColor)
    {
        Gizmos.color = gizmoColor;
        float segments = 36f;

        float angleIncrement = 360f / segments;

        Vector2 prevPoint = position + new Vector2(radius, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = angleIncrement * i;
            float rad = Mathf.Deg2Rad * angle;

            Vector2 newPoint = position + new Vector2(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius);

            Gizmos.DrawLine(prevPoint, newPoint);

            prevPoint = newPoint;
        }
    }

    private void DrawLine(Vector2 posA, Vector2 posB, Color gizmoColor)
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(posA, posB);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerParent = collision.gameObject.transform.parent;
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(playerParent);
        }
    }
}
