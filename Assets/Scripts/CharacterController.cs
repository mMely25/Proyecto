using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float velocidad;
     public float fuerzaSalto;
     public int saltosMaximos;
     public LayerMask capaSuelo;
    
    private Rigidbody2D Rigidbody;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = false;
    private bool mirandoIzquierda = true;
    private int  saltosRestantes;
    private Animator animator;

     private void Start()
     {
        Rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;
        animator = GetComponent<Animator>();
     }
    
    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
        
    }


     bool EstaEnSuelo()
    {
     RaycastHit2D raycast= Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f,Vector2.down, 0.2f, capaSuelo );
     return raycast.collider != null;
    }

    void ProcesarSalto()
    {
         if(EstaEnSuelo())
         {
            saltosRestantes = saltosMaximos;
         }


        if(Input.GetKeyDown (KeyCode.Space) && saltosRestantes > 0)
        {
            saltosRestantes = saltosRestantes - 1;
            Rigidbody.velocity = new  Vector2(Rigidbody.velocity.x, 0f);
            GetComponent<Rigidbody2D>(). AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }


    void ProcesarMovimiento()
    {
        // Logica de movimiento.
        float inputMovimiento = Input.GetAxis("Horizontal");
        
        if(inputMovimiento != 0f)
        {
            animator.SetBool("Correr", true);
        }
        else
        {
            animator.SetBool("Correr", false);
        }

     Rigidbody.velocity = new Vector2(inputMovimiento * velocidad, Rigidbody.velocity.y);
     GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
             if(Input.GetKeyDown(KeyCode.LeftArrow) && mirandoDerecha){
                Debug.Log("volteando a la izquierda:    "+transform.localScale.x);
                if(transform.localScale.x > 0)
                {
                    transform.localScale = new Vector2(-1*transform.localScale.x, transform.localScale.y);
                }
                mirandoDerecha = false;
                mirandoIzquierda = true;
            }

            
            if(Input.GetKeyDown(KeyCode.RightArrow) && mirandoIzquierda)
            {
                Debug.Log("volteando a la derecha:    "+transform.localScale.x);
                if(transform.localScale.x < 0){
                    transform.localScale = new Vector2(-1*transform.localScale.x, transform.localScale.y);
                }
                mirandoIzquierda = false;
                mirandoDerecha = true;
            }
    
           // Si se cumple condicion
           /*if( (mirandoDerecha = true && inputMovimiento < 0) || (mirandoDerecha = false && inputMovimiento >  0) )
           {
            // Ejecutar codigo de volteado
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
           }*/
    }
}
