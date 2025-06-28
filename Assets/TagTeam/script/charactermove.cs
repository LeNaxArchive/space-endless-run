using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public BoxCollider playerCollider;
    public float moveSpeed = 5f;
    public bool walking;

    void Awake()
    {
        if (playerAnim == null)
            playerAnim = GetComponent<Animator>();

        if (playerRigid == null)
            playerRigid = GetComponent<Rigidbody>();

        if (playerCollider == null)
            playerCollider = GetComponent<BoxCollider>();
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection = transform.right * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -transform.right * moveSpeed;
        }

        playerRigid.linearVelocity = moveDirection;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("slide");
            playerAnim.ResetTrigger("running");
            walking = true;

            StartCoroutine(DisableColliderTemporarily());
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("slide");
            playerAnim.SetTrigger("running");
            walking = false;
        }
    }

    IEnumerator DisableColliderTemporarily()
    {
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
            yield return new WaitForSeconds(1f);
            playerCollider.enabled = true;
        }
    }
}
