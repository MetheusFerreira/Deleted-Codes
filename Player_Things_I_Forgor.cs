using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maria : MonoBehaviour
{
    #region Variaveis
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpPower = 500;
    [SerializeField] bool rastejow = false;

    private const float detectionRadius = 0.2F;
    const float groundCheckRadius = 0.2f;
    public float velocidade;
    public Transform detectionPoint;
    
    public LayerMask detectionLayer;

    float horizontalValor;
    bool olhandoD;
    bool jump;
    //por default, bools sempre serão falsas, então é desnecessário especificar.
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalValor = Input.GetAxisRaw("Horizontal");
        // if(DetectObject())
        // {
        //     if(InterageInput())
        //     {
        //         Debug.Log("INTERACT");
        //     } 
        // }
        // desnecessário
        
        
        if(Input.GetButtonDown("Jump")){
            jump = true;
        }else if(Input.GetButtonUp("Jump")){
            jump = false;
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValor, jump);
    }

    void GroundCheck()
    {
        rastejow = false;
        
        //checar se terá sombra no chão ou nao.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if(colliders.Length>0)
        {
            rastejow = true;
        }
    }

    void Move(float dir, bool jumpFlag) 
    {
        #region Mover
        float xVal = dir * velocidade * 100 * Time.fixedDeltaTime;
        Vector2 targetVelocity = new Vector2(xVal,rb.velocity.y);
        rb.velocity = targetVelocity;

        if(olhandoD && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            olhandoD = false;
        }
        else if(!olhandoD && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            olhandoD = true;
        }

        animator.SetFloat("velocidade", Mathf.Abs(rb.velocity.x));
        #endregion
        
        if(rastejow && jumpFlag){
            rastejow = false;
            jumpFlag = false;
            rb.AddForce(new Vector2(0f, jumpPower));
        }
    }

    bool InterageInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
    // bool DetectObject()
    // {
    //     return Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
    // }
    // private void OnDrawGizmosSelected() 
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    // }
    // desnecessário
}
