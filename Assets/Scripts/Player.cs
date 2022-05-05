using UnityEngine;

public class Player : MonoBehaviour
{
    public int HealthPoints { get; set; }
    private float acceleration = 0.2f;
    private float HorizontalMove;
    private float VerticalMove;
    private Rigidbody2D rigidBodyComponent;
    public Animator Animator;
    public static bool IsDead;
    private bool IsRight = true;
    private int lastPose;
    public GameObject ItemPrefab;
    public Sprite Sprite;


    void Start()
    {
        var inst = Instantiate(ItemPrefab, transform.position, Quaternion.identity);
        inst.GetComponent<Item>().itemModel = new ItemModel("Health Potion", "Just heal yourself", Sprite);
        HealthPoints = 100;
        rigidBodyComponent = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HorizontalMove = Input.GetAxisRaw("Horizontal") * acceleration;
        VerticalMove = Input.GetAxisRaw("Vertical") * acceleration;
        Animator.SetFloat("MoveHorizontally", Mathf.Abs(HorizontalMove * acceleration));
        if (HorizontalMove != 0 && VerticalMove > 0)
            lastPose = 3;
        else if (VerticalMove > 0)
            lastPose = 1;
        else if (VerticalMove < 0)
            lastPose = 2;
        else if (HorizontalMove != 0)
            lastPose = 0;
        Animator.SetFloat("MoveUp", VerticalMove * acceleration);


        var targetVelocity = new Vector2(HorizontalMove * 10f, VerticalMove * 10f);
        rigidBodyComponent.velocity = targetVelocity;

        Animator.SetInteger("Direction", lastPose);

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
