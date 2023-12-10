using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float lifeSpan;

    private Vector2 movement;

    public void setMovement(Quaternion rotation)
    {
        movement = new Vector2(Mathf.Cos(rotation.eulerAngles.z * Mathf.PI/180f),Mathf.Sin(rotation.eulerAngles.z * Mathf.PI/180f));
    }

    private void Update()
    {
        lifeSpan = lifeSpan - Time.deltaTime;
        if (lifeSpan <= 0 )
        {
            Destroy(this.gameObject);
        }

        transform.position = new Vector2(transform.position.x + movement.x * speed, transform.position.y + movement.y * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage);
            if (collision.gameObject.GetComponent<Enemy>().getHealth() <= 0)
            {
                collision.gameObject.GetComponent<Enemy>().onDeath();
            }

            Destroy(this.gameObject);
        }
    }
}
