using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotProperties : MonoBehaviour
{
    public float damage;
    public bool piercing;

    public float ReportDamage()
    {
        return damage;
    }
}
