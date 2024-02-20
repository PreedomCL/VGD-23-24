using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength = 10;
    [SerializeField] private float maxJumpTime = 0.2f;
    [SerializeField] private float maxFallSpeed = -10;
    [SerializeField] private Transform groundCollider;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float respawnTime = 2;
    [SerializeField] private Animator animator;

    private float groundColliderRadius = 0.1f;

    private Rigidbody2D body;

    private float xVelocity = 0;
    private float jumpTime = 0;

    private bool playerDead = false;

    public float addVelocity = 0;
    public float signedVelocityModifier = 0;

    private float checkpointX = 0;
    private float checkpointY = 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputManager.EnableSideControl();
        inputManager.SideMoveEvent += OnMove;
        inputManager.JumpBeginEvent += OnJumpBegin;
        inputManager.JumpEndEvent += OnJumpEnd;

        checkpointX = transform.position.x;
        checkpointY = transform.position.y;

        animator.SetBool("falling", false);
        animator.SetBool("right", false);
        animator.SetBool("left", false);
    }

    public void OnMove(float direction)
    {
        xVelocity = direction * speed;
        animator.SetBool("right", direction > 0);
        animator.SetBool("left", direction < 0);
    }

    public void OnJumpBegin()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCollider.position, groundColliderRadius, groundLayerMask);
        if (colliders.Length == 0)
            return;

        jumpTime = maxJumpTime;
    }

    public void OnJumpEnd()
    {
        jumpTime = 0;
    }

    private void FixedUpdate()
    {
        if(playerDead)
        {
            body.velocity = new Vector2(0, body.velocity.y);
            return;
        }
        //float modifiedXVelocity = (signedVelocityModifier != 0 && xVelocity / signedVelocityModifier > 0) ? Mathf.Abs(signedVelocityModifier) : 1
        //    * xVelocity + addVelocity;

        float yVelocity = body.velocity.y;
        if(jumpTime > 0)
        {
            jumpTime -= Time.deltaTime;
            yVelocity = jumpStrength * (2 + jumpTime / maxJumpTime) / 3; // num + 1 must = divisor. 1 / divisor is how much the jump decays over time
        }

        body.velocity = new Vector2(xVelocity, Mathf.Max(yVelocity, maxFallSpeed));

        animator.SetBool("falling", body.velocity.y <= maxFallSpeed);
    }

    public void KillPlayer()
    {
        if (playerDead)
            return;
        animator.SetTrigger("die");
        playerDead = true;
        StartCoroutine(RespawnPlayer());
    }
    
    public void SetCheckpoint(Vector2 position)
    {
        checkpointX = position.x;
        checkpointY = position.y;
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnTime);
        animator.SetTrigger("respawn");
        playerDead = false;
        transform.position = new Vector3(checkpointX, checkpointY, 0);
        body.velocity = Vector3.zero;
    }
}
