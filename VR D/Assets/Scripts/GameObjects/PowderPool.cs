using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using API;
public class PowderPool : ObjectPoolBase
{
    private void Start()
    {
        Setup();
    }

    public ParticleSystem GetParticle()
    {
        return GetObject().GetComponent<ParticleSystem>();
    }
}
