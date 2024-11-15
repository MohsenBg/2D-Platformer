using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTarget : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private float minY = 5f;

    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = player.transform.position;
        newPosition.y = newPosition.y < minY ? minY : newPosition.y;
        transform.position = newPosition;
    }
}
