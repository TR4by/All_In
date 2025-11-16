using System.Collections;
using UnityEngine;

public class StoryScreensManager : MonoBehaviour
{
    [SerializeField] GameObject IntroScreen;
    [SerializeField] GameObject WinScreen;
    [SerializeField] GameObject DeathScreen;

    void Awake()
    {
        IntroScreen.SetActive(true);
        WinScreen.SetActive(false);
        DeathScreen.SetActive(false);
    }

    public void ShowWinEnding()
    {
        GameManager.Instance.BlockInteraction(false);
        if (DeathScreen.activeInHierarchy) return;
        WinScreen.SetActive(true);
    }

    public void ShowDeathEnding()
    {
        GameManager.Instance.BlockInteraction(false);
        if (WinScreen.activeInHierarchy) return;
        DeathScreen.SetActive(true);
    }

    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartDeathEndingSequence()
    {
        StartCoroutine(DeathEndingSequence());
    }

    public void StartWinEndingSequence()
    {
        StartCoroutine(WinEndingSequence());
    }

    private IEnumerator DeathEndingSequence()
    {
        GameManager.Instance.BlockInteraction(true);
        yield return new WaitForSeconds(0.5f);
        ShowDeathEnding();
    }

    private IEnumerator WinEndingSequence()
    {
        GameManager.Instance.BlockInteraction(true);
        yield return new WaitForSeconds(2f);
        ShowWinEnding();
    }
}
