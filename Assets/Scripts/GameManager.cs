using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Panels")]
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject gameplayUI;
    
    [Header("Buttons")]
    public GameObject startButton;
    public GameObject gameOverButton;
    public GameObject restartButton;
    
    [Header("UI Elements")]
    public TextMeshProUGUI gasText;
    
    public bool isGameActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ShowStartScreen();
    }
    
    private void ShowStartScreen()
    {
        startPanel.SetActive(true);
        startButton.SetActive(true);
        
        gameOverPanel.SetActive(false);
        gameOverButton.SetActive(false);
        gameplayUI.SetActive(false);
        restartButton.SetActive(false);
        
        isGameActive = false;
    }
    
    public void StartGame()
    {
        isGameActive = true;
        
        gameplayUI.SetActive(true);
        
        startPanel.SetActive(false);
        startButton.SetActive(false);
        
        gameOverPanel.SetActive(false);
        gameOverButton.SetActive(false);
        restartButton.SetActive(false);
        
        PlayerController.Instance.ResetPlayer();
    }
    
    public void GameOver()
    {
        isGameActive = false;
        
        gameOverPanel.SetActive(true);
        gameOverButton.SetActive(true);
        restartButton.SetActive(true);
        
        gameplayUI.SetActive(false);
        
        startPanel.SetActive(false);
        startButton.SetActive(false);
    }
    
    public void RestartGame()
    {
        isGameActive = true;
        
        gameplayUI.SetActive(true);
        gameOverPanel.SetActive(false);
        gameOverButton.SetActive(false);
        restartButton.SetActive(false);
        
        startPanel.SetActive(false);
        startButton.SetActive(false);
        
        PlayerController.Instance.ResetPlayer();
        
        FindObjectOfType<RoadManager>().ResetRoads();
    
        UpdateGasUI(100f);
    }

    public void UpdateGasUI(float gas)
    {
        gasText.text = $"Gas: {gas:F0}";
    }
}