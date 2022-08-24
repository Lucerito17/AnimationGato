using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleController : MonoBehaviour
{
    public float velocity = 10;
    public float velocityJump = 8;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;

    const int ANIMATION_CORRER = 1;
    const int ANIMATION_QUIETO = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Iniciando Script de Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Correr();
        Saltar();
        Render();//para que la animacion de la vuelta
        CheckGround();
    }

    private void Correr()
    {
        //Avanzar (A)
        //avanzar con teclas A y D
        if (Input.GetKey(KeyCode.D)){
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            ChangeAnimation(ANIMATION_CORRER);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_QUIETO);
        }
        //Regresar (D)
        if (Input.GetKey(KeyCode.A)){
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            ChangeAnimation(ANIMATION_CORRER);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_QUIETO);
        }
    }

    private void Saltar()
    {
        animator.SetFloat("jumpVelocity", rb.velocity.y);
        //Saltar
        if(!cl.IsTouchingLayers(LayerMask.GetMask("ground"))){return;}
        //si se ejecuta este if es porque es falso(esta en el piso) y saldra del metodo saltar 
        if (Input.GetKeyDown(KeyCode.W))//SALTO
        {
            rb.velocity = new Vector2(rb.velocity.x, velocityJump);
        }
    }

    //metodo para voltear la animacion
    private void Render()
    {
        if(rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        else if(rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
    }

    //habilitar animacion correr
    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }

    //metodo para comprobar si existe suelo y activar la animacion de 
    //saltar y activar la animacion
    private void CheckGround()
    {
        if(cl.IsTouchingLayers(LayerMask.GetMask("ground")))
        {
            animator.SetBool("isGorund", true);
        }
        else
        {
            animator.SetBool("isGorund", false);
        }
    }
}

