using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Time
    private float startTime;
    private bool isRunning = true;
    public TMP_Text timeText; 

    public Slider healthBarSlider;

    //Constant Stats
    const float punchDelay = 1f;
    const float timeInvincible = 2.0f;

    //Variable Stats
    float speed = 3.0f;
    float punchTimer = 0.0f;
    bool isInvincible;
    float invincibleTimer;

    //Health Stuff
    int maxHealth = 5;
    int currentHealth;
    public TMP_Text healthText; 

    //Money stuff
    float money = 100.0f;
    public TMP_Text moneyText;  

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    float scaleXNegative;

    public GameObject projectilePrefab;
    public bool hasBow;

    public int health { get { return currentHealth; }}
    
    void Start()
    {
        StartTime();

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        currentHealth = maxHealth;
        scaleXNegative = transform.localScale.x * -1f;
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
    }

    void Update()
    {
        //movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(horizontal, vertical);

        //animator.SetFloat("Look X", lookDirection.x);
        //animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        if (horizontal<0f){
            //Right face scale
            transform.localScale = new Vector2 (-scaleXNegative, transform.localScale.y);
        }else if (horizontal>0f){
            //Left face scale
            transform.localScale = new Vector2 (scaleXNegative, transform.localScale.y);
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        //if click, punch
        if(punchTimer >= punchDelay)
            if (Input.GetMouseButton(0))
            {
                if(hasBow)
                    ShootRifle();
                else
                    Punch(1);
                punchTimer = 0.0f;
            }
        punchTimer = punchTimer + Time.deltaTime;
        //talk to npc
        /*if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }*/

        if (Input.GetKeyDown(KeyCode.E)) {
            openDoor();
        }

        displayMoney();
        UpdateTime();
    }

    void displayMoney() {
        moneyText.text = "$" + money;
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthBarSlider.value = currentHealth;
    }
    
    void Punch(int strength)
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("Enemy"));

            if (hit.collider != null)
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.damage(strength);
                }
            }
        animator.SetTrigger("Punch");
    }

    void openDoor() {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.5f, LayerMask.GetMask("Door"));

            if (hit.collider != null)
            {
                Door1 door = hit.collider.GetComponent<Door1>();
                if (door != null)
                {
                    door.ChangeSceneByName("Pub");
                }
            }
    }
    void ShootRifle() {
        // 1. Detect Mouse Position in World
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // 2. Calculate Direction
        Vector2 shootDirection = (mousePosition - (Vector2)transform.position).normalized;

        // 3. Instantiate and Shoot the Bullet
        GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * 10.0f;
    }
         private void StartTime() {
        startTime = Time.time;
    }

    private void UpdateTime() {
        if (isRunning)
        {
            float timeElapsed = Time.time - startTime;
            string formattedTime = FormatTime(timeElapsed*5760);
            timeText.text = formattedTime;
        }
    }

    private string FormatTime(float time) {
        time *= 96;
        time /= 3600;
        int days = (int)(time / 86400); // 24 * 60 * 60
        int hours = (int)((time % 86400) / 3600);
        int minutes = (int)((time % 3600) / 60);

        //return "" + time;
        return $"{days:D2}:{hours:D2}:{minutes:D2}";
    }

    public void StopTimer() {
        isRunning = false;
    }

    public void StartTimer() {
        isRunning = true;
    }

}
