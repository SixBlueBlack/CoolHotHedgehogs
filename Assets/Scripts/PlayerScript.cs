using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        HorizontalMove = Input.GetAxisRaw("Horizontal") * acceleration;
        VerticalMove = Input.GetAxisRaw("Vertical") * acceleration;
        animator.SetFloat("MoveHorizontally", Mathf.Abs(HorizontalMove * acceleration));
        if (HorizontalMove != 0 && VerticalMove > 0)
            lastPose = 3;
        else if (VerticalMove > 0)
            lastPose = 1;
        else if (VerticalMove < 0)
            lastPose = 2;
        else if (HorizontalMove != 0)
            lastPose = 0;
        animator.SetFloat("MoveUp", VerticalMove * acceleration);


        var targetVelocity = new Vector2(HorizontalMove * 10f, VerticalMove * 10f);
        rigidBodyComponent.velocity = targetVelocity;

        animator.SetInteger("Direction", lastPose);

        if (HorizontalMove < 0 && IsRight)
        {
            Rotate();
        }
        else if (HorizontalMove > 0 && !IsRight)
        {
            Rotate();
        }
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
