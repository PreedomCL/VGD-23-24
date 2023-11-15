using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperPlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    //[SerializeField] private float maxFallSpeed = 10;
    [SerializeField] private float deathFallSpeed = 5;
    [SerializeField] private Transform groundCollider;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float respawnTime = 2;

    private float groundColliderRadius;

    private Rigidbody2D body;

    private float xVelocity = 0;
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
        checkpointX = transform.position.x;
        checkpointY = transform.position.y;
    }

    public void OnMove(float direction)
    {
        xVelocity = direction * speed;
    }

    private void FixedUpdate()
    {
        if(playerDead)
        {
            body.velocity = new Vector2(0, body.velocity.y);
            return;
        }
        float modifiedXVelocity = (signedVelocityModifier != 0 && xVelocity /signedVelocityModifier > 0) ? Mathf.Abs(signedVelocityModifier) : 1
            * xVelocity + addVelocity;
        body.velocity = new Vector2(modifiedXVelocity, body.velocity.y);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCollider.position, groundColliderRadius, groundLayerMask);

        if(colliders.Length > 0 && Mathf.Abs(body.velocity.y) > deathFallSpeed)
        {
            PlayerDied();
        }
    }

    public void PlayerDied()
    {
        Debug.Log("Player Dead");
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
        playerDead = false;
        transform.position = new Vector3(checkpointX, checkpointY, 0);
        body.velocity = Vector3.zero;
    }
}
