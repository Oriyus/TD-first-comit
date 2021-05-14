using System;
using UnityEngine;

namespace TD
{
    public class Collector : MonoBehaviour
    {
        [SerializeField]
        private Loot allLoot;

        [SerializeField]
        private float speed = 5f;
        [SerializeField]
        private float rotSpeed = 5f;

        public GameObjectEvent OnLootCollected = null;

        private int targetIndex = -1;

        public void OnNewLootOnMap(GameObject obj)
        {
            this.ClosestLoot();
        }

        private void ClosestLoot()
        {
            // Calculate closest loot
            targetIndex = 0;
            for (int i = 0; i < allLoot.Items.Count; i++)
            {
                if ((allLoot.Items[i].transform.position - this.transform.position).magnitude <
                    (allLoot.Items[targetIndex].transform.position - this.transform.position).magnitude)
                {
                    targetIndex = i;
                }
            }
        }

        private void MoveAndRotateCollector()
        {
            // Move to position
            this.transform.position = Vector2.MoveTowards(this.transform.position, allLoot.Items[targetIndex].transform.position, this.speed * Time.deltaTime);

            // Rotate towards target
            var dir = allLoot.Items[targetIndex].transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * this.rotSpeed);
        }

        private void IsLootReached()
        {
            //Check to see if loot reached
            if (this.transform.position == this.allLoot.Items[targetIndex].transform.position)
            {
                // Reached index loot
                GameObject obj = this.allLoot.Items[this.targetIndex];
                this.allLoot.Items.Remove(obj);
                this.OnLootCollected.Raise(obj);
                Destroy(obj);
                if (allLoot.Items.Count == 0)
                {
                    this.targetIndex = -1;
                }
                else
                {
                    this.ClosestLoot();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (this.allLoot.Items.Count > 0 && this.targetIndex != -1)
            {
                this.MoveAndRotateCollector();
                this.IsLootReached();
            }
        }
    }
}
