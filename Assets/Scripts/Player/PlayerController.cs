using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 lastMovedVector;
    [HideInInspector]
    public bool right = true;

    Rigidbody2D rb;
    float inputHorizontal;
    bool facingRight = true;
    PlayerStats player;
    Animator am;
    private AudioSource _audioSource;

    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        lastMovedVector = new Vector2(1, 0f);
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Move();

        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if (inputHorizontal > 0 && !facingRight)
        {
            Flip();
            right = true;
        }
        if (inputHorizontal < 0 && facingRight)
        {
            Flip();
            right = false;
        }
    }

    void InputManagement()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);
        }

        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
        if (moveX != 0 || moveY != 0)
        {
            am.SetBool("Move", true);
            if (!_audioSource.isPlaying)
            { 
            _audioSource.Play();
            }
        }
        else
        {
            am.SetBool("Move", false);
            _audioSource.Stop();
        }
    }

    void Move()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        
        rb.velocity = new Vector2(moveDir.x * player.CurrentMoveSpeed, moveDir.y * player.CurrentMoveSpeed);
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
