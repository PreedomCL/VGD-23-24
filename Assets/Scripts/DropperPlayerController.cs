using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperPlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float maxFallSpeed = 10;

    private Rigidbody2D body;

    private float xVelocity = 0;
    private bool playerDead = false;

    public float addVelocity = 0;
    public float signedVelocityModifier = 0;

    public float checkpointX = -7.43f;
    public float checkpointY = 2.95f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputManager.EnableSideControl();
        inputManager.SideMoveEvent += OnMove;
        inputManager.InteractEvent += OnInteract;
    }

    public void OnMove(float direction)
    {
        xVelocity = direction * speed;
    }

    public void OnInteract()
    {
       RespawnPlayer();
    }

    private void FixedUpdate()
    {
        if(playerDead)
        {
            return;
        }
        float modifiedXVelocity = (signedVelocityModifier != 0 && xVelocity /signedVelocityModifier > 0) ? Mathf.Abs(signedVelocityModifier) : 1
            * xVelocity + addVelocity;
        body.velocity = new Vector2(modifiedXVelocity, body.velocity.y);
    }

    public void PlayerDied()
    {
        playerDead = true;
    }

    public void RespawnPlayer()
    {
        playerDead = false;
        transform.position = new Vector3(checkpointX, checkpointY, 0);
    }
}
