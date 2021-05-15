using UnityEngine;

namespace TD
{
    public class Turret : MonoBehaviour
    {
        [SerializeField]
        private Sockets sockets;

        [SerializeField]
        private Turrets liveTurrets;

        public GameObject bullet;
        public float bulletSpeed;
        public int bulletDmg = 1;
        public Collider2D clickArea;

        private bool shoot = false;
        private Transform target;
        private GameObject newBullet;

        public int rateOfFireTier = 0;

        public int RateOfFireTier { get {return this.rateOfFireTier; } set {this.rateOfFireTier = value; } }

        // Check for collision with enemy units to see if it should shoot
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("enemy") && !target)
            {
                target = collision.gameObject.transform;
                newBullet = Instantiate(bullet);
                newBullet.transform.position = this.transform.position;
                shoot = true;
            }
        }

        // Check if collision stopped to stop shooting
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision && target)
            {
                if (collision.gameObject == target.gameObject)
                {
                    shoot = false;
                    target = null;
                    Destroy(newBullet);
                }
            }
        }

        private void Update()
        {
            if (shoot)
            {
                newBullet.transform.position = Vector2.MoveTowards(newBullet.transform.position, target.position, bulletSpeed * Time.deltaTime);
                if (newBullet.transform.position == target.position)
                {
                    newBullet.transform.position = this.transform.position;
                    target.GetComponent<EnemyUnit>().DamageEnemy(bulletDmg);
                }
            }
        }

        private void OnEnable()
        {
            this.liveTurrets.Add(this.gameObject);
        }

        private void OnDisable()
        {
            this.liveTurrets.Remove(this.gameObject);
        }
    }
}
