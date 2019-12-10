using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed; 
    public int jumpForce;
    public int health;
    public Transform groundCheck;
    
    private bool invunerable = false;
    private bool grounded = false;
    private bool jumping = false;
    private bool facingRight = true;

    private   SpriteRenderer sprite;
    private RigidBody2D rb2d;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
