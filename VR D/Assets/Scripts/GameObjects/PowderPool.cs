using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowderPool : SingletonObjectPool<PowderPool>
{
    protected override void Awake()
    {
        base.Awake();
        Setup();
    }

    public ParticleSystem GetParticle()
    {
        return GetObject().GetComponent<ParticleSystem>();
    }
}
