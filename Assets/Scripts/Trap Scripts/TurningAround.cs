using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningAround : MonoBehaviour
{
    public Transform center;
    public Vector3 axis = Vector3.up;
    public float radius = 2f;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 80f;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = (transform.position - center.position).normalized * radius + center.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
        Vector3 desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.Instance == null)
            return;
        if (collision.attachedRigidbody.gameObject != Player.Instance.gameObject)
            return;

        if (Player.Instance.life > 0)
        {
            Player.Instance.ApplyHit(null);

        }

        
    }
}
