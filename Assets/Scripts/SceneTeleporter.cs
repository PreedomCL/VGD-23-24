using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    [SerializeField] private string switchToScene;
    [SerializeField] private GameObject indicator;
    [SerializeField] private InputManager inputManager;

    private bool inRange = false;

    private void OnEnable()
    {
        inputManager.InteractEvent += OnInteract;
        indicator.SetActive(false);
    }

    private void Update()
    {
        if (inRange)
        {
            indicator.SetActive(true);
        } else
        {
            indicator.SetActive(false);
        }
    }

    public void OnInteract()
    {
        if(inRange)
        {
            SceneManager.LoadScene(switchToScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
