using UnityEngine;

public enum PowerupTypes
{
    Speed, Shield, ScoreBoost
}

public class Powerup : MonoBehaviour
{
    [SerializeField] PowerupTypes powerups;

    public PowerupTypes GetPowerupType()
    {
        return powerups;
    }
}
