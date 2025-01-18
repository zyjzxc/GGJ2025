using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLoop : MonoBehaviour
{
    private float scrollSpeed;
    private float imageHeight = 13.9f;

    void Start()
    {
    }

    void Update()
    {
        scrollSpeed = GameManager._instance.upSpeed;

        float newYPosition = transform.position.y - scrollSpeed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

        if (transform.position.y < -imageHeight)
        {
            transform.position = new Vector3(transform.position.x, imageHeight, transform.position.z);
        }
    }
}

