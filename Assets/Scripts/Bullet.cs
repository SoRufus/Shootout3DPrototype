using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] [Range(0f, 100f)] private int bulletSpeed;
    [SerializeField] private int destroyTime;


    void OnEnable()
    {
        Invoke("OnCollisionEnter", destroyTime);
    }

    void Update()
    {
        Movement();
    }

    private void OnCollisionEnter()
    {
        gameObject.SetActive(false);
        PlayerMovement.bulletPoolList.Add(gameObject); 
    }

    void Movement()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
    }
}
