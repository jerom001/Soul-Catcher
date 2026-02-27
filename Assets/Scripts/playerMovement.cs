using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb2D;
    public float minX = -8;
    public float maxX = 8;
    Animator anim;
    private Coroutine glowRoutine;

    private float moveInput;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if (moveInput > 0)
            transform.right = Vector3.right;
        else if (moveInput < 0)
            transform.right = Vector3.left;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Attack");
        }
    }

    void FixedUpdate()
    {
        rb2D.linearVelocity = new Vector2(moveInput * moveSpeed, rb2D.linearVelocity.y);

        float clampedX = Mathf.Clamp(rb2D.position.x, minX, maxX);
        rb2D.position = new Vector2(clampedX, rb2D.position.y);
    }

    public void TriggerGlow()
    {
        if (glowRoutine != null)
            StopCoroutine(glowRoutine);

        glowRoutine = StartCoroutine(GlowPulse());
    }


    IEnumerator GlowPulse()
     {
         SpriteRenderer sr = GetComponent<SpriteRenderer>();
         Color original = sr.color;

         sr.color = original * 1.6f;

         yield return new WaitForSeconds(0.1f);

         sr.color = original;
     }
}
