using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDamagable : MonoBehaviour
{
    public interface IDamageable
    {
        void TakeDamage(int amount);
    }

}
