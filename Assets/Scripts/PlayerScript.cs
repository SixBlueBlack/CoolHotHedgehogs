using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class PlayerScript : MonoBehaviour
{
    private float acceleration = 0.2f;
    private float HorizontalMove;
    private float VerticalMove;
    private Rigidbody2D rigidBodyComponent;
    public Animator animator;
    public static bool IsDead;
    private bool IsRight = true;

    private int lastPose;
    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //положение мыши из экранных в мировые координаты
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);//угол между вектором от объекта к мыше и осью х
        angle = transform.position.y < mousePosition.y ? angle : -angle;
        if (angle > 90 || angle < -90)
        {
            angle = math.sign(angle) * (180 - math.abs(angle));
            if (IsRight)
                Rotate();
        }
        else if (!IsRight)
            Rotate();

        animator.SetFloat("Angle", angle);
        HorizontalMove = Input.GetAxisRaw("Horizontal") * acceleration;
        VerticalMove = Input.GetAxisRaw("Vertical") * acceleration;
        animator.SetBool("IsMoving", HorizontalMove != 0 || VerticalMove != 0);
        //if (HorizontalMove != 0 || VerticalMove != 0)
            //animator.SetBool("IsMoving", true);
        //else
            //animator.SetBool("IsMoving", false);
        //animator.SetFloat("MoveHorizontally", Mathf.Abs(HorizontalMove * acceleration));
        //if (HorizontalMove != 0 && VerticalMove > 0)
        //lastPose = 3;
        //else if (VerticalMove > 0)
        //lastPose = 1;
        //else if (VerticalMove < 0)
        //lastPose = 2;
        //else if (HorizontalMove != 0)
        //lastPose = 0;
        //animator.SetFloat("MoveUp", VerticalMove * acceleration);


        var targetVelocity = new Vector2(HorizontalMove * 10f, VerticalMove * 10f);
        rigidBodyComponent.velocity = targetVelocity;

        //animator.SetInteger("Direction", lastPose);

        //if (HorizontalMove < 0 && IsRight)
        //{
            //Rotate();
        //}
        //else if (HorizontalMove > 0 && !IsRight)
        //{
            //Rotate();
        //}
    }

    private void Rotate()
    {
        var a = transform;
        IsRight = !IsRight;
        var scale = a.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
