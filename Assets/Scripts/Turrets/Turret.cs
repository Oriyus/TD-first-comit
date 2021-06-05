using System.Collections.Generic;
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

        [SerializeField]
        private Transform[] barrels;

        public GameObject bullet;
        public float bulletSpeed;
        public float splashArea;
        public int bulletDmg = 1;
        public Collider2D clickArea;
        public int activeBarrels = 1;

        private Transform target;

        private List<GameObject> newBullet;
        private List<GameObject> newSplash;

        private int rateOfFireTier = 0;
        private int splashTier = 0;

        public int RateOfFireTier { get {return this.rateOfFireTier; } set {this.rateOfFireTier = value; } }
        public int SplashTier { get { return this.splashTier; } set { this.splashTier = value; } }

        // Check for collision with enemy units to see if turret should shoot
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("enemy") && !target)
            {
                target = collision.gameObject.transform;
                for (int i = 0; i <= activeBarrels; i++)
                {
                    newBullet[i].transform.position = this.barrels[i].position;
                }
            }
        }

        private void Shoot(int index)
        {
            if (target)
            {
                newBullet[index].SetActive(true);
                newBullet[index].transform.position = Vector2.MoveTowards(newBullet[index].transform.position, target.position, bulletSpeed * Time.deltaTime);
                
                // Bullet reached target
                if (newBullet[index].transform.position == target.position)
                {
                    // Should bullet make splash damage
                    if (this.splashTier > 0)
                    {
                        newSplash[index].transform.position = newBullet[index].transform.position;
                        Splash spl = newSplash[index].GetComponent<Splash>();
                        spl.Scale = this.splashArea;
                        spl.Explode();
                    }
                    newBullet[index].transform.position = this.barrels[index].position;
                    newBullet[index].SetActive(false);
                    target.GetComponent<EnemyUnit>().DamageEnemy(bulletDmg);
                }
            }
        }

        private void Update()
        {
            for (int i = 0; i <= this.activeBarrels; i++)
            {
                this.Shoot(i);
            }
        }

        private void Start()
        {
            this.newBullet = new List<GameObject>();
            this.newSplash = new List<GameObject>();

            for (int i = 0; i <= this.activeBarrels; i++)
            {
                GameObject bul = Instantiate(this.bullet);
                bul.SetActive(false);
                this.newBullet.Add(bul);
                this.newSplash.Add(Instantiate(this.splash, new Vector3(1000f, 1000f, 0f), Quaternion.identity));
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
