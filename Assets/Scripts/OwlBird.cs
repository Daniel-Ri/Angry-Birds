using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBird : Bird
{
    [SerializeField]
    public float _explodeForce = 40f;
    public float _radius = 5f;
    public bool _hasExploded = false;

    public override void OnExplode()
    {
        Explode();
        Destroy(gameObject);
    }

    public void Explode()
    {
        if (State == BirdState.HitSomething && !_hasExploded)
        {
            // Mendapatkan semua collider object di sekitar area burung dengan besar _radius
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius);

            foreach (Collider2D nearbyObject in colliders)
            {
                Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();

                // Mengecek apakah objek di sekitar merupakan objek nyata
                if (rb != null)
                {
                    Vector2 direction = rb.transform.position - transform.position;
                    rb.AddForce(direction * _explodeForce);
                }
            }

            _hasExploded = true;
        }
    }
}
