namespace TD
{
    using UnityEngine;

    public class LoadLevel : MonoBehaviour
    {
        [SerializeField]
        private Levels levels;

        [SerializeField]
        private CurrentLevel currentLevel;

        public LevelEvent levelEvent = null;

        private void Awake()
        {
            levelEvent.Raise(this.levels.Items[this.currentLevel.currentLevel]);
        }
    }
}
