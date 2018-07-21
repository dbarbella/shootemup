using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotProperties : MonoBehaviour
{
    public float damage;
    public bool piercing;
    // This is whatever visual the shot makes when it disappears
    public GameObject poof;

    public float ReportDamage()
    {
        return damage;
    }
}
