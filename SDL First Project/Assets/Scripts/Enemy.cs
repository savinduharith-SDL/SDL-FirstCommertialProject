using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]protected float moveSpeed = 3f;
    protected Vector2 moveVector;
    // Start is called before the first frame update
    void Start()
    {
        moveVector = new Vector2(moveSpeed * Time.deltaTime, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    protected virtual void ChangeDirection()
    {
        transform.localScale = new Vector2(-(transform.localScale.x),1);
        moveSpeed = Mathf.Sign(transform.localScale.x)*Mathf.Abs(moveSpeed);
        moveVector = new Vector2(moveSpeed * Time.deltaTime, 0);
    }
    protected void Move()
    {
        transform.Translate(moveVector);
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyBoundary"))
        {
            ChangeDirection();
        }
    }
}
