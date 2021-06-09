using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD
{
    public class Barrel : MonoBehaviour
    {
        [SerializeField]
        private GameObject splash;

        [SerializeField]
        private Transform barrelTip;

        [SerializeField]
        public GameObject bullet;

        public float bulletSpeed;
        public float splashArea;
        public int bulletDmg = 1;
        public int activeBarrels = 1;

        private List<Transform> targets;
        private GameObject newBullet;
        private GameObject newSplash;

        private bool targetAquired = false;

        private int rateOfFireTier = 0;
        private int splashTier = 0;

        public int RateOfFireTier { get { return this.rateOfFireTier; } set { this.rateOfFireTier = value; } }
        public int SplashTier { get { return this.splashTier; } set { this.splashTier = value; } }

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
            if (collision.gameObject.CompareTag("bullet"))
            {
                collision.gameObject.SetActive(false);
                newBullet.transform.position = this.barrelTip.position;
            }
        }

        // Shoot from active barrel
        private void Shoot()
        {
            newBullet.SetActive(true);
            newBullet.transform.position = Vector2.MoveTowards(newBullet.transform.position, targets[0].position, bulletSpeed * Time.deltaTime);

            // Bullet reached target
            if (newBullet.transform.position == targets[0].position)
            {
                // Should bullet make splash damage
                if (this.splashTier > 0)
                {
                    newSplash.transform.position = newBullet.transform.position;
                    Splash spl = newSplash.GetComponent<Splash>();
                    spl.Scale = this.splashArea;
                    spl.Explode();
                }

                newBullet.transform.position = this.barrelTip.position;
                newBullet.SetActive(false);
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
                    newBullet.transform.position = this.barrelTip.position;
                }

                this.Shoot();
            }
        }

        private void Start()
        {
            this.newBullet = Instantiate(this.bullet);
            this.newBullet.SetActive(false);

            this.newSplash = Instantiate(this.splash, new Vector3(1000f, 1000f, 0f), Quaternion.identity);
            this.targets = new List<Transform>();
        }
    }
}
