using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float platSpeed = 2;
    float platMove = 1;
    //public float platRange = 0;
    //float platLowerLimitX = 0;
    //float platUpperLimitX = 0;
    //float platLowerLimitY = 0;
    //float platUpperLimitY = 0;

    //either horizontal or vertical
    public enum MovementType
    {
        Horizontal,
        Vertical
    }

    public MovementType moveType;

    

    Rigidbody2D platBody;

    // Start is called before the first frame update
    void Start()
    {
        platBody = GetComponent<Rigidbody2D>();
        //platLowerLimitX = transform.position.x;
        //platLowerLimitY = transform.position.y;
        //platUpperLimitX = transform.position.x + platRange;
        //platUpperLimitY = transform.position.y + platRange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (moveType == MovementType.Horizontal)
        {
            platBody.MovePosition(new Vector2(transform.position.x + platMove * platSpeed, transform.position.y));
            
        }

        else if (moveType == MovementType.Vertical)
        {
            platBody.MovePosition(new Vector2(transform.position.x, transform.position.y + platMove * platSpeed));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Platform Limit"))
        {
            platMove = -platMove;
        }
    }

}
