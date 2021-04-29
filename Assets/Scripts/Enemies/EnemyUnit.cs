namespace TD
{
    using System;
    using UnityEngine;
    using System.Collections.Generic;

    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField]
        private Enemy enemy;

        [SerializeField]
        private GameObject loot;

        public static Action<GameObject, Transform> OnLootDropedEvent;

        private List<Transform> path;
        private SpriteRenderer graphics;
        public float speed;
        private int index = 0;
        private bool move = true;
        private int health;

        // Do damage to enemy unit
        public void DamageEnemy(int dmg)
        {
            this.health -= dmg;
            if (this.health <= 0)
            {
                this.CheckWhichEnemyToRemove();
                // Drop loot
                OnLootDropedEvent?.Invoke(this.loot, this.transform);
                // Destroy enemy
                Destroy(gameObject);
            }
        }

        // Moving enemy unit on given path
        private void UnitPathFollow()
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, this.path[this.index].position, this.speed * Time.deltaTime);
            if ((this.index < this.path.Count - 1) && (this.transform.position == this.path[this.index].position))
            {
                this.index++;
                // Reached index path point
            }
            else if(this.transform.position == this.path[this.path.Count -1].position)
            {
                // Reached End of Path
                this.move = false;
                this.CheckWhichEnemyToRemove();
                Destroy(gameObject);
            }
        }

        private void CheckWhichEnemyToRemove()
        {
            for (int i = 0; i <= EnemyManager.Instance.LiveEnemies.Count - 1; i++)
            {
                if (EnemyManager.Instance.LiveEnemies[i] == this.gameObject)
                {
                    EnemyManager.Instance.RemoveLiveEnemy(i);
                }
            }
        }

        private void Update()
        {
            if (move)
            {
                this.UnitPathFollow();
            }
        }

        private void Start()
        {
            this.health = enemy.health;
            this.speed = enemy.speed;
            this.path = PathManager.Instance.Path;
            this.transform.position = this.path[0].position;
        }
    }
}
