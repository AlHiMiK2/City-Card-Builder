using UnityEngine;

namespace _Project.Scripts.Handlers.GameStateMachine.States
{
    public class PlayerTurnState : State
    {
        [SerializeField] private int _targetPlayerIndex;

        private GameHandler _gameHandler;

        private void OnEnable()
        {
            if (!_gameHandler)
            {
                _gameHandler = GameHandler.Instance;
            }
            
            _gameHandler.Players[_targetPlayerIndex].EnableTurn();
        }
    }
}