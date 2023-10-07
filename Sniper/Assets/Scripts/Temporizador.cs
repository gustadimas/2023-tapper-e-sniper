using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{
    public Text temporizadorTexto;
    int temporizadorDuracao = 300;
    int temporizador = -1;

    private void Update()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        if (temporizador == -1)
        {
            temporizador = temporizadorDuracao;
            InvokeRepeating("UpdateTimer", 0f, 1f);
        }
    }

    public void ResetTimer()
    {
        temporizador = temporizadorDuracao;
        UpdateTimerText();
    }

    private void UpdateTimer()
    {
        if (temporizador > 0)
        {
            temporizador -= 1;
            UpdateTimerText();
        }
        else if (temporizador == 0)
        {
            Debug.Log("Tempo acabou!");
            CancelInvoke("UpdateTimer");

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void UpdateTimerText()
    {
        int minutes = temporizador / 60;
        int seconds = temporizador % 60;
        temporizadorTexto.text = string.Format("Tempo: " + "{0:00}:{1:00}", minutes, seconds);
    }
}
