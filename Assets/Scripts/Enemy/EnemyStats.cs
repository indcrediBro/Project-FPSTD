using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Health
{
  protected override void Die()
  {
    base.Die();

    EnemyManager.Instance.ActiveEnemies.Remove(gameObject);
    EnemyManager.Instance.UpdateAcviteEnemies();
    ResetHealth();
  }
}
