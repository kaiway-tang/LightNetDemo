using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Projectile
{
    [SerializeField] ParticleSystem trail;
    [SerializeField] float speed;

    public override void Init(ushort ownerID, float timeDelta)
    {
        base.Init(ownerID, timeDelta);
        trfm.position += trfm.forward * speed * timeDelta;        
    }

    // Update is called once per frame
    void Update()
    {
        trfm.position += trfm.forward * speed * Time.deltaTime;
    }

    HPEntity target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer == 8)
        {
            if (other.gameObject.layer == 6)
            {
                target = other.GetComponent<HPEntity>();
                if (target.objID != ownerID)
                {
                    target.TakeDamage(damage, ownerID);
                }
                else
                {
                    return;
                }
            }
            End();
        }
    }

    protected override void End()
    {
        trail.Stop();
        trail.transform.parent = null;
        Destroy(trail.gameObject, 3);
        Destroy(gameObject);
    }
}
