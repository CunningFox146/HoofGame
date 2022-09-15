using UnityEngine;

namespace HoofGame.Horse
{
    public class Horse : MonoBehaviour
    {
        private static readonly int StandUpHash = Animator.StringToHash("StandUp");
        private static readonly int WinHash = Animator.StringToHash("Win");
        private static readonly int LooseHash = Animator.StringToHash("Loose");

        [SerializeField] private Animator _animator;

        private void Start()
        {
            _animator.SetTrigger(StandUpHash);
        }
    }
}
