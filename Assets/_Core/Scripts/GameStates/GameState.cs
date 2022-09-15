using System;
using UnityEngine;

namespace HoofGame.GameStates
{
    public static class GameState
    {
        public static event Action<IGameState> StateChanged;

        private static IGameState _gameState;
        public static IGameState CurrentState
        {
            get => _gameState;
            set
            {
                _gameState?.OnExit();
                _gameState = value;
                _gameState?.OnEnter();
                StateChanged?.Invoke(_gameState);
            }
        }
    }
}
