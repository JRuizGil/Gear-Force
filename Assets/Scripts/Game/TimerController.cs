using UnityEngine;
using UnityEngine.Events;
using TMPro; // Solo si quieres mostrar el tiempo en pantalla

public class TimerController : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 60f; // Tiempo inicial del contador
    private float currentTime;
    private bool isRunning = false;

    [Header("UI (Opcional)")]
    public TMP_Text timerText; // Arrástralo desde el canvas si quieres mostrarlo

    [Header("Events")]
    public UnityEvent OnTimerEnd; // Se ejecuta cuando el tiempo llega a 0

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            isRunning = false;

            OnTimerEnd?.Invoke(); // Llamar evento de fin
        }

        UpdateTimerUI();
    }

    // -------------------- PUBLIC FUNCTIONS --------------------

    public void StartTimer()
    {
        isRunning = true;
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        currentTime = startTime;
        isRunning = false;
        UpdateTimerUI();
    }

    public void RestartTimer()
    {
        currentTime = startTime;
        isRunning = true;
        UpdateTimerUI();
    }

    public bool IsTimerFinished()
    {
        return currentTime <= 0;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    // -------------------- INTERNAL --------------------

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = $"Time Left: {Mathf.Ceil(currentTime)}";
        }
    }
}