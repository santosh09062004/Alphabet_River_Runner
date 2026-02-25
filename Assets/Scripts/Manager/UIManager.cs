using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public AlphabetManager alphabetManager;
    public CoinManager coinManager;

    [Header("UI Elements")]
    public TextMeshProUGUI targetLetterText;
    public TextMeshProUGUI coinText;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    private bool stateLocked = false;

    void Start()
    {
        // Ensure panels are hidden at start
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    void Update()
    {
        if (alphabetManager == null || coinManager == null)
            return;

        // Update Target Letter
        targetLetterText.text = "Target: " + alphabetManager.GetCurrentTarget();

        // Update Coin Count
        coinText.text = "Coins: " + coinManager.GetCoinCount();

        // Handle state changes
        if (!stateLocked)
        {
            if (alphabetManager.IsGameOver())
            {
                ShowPanel(gameOverPanel);
            }

            if (alphabetManager.IsGameWon())
            {
                ShowPanel(winPanel);
            }
        }
    }

    void ShowPanel(GameObject panel)
    {
        stateLocked = true;
        StartCoroutine(FadeIn(panel));
    }

    IEnumerator FadeIn(GameObject panel)
    {
        panel.SetActive(true);

        CanvasGroup group = panel.GetComponent<CanvasGroup>();

        group.alpha = 0f;
        group.interactable = true;
        group.blocksRaycasts = true;

        float duration = 0.4f;
        float time = 0f;

        while (time < duration)
        {
            group.alpha = Mathf.Lerp(0f, 1f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        group.alpha = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}