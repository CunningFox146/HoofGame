using System;
using UnityEngine;

namespace HoofGame.Horse
{
    public class HorseAnimation : MonoBehaviour
    {
        private static readonly int StandUpHash = Animator.StringToHash("StandUp");
        private static readonly int WinHash = Animator.StringToHash("Win");
        private static readonly int LooseHash = Animator.StringToHash("Loose");

        [SerializeField] private Animator _animator;

        internal void StandUp()
        {
            _animator.SetTrigger(StandUpHash);
        }
    }
}
