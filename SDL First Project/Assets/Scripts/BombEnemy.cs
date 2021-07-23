using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Enemy
{
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
    
}
