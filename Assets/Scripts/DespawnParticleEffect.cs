using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnParticleEffect : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        Debug.Log(particleSystem.main.duration);
        Destroy(gameObject, particleSystem.main.duration);
    }
}
