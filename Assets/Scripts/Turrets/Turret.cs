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

        private List<Transform> targets;
        private List<GameObject> newBullet;
        private List<GameObject> newSplash;

        private bool targetAquired = false;

        private int rateOfFireTier = 0;
        private int splashTier = 0;

        public int RateOfFireTier { get { return this.rateOfFireTier; } set { this.rateOfFireTier = value; } }
        public int SplashTier { get { return this.splashTier; } set { this.splashTier = value; } }

        private void ResetAllBulletsPosition()
        {
            for (int i = 0; i <= activeBarrels; i++)
            {
                newBullet[i].transform.position = this.barrels[i].position;
            }
        }

        // Check for collision with enemy units
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                targets.Add(collision.gameObject.transform);
            }
        }

        // Check for enemy/bullet exiting shooting range
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                targets.Remove(collision.gameObject.transform);
                DisableAllBullets();
                ResetAllBulletsPosition();
            }
            if (collision.gameObject.CompareTag("bullet"))
            {
                collision.gameObject.SetActive(false);
            }
        }

        // Deactivate all bullet objects
        private void DisableAllBullets()
        {
            foreach (var item in newBullet)
            {
                item.SetActive(false);
            }
        }

        // Shoot from active barrels
        private void Shoot(int barrelIndex)
        {
            newBullet[barrelIndex].SetActive(true);
            newBullet[barrelIndex].transform.position = Vector2.MoveTowards(newBullet[barrelIndex].transform.position, targets[0].position, bulletSpeed * Time.deltaTime);

            // Bullet reached target
            if (newBullet[barrelIndex].transform.position == targets[0].position)
            {
                // Should bullet make splash damage
                if (this.splashTier > 0)
                {
                    newSplash[barrelIndex].transform.position = newBullet[barrelIndex].transform.position;
                    Splash spl = newSplash[barrelIndex].GetComponent<Splash>();
                    spl.Scale = this.splashArea;
                    spl.Explode();
                }

                newBullet[barrelIndex].transform.position = this.barrels[barrelIndex].position;
                newBullet[barrelIndex].SetActive(false);
                targets[0].GetComponent<EnemyUnit>().DamageEnemy(bulletDmg);
            }
        }

        private void Update()
        {
            if (targets.Count > 0)
            {
                if (!targetAquired)
                {
                    targetAquired = true;
                    ResetAllBulletsPosition();
                }

                for (int i = 0; i <= this.activeBarrels; i++)
                {
                    this.Shoot(i);
                }
            }
        }

        private void Start()
        {
            this.newBullet = new List<GameObject>();
            this.newSplash = new List<GameObject>();
            this.targets = new List<Transform>();

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