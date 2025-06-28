using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float moveSpeed = 3f;

    private Transform playerTransform;

    [SerializeField] int rotateSpeed = 2;

    void Start()
    {
        // Find the player in the scene (make sure there's only one with the tag "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found in the scene.");
        }
    }

    void Update()
    {

        detectplayer();

        
    }

    void detectplayer()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            // Chase the player
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Optional: Rotate to face the player
            transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
        }
    }

    void rotatestun()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
        detectionRadius = 0;
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Gale" && other.attachedRigidbody)
        {
            rotatestun();
        }
    }
}