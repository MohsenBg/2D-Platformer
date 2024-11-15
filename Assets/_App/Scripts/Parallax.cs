using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startPosition;

    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;
    [SerializeField] private float smoothingFactor = 5f; // Adjust smoothing factor as needed

    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }


    void LateUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        Vector3 targetPosition = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothingFactor);
    }

}