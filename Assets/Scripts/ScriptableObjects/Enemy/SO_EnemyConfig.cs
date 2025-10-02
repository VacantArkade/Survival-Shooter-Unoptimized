using UnityEngine;

[CreateAssetMenu(fileName = "SO_EnemyConfig", menuName = "Scriptable Objects/SO_EnemyConfig")]
public class SO_EnemyConfig : ScriptableObject
{
    public float timeBetweenAttack;
    public int damage;
    public int startHealth;
    public float sinkSpd;
    public int score;
}
