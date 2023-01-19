using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private Rigidbody2D theRB;

    public float force = 10f, lifetime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        theRB.velocity = new Vector2(Random.Range(-force, force), Random.Range(force * .5f, force));
        Destroy(gameObject, lifetime);

        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
    }
}
