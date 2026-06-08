using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Transform healthPanel;
    public GameObject healthIconPrefab;

    public AudioSource audioSource;
    public AudioClip enemyExplosionSound;
    public AudioClip playerExplosionSound;

    private int score = 0;
    private int currentHealth;
    private List<GameObject> healthIcons = new List<GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    public void InitializeHealth(int maxHealth)
    {
        currentHealth = maxHealth;

        foreach (Transform child in healthPanel) Destroy(child.gameObject);
        healthIcons.Clear();

        for (int i = 0; i < maxHealth; i++)
        {
            GameObject icon = Instantiate(healthIconPrefab, healthPanel, false);
            icon.transform.localScale = Vector3.one;
            healthIcons.Add(icon);
        }
    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            
            if (healthIcons.Count > 0)
            {
                GameObject iconToRemove = healthIcons[healthIcons.Count - 1];
                healthIcons.Remove(iconToRemove);
                Destroy(iconToRemove);
            }

            if (currentHealth <= 0)
            {
                GameOver();
            }
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void PlayEnemyExplosion()
    {
        if (audioSource && enemyExplosionSound)
            audioSource.PlayOneShot(enemyExplosionSound);
    }

    public void GameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }

        if (audioSource && playerExplosionSound)
            audioSource.PlayOneShot(playerExplosionSound);

        Debug.Log("Game Over! Restarting...");
        // Requirement 3: Restart the game by reloading the active scene
        Invoke("RestartScene", 1.5f); // 1.5 second delay to let the sound play
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
