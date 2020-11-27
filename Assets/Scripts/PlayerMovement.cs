using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0f , 1000f)] private float rotationSensibility;
    [SerializeField] [Range(0f, 300f)] private float defaultBalisticRadius;
    [SerializeField] [HideInInspector] private LineRenderer bulletLine;
    [SerializeField] [HideInInspector] private ParticleSystem gunParticles;
    [SerializeField] private GameObject bulletPrefab;
    float newRotation;

    void Update()
    {
        Movement();
        BalisticView();
    }

    public void Rotate(InputAction.CallbackContext value)
    {
        newRotation = value.ReadValue<float>();
    }

    public void Movement()
    {
        transform.rotation *= Quaternion.Euler(0, newRotation * Time.deltaTime * rotationSensibility, 0);
    }

    public void Shoot(InputAction.CallbackContext value)
    {
        var shot = value.ReadValue<float>();
        if (shot == 0)
        {
            GameControler.SoundPlayer(1);

            gunParticles.Play();

            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
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

            var angle = Mathf.PI * 2 * transform.rotation.eulerAngles.y/360;
            bulletLine.SetPosition(0, new Vector3(balisticRadius * Mathf.Sin(angle), 0.3f, balisticRadius * Mathf.Cos(angle)));
            bulletLine.SetPosition(1, new Vector3(0, 0.3f, 0));
        }
    }