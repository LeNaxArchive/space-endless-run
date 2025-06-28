using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPusher : MonoBehaviour
{
    public float pushRadius = 5f;
    public float pushForce = 500f;
    public float pushCooldown = 2f;
    public AudioClip pushSound;
    public GameObject pushEffectPrefab; // Assign a particle or effect prefab
    public Animator playerAnim;         // Assign the player Animator

    private AudioSource audioSource;
    private float lastPushTime = -Mathf.Infinity;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (playerAnim == null)
        {
            playerAnim = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && Time.time >= lastPushTime + pushCooldown)
        {
            PlayPushSound();
            PlayPushAnimation();
            PlayPushEffect();
            PushNearbyEnemies();
            lastPushTime = Time.time;
        }
    }

    void PushNearbyEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pushRadius);

        foreach (Collider hit in hitColliders)
        {
            if (hit.CompareTag("Enemy"))
            {
                Rigidbody enemyRb = hit.GetComponent<Rigidbody>();

                if (enemyRb != null)
                {
                    Vector3 pushDirection = (hit.transform.position - transform.position).normalized;
                    enemyRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
                }
            }
        }
    }

    void PlayPushSound()
    {
        if (pushSound != null)
        {
            audioSource.PlayOneShot(pushSound);
        }
    }

    void PlayPushAnimation()
    {
        if (playerAnim != null)
        {
            playerAnim.SetTrigger("Push");
        }
    }

    void PlayPushEffect()
    {
        if (pushEffectPrefab != null)
        {
            Instantiate(pushEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pushRadius);
    }
}
