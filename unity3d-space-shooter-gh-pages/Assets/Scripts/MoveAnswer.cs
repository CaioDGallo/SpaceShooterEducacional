using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnswer : MonoBehaviour {

    public Transform Asteroid;

    private void Start()
    {
        Destroy(gameObject, 14);
    }

    void Update () {
        if(Asteroid != null)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Asteroid.position.z);
        }
    }
}
