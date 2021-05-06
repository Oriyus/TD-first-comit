namespace TD
{
    using System;
    using UnityEngine;
    using System.Collections.Generic;

    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField]
        private LevelData level;

        [SerializeField]
        private Enemies enemies;

        [SerializeField]
        private Loot allLoot;

        [SerializeField]
        private Enemy enemy;

        private GameObject loot;

        //public static Action<GameObject, Transform> OnLootDropedEvent;
        public GameObjectEvent OnLootDropedEvent = null;

        private List<Vector2> path;
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
                // Drop loot
                GameObject newLoot = Instantiate(this.loot, this.transform.position, Quaternion.identity);
                allLoot.Add(newLoot);
                OnLootDropedEvent.Raise(newLoot);
                // Destroy enemy
                Destroy(gameObject);
            }
        }

        // Moving enemy unit on given path
        private void UnitPathFollow()
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, this.path[this.index], this.speed * Time.deltaTime);
            Vector3 targetPoint = this.path[index];
            Vector3 lastPoint = this.path[this.path.Count - 1];
            if ((this.index < this.path.Count - 1) && (this.transform.position == targetPoint))
            {
                this.index++;
                // Reached index path point
            }
            else if(this.transform.position == lastPoint)
            {
                // Reached End of Path
                this.move = false;
                Destroy(gameObject);
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
            this.loot = enemy.loot;
            this.path = level.pathPoints;
            this.transform.position = this.path[0];
        }

        private void OnEnable()
        {
            this.enemies.Add(this.gameObject);
        }

        private void OnDisable()
        {
            this.enemies.Remove(this.gameObject);
        }
    }
}
