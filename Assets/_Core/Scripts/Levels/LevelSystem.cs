using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HoofGame.Levels
{
    public class LevelSystem : MonoBehaviour
    {
        public static event Action LevelLoaded;
        public static LevelSystem Instance { get; private set; }

        private List<Level> _levels;

        public Level CurrentLevel { get; private set; }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLevels();
        }

        private void Start()
        {
            LoadCurrentLevel();
        }

        private void LoadCurrentLevel()
        {
            int savedLevel = PlayerPrefs.GetInt(nameof(CurrentLevel));
            if (savedLevel > _levels.Count)
            {
                savedLevel = Random.Range(0, _levels.Count - 1);
            }
            Debug.Log($"Loading level {savedLevel}");
            CurrentLevel = _levels[savedLevel];
            LevelLoaded?.Invoke();
        }

        public void LevelPassed()
        {
            PlayerPrefs.SetInt(nameof(CurrentLevel), CurrentLevel.LevelNum + 1);
        }

        private void LoadLevels()
        {
            var levels = Resources.LoadAll<Level>("Levels/");
            _levels = new(levels);
            _levels.Sort((a, b) => a.LevelNum.CompareTo(b.LevelNum));
        }
    }
}
