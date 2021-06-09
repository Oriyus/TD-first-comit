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