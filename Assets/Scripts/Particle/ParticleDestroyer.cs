using System;
using System.Collections;
using UnityEngine;
public class ParticleDestroyer : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    void OnEnable()
    {
      Invoke("DestroyParticle",destroyTime);
    }
    void DestroyParticle()
    {
        if(gameObject.name=="DefualtBulletImpact(Clone)")
            BlastParticlePoolManager.Instance.ReleaseParticle(gameObject);
        else  if(gameObject.name=="Blood(Clone)")
            BloodParticlePoolManager.Instance.ReleaseParticle(gameObject);
        else if(gameObject.name=="SuicideParticle(Clone)")
            gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        if(gameObject.name=="HeadBlood(Clone)")
            Invoke("DestroyHeadBlood",2);
    }

    private void DestroyHeadBlood()
    {
            HeadBloodPoolManager.Instance.ReleaseParticle(gameObject);
    }
}
