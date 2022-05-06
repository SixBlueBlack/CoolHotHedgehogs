using UnityEngine;

public class Player : MonoBehaviour
{
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; } = 100;
    public HealthBar HealthBar;
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
    public Canvas HealthBarCanvas;


    void Start()
    {
        var inst = Instantiate(ItemPrefab, transform.position, Quaternion.identity);
        inst.GetComponent<Item>().itemModel = new ItemModel("Health Potion", "Just heal yourself", Sprite);
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;
        HealthBar.SetMaxHealth(MaxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(10);
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
        var canvasScale = HealthBarCanvas.transform.localScale;
        canvasScale.x *= -1;
        HealthBarCanvas.transform.localScale = canvasScale;
    }

    void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        HealthBar.SetHealth(CurrentHealth);
        if (CurrentHealth <= 0)
            IsDead = true;
    }
}
