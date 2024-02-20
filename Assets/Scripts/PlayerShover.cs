using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShover : MonoBehaviour
{
    [SerializeField] private float addVelocity = 0;
    [SerializeField] private float addAccel = 0;
    [SerializeField] private float signedVelocityModifier = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SidePlayerController controller = collision.GetComponent<SidePlayerController>();
            if(controller != null)
            {
                controller.addVelocity = addVelocity;
                controller.signedVelocityModifier = signedVelocityModifier;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SidePlayerController controller = collision.GetComponent<SidePlayerController>();
            if (controller != null)
            {
                controller.addVelocity += addAccel * Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SidePlayerController controller = collision.GetComponent<SidePlayerController>();
            if (controller != null)
            {
                controller.addVelocity = 0;
                controller.signedVelocityModifier = 0;
            }
        }
    }
}
