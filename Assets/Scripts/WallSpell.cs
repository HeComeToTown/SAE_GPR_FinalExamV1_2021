using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpell : MonoBehaviour
{
    [SerializeField] private float selfdestructTime = 10;
    [SerializeField] private float riseSpeed = 10;

    private float spawnPointY;

    private void Start()
    {
        spawnPointY = transform.position.y;
        Destroy(gameObject, selfdestructTime);
    }

    private void Update()
    {
        if (transform.position.y < spawnPointY + transform.localScale.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + riseSpeed * Time.deltaTime, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, spawnPointY + transform.localScale.y, transform.position.z);
        }
    }
}