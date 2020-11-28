using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0f, 1000f)] private float rotationSensibility;
    [SerializeField] [Range(0f, 300f)] private float defaultBalisticRadius;

    [SerializeField] [HideInInspector] private LineRenderer bulletLine;
    [SerializeField] [HideInInspector] private ParticleSystem gunParticles;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPool;
    [SerializeField] private int bulletPoolSize;

    public static List<GameObject> bulletPoolList;

    float newRotation;

    void Start()
    {
        bulletPoolList = new List<GameObject>();
        PoolCreate();
    }

    void PoolCreate()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.SetActive(false);
            bullet.transform.parent = bulletPool.transform;
            bulletPoolList.Add(bullet);
        }
    }

    void Update()
    {
        Movement();
        BalisticView();
    }

    public void Movement()
    {
        transform.rotation *= Quaternion.Euler(0, newRotation * Time.deltaTime * rotationSensibility, 0);
    }

    public void Rotate(InputAction.CallbackContext value)
    {
        newRotation = value.ReadValue<float>();
    }

    void BalisticView()
    {
        RaycastHit rayHit;
        float balisticRadius;

        if (Physics.Raycast(transform.position, this.transform.forward, out rayHit))
        {
            if (rayHit.collider.tag == "Bullet") return;

            balisticRadius = rayHit.distance;
        }
        else
        {
            balisticRadius = defaultBalisticRadius;
        }

        var angle = Mathf.PI * 2 * transform.rotation.eulerAngles.y / 360;
        bulletLine.SetPosition(0, new Vector3(balisticRadius * Mathf.Sin(angle), 0.3f, balisticRadius * Mathf.Cos(angle)));
        bulletLine.SetPosition(1, new Vector3(0, 0.3f, 0));
    }


public void Shoot(InputAction.CallbackContext value)
    {
        var shot = value.ReadValue<float>();
        if (shot == 0)
        {
            GameControler.SoundPlayer(1);

            gunParticles.Play();

            PoolSpawn();
        }
    }

    void PoolSpawn()
    {
        var bullet = bulletPoolList[0];
        bullet.SetActive(true);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bulletPoolList.Remove(bullet);
    }
}