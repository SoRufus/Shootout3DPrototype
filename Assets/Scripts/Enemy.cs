using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rigid;
    private Collider collid;
    [SerializeField] [Range(0f, 80f)] private int deathForce;
    [SerializeField] [Range(0f, 300f)] private int randomTorqueRange;
    [SerializeField] private ParticleSystem damageParticle;
    [SerializeField] private float destroyTime;
    static int enemiesCounter;

    void Start()
    {
        GameControler.enemiesCounter++;
       rigid = gameObject.GetComponent<Rigidbody>();
       collid = gameObject.GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collid.enabled) return;

        rigid.AddForce(collision.transform.position * Time.deltaTime * deathForce * 100);
        rigid.AddTorque(Random.Range(0, randomTorqueRange), Random.Range(0, randomTorqueRange), Random.Range(0, randomTorqueRange));

        GameControler.enemiesCounter--;
        GameControler.ScorePreview();

        GameControler.SoundPlayer(0);

        damageParticle.Play();

        collid.enabled = false;
        Destroy(gameObject, destroyTime);
    }
}
