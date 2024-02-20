using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimOffset : MonoBehaviour
{
    [SerializeField] private string animationName;
    void Start()
    {
        Animator animatior = GetComponent<Animator>();
        float offset = Random.Range(0f, 1f);
        animatior.Play(animationName, 0, offset);
    }

    
}
