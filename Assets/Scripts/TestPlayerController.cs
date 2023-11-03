using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField] private InputManager inputManager;

    private Rigidbody2D playerBody;

    private Vector2 move = Vector2.zero;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        inputManager.TopdownMoveEvent += OnMovement;
    }

    private void OnEnable()
    {
        inputManager.EnableTopdownControl();
    }

    public void OnMovement(Vector2 move)
    {
        this.move = move;
    }

    private void FixedUpdate()
    {
        playerBody.velocity = move * speed;
    }
}
