using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(0);
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene(5);
    }
}
