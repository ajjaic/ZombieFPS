using UnityEngine;

[CreateAssetMenu]
public class PlayerDeathEvent: ScriptableObject
{
    public delegate void PlayerDeathEventType();

    public event PlayerDeathEventType PlayerDeathEventInstance;

    public void RaiseEvent()
    {
        PlayerDeathEventInstance?.Invoke(); 
    }
}