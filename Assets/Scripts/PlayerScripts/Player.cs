using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; } = 100;
    public HealthBar HealthBar;
    private float acceleration = 0.2f;
    private float speed = 2.5f;
    private float horizontalMove;
    private float verticalMove;
    private Rigidbody2D rigidBodyComponent;
    public Animator Animator;
    public static bool IsDead;
    private bool isRight = true;
    private int lastPose;
    public GameObject ItemPrefab;
    public Sprite Sprite;
    public Canvas HealthBarCanvas;

    private AudioSource DamageAudio { get; set; }
    private AudioSource DeathAudio { get; set; }


    void Start()
    {
        var inst = Instantiate(ItemPrefab, transform.position, Quaternion.identity);
        inst.GetComponent<Item>().itemModel = new ItemModel("Health Potion", "Just heal yourself", Sprite);
        
        rigidBodyComponent = GetComponent<Rigidbody2D>();

        var audios = GetComponents<AudioSource>();
        DamageAudio = audios[0];
        DeathAudio = audios[1];

        CurrentHealth = MaxHealth;
        HealthBar.SetMaxHealth(MaxHealth);
    }

    void Update()
    {
        Move();
        SetLastPose();
    }

    private void Move()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * acceleration;
        verticalMove = Input.GetAxisRaw("Vertical") * acceleration;
        Animator.SetFloat("MoveHorizontally", Mathf.Abs(horizontalMove * acceleration));

        var targetVelocity = new Vector2(horizontalMove, verticalMove).normalized;
        rigidBodyComponent.velocity = targetVelocity * speed;

        if (horizontalMove < 0 && isRight || horizontalMove > 0 && !isRight)
            Rotate();
    }

    private void SetLastPose()
    {
        if (horizontalMove != 0 && verticalMove > 0)
            lastPose = 3;
        else if (verticalMove > 0)
            lastPose = 1;
        else if (verticalMove < 0)
            lastPose = 2;
        else if (horizontalMove != 0)
            lastPose = 0;
        Animator.SetFloat("MoveUp", verticalMove * acceleration);
        Animator.SetInteger("Direction", lastPose);
    }

    private void Rotate()
    {
        var a = transform;
        isRight = !isRight;
        var scale = a.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        var canvasScale = HealthBarCanvas.transform.localScale;
        canvasScale.x *= -1;
        HealthBarCanvas.transform.localScale = canvasScale;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        HealthBar.SetHealth(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            IsDead = true;
            DamageAudio.Stop();
            DeathAudio.Play();
        }

        else if (!DamageAudio.isPlaying)
            DamageAudio.Play();
    }

    public void Heal(int value)
    {
        TakeDamage(Math.Max(-value, -(MaxHealth - CurrentHealth)));
    }
}
