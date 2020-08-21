using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parcticle : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(_particleSystem.main.duration);
        Destroy(this.gameObject);
    }
}
