using HoofGame.InputActions;
using UnityEngine;

namespace HoofGame.Infrastructure
{
    public class GameplaySystemsContainer : MonoBehaviour
    {
        public static GameplaySystemsContainer Instance { get; private set; }

        public GameplayInputActions GameplayInputActions { get; private set; }

        private void Awake()
        {
            Instance = this;
            GameplayInputActions = new();
        }
    }
}
