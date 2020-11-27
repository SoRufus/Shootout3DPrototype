using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] [Range(0f, 100f)] private int bulletSpeed;

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        Movement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    void Movement()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
    }
}
