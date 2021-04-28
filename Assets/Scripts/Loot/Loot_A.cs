namespace TD
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Loot_A : MonoBehaviour
    {
        public int resources;
        private bool move = false;
        private Vector3 offset;
        private Vector3 target;
        private float speed = 1f;

        private void Update()
        {
            if (this.move)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, this.target, this.speed * Time.deltaTime);
                if (this.transform.position == this.target)
                {
                    this.move = false;
                }
            }
        }

        private void Start()
        {
            this.resources = Random.Range(1, 10);
            this.move = true;
            this.offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
            this.target = this.transform.position + this.offset;
        }
    }
}
