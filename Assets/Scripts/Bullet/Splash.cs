using UnityEngine;

namespace TD
{
    public class Splash : MonoBehaviour
    {

        [SerializeField]
        private int splashDmg;

        private bool boom = false;
        private Vector3 startScale;
        private Vector3 targetScale;
        private float currentTime;
        private float duration = .5f;
        private float scale;

        public float Scale 
        { 
            get 
            { 
                return this.scale; 
            } 
            set 
            { 
                this.scale = value;
                this.targetScale = new Vector3(scale, scale, scale);
            }
        }

        public void Explode()
        {
            this.boom = true;
            this.startScale = new Vector3(0f, 0f, 0f);
            this.transform.localScale = this.startScale;
            this.currentTime = 0f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                Transform target = collision.gameObject.transform;
                target.GetComponent<EnemyUnit>().DamageEnemy(splashDmg);
            }
        }

        private void OnEnable()
        {

        }

        private void Update()
        {
            if (this.scale > 0f && this.boom)
            {
                this.transform.localScale = Vector3.Lerp(this.startScale, this.targetScale, this.currentTime / this.duration);
                this.currentTime += Time.deltaTime;
                if (this.currentTime >= this.duration)
                {
                    this.boom = false;
                    this.transform.localScale = this.startScale;
                }
            }
        }
    }
}
