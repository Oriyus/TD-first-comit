using UnityEngine;

namespace TD
{
    public class Turret : MonoBehaviour
    {
        [SerializeField]
        private Sockets sockets;

        [SerializeField]
        private Turrets liveTurrets;

        [SerializeField]
        private GameObject splash;

        public GameObject bullet;
        public float bulletSpeed;
        public float splashArea;
        public int bulletDmg = 1;
        public Collider2D clickArea;

        private bool shoot = false;
        private Transform target;
        private GameObject newBullet;

        private int rateOfFireTier = 0;
        private int splashTier = 0;

        public int RateOfFireTier { get {return this.rateOfFireTier; } set {this.rateOfFireTier = value; } }
        public int SplashTier { get { return this.splashTier; } set { this.splashTier = value; } }

        // Check for collision with enemy units to see if it should shoot
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("enemy") && !target)
            {
                target = collision.gameObject.transform;
                newBullet = Instantiate(bullet);
                newBullet.transform.position = this.transform.position;
                target.GetComponent<EnemyUnit>().SubscribeBullet(newBullet);
                shoot = true;
            }
        }

        private void Update()
        {
            if (newBullet && shoot)
            {
                newBullet.transform.position = Vector2.MoveTowards(newBullet.transform.position, target.position, bulletSpeed * Time.deltaTime);
                if (newBullet.transform.position == target.position)
                {
                    // Bullet reached target
                    if (this.splashTier > 0)
                    {
                        GameObject newSplash = Instantiate(this.splash, newBullet.transform.position, Quaternion.identity);
                        newSplash.GetComponent<Splash>().Scale = this.splashArea;
                    }
                    newBullet.transform.position = this.transform.position;
                    target.GetComponent<EnemyUnit>().DamageEnemy(bulletDmg);
                    target.GetComponent<EnemyUnit>().UnsubscribeBullet(newBullet);
                    shoot = false;
                    target = null;
                    Destroy(newBullet);
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
