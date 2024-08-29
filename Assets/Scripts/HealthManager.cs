using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public int MaxLives = 3;
    private int CurrentLives;
    public GameObject explodePrefab;
    public GameObject playerShip;
    public GameObject miniShipPrefab;
    public Transform ShipLivesPanel;
    public GameObject GameOverPanel;

    private bool isDestroyed = false;
    private float respawnTimer = 0f;
    public float respawnDelay = 2f;

    private SpriteRenderer shipSpriteRenderer;
    private PlayerMovement shipMovementScript;

    public TextMeshProUGUI LivesCounterText; //for displaying lives in text. but i want it in ships!

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; //adding this fixed the issue of the game being paused upon gameover restart.
        CurrentLives = MaxLives;
        UpdateLivesUI();
        GameOverPanel.SetActive(false); //makes sure the gameover panel is hidden on startup

        shipSpriteRenderer = playerShip.GetComponent<SpriteRenderer>();
        shipMovementScript = playerShip.GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
       if(isDestroyed)
        {
            respawnTimer += Time.deltaTime;

            if(respawnTimer >= respawnDelay)
            {
                RespawnShip();
            }
            return;
        }

        if (healthAmount <= 0)  
        {
            LoseLife();
        }
        //below was to help with health bar implementation. should be removed once healthbar works. 
        if (Input.GetKeyDown(KeyCode.Return)) //enter does 20 damage to ship
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.B)) //B button heals 5 damage
        {
            Heal(5);
        }
        
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;
    }

    void LoseLife()
    {
        CurrentLives--;
        UpdateLivesUI();

        if(CurrentLives <= 0)
        {
            TriggerExplosionAndDelay();
            GameOver();
        }
        else
        {
            TriggerExplosionAndDelay();
        }
    }
    void TriggerExplosionAndDelay()
    {
        Vector3 deathPosition = playerShip.transform.position;

        Instantiate(explodePrefab, playerShip.transform.position, Quaternion.identity);
        shipSpriteRenderer.enabled = false;
        shipMovementScript.enabled = false;

        // stop the ship's movement by resetting its velocity
        Rigidbody2D shipRb = playerShip.GetComponent<Rigidbody2D>();
        shipRb.velocity = Vector2.zero;

        isDestroyed = true;
        respawnTimer = 0f;

        playerShip.transform.position = deathPosition;
        

    }
    
    void RespawnShip()
    { 
        healthAmount = 100f;
        healthBar.fillAmount = healthAmount / 100f;

        shipSpriteRenderer.enabled = true;
        shipMovementScript.enabled = true;

        isDestroyed = false;
    }
    

    void UpdateLivesUI()
    {
        foreach (Transform child in ShipLivesPanel)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < CurrentLives; i++)
        {
            GameObject miniShip = Instantiate(miniShipPrefab, ShipLivesPanel);
            RectTransform shipRect = miniShip.GetComponent<RectTransform>();
            shipRect.anchoredPosition = new Vector2(i * 100f, 0);

        }
        LivesCounterText.text = "Lives: "; 
    }

    void GameOver()
    {
        StartCoroutine(GameOverDelay());
    }

    IEnumerator GameOverDelay()
    {
        // wait for a short delay (e.g., 1 second) before showing the game over screen
        //this lets me show the ship exploding before game over
        yield return new WaitForSecondsRealtime(1f);

       
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
