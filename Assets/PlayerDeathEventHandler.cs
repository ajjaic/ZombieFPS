using UnityEngine;

public class PlayerDeathEventHandler : MonoBehaviour
{
    [SerializeField] private PlayerDeathEvent playerDeathEvent;
    [SerializeField] private Canvas gameOverScreen;
    
    // messages
    private void OnEnable()
    {
        playerDeathEvent.PlayerDeathEventInstance += OnPlayerDeath;
    }

    private void OnDisable()
    {
        playerDeathEvent.PlayerDeathEventInstance -= OnPlayerDeath;
    }
    
    private void OnPlayerDeath()
    {
        gameOverScreen.gameObject.SetActive(true);
    }
}
