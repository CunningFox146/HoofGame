using HoofGame.Infrastructure;
using System.Collections;
using UnityEngine;

namespace HoofGame.Horse
{
    public class Knife : MonoBehaviour
    {
        [SerializeField] private GameObject _spiralPrefab;
        [SerializeField] private Transform _spiralContainer;
        [SerializeField] private ParticleSystem _fx;
        [SerializeField] private float _scaleMult = 0.5f;

        private Rigidbody _currentSpiral;
        private bool _isCleaning;
        private Vector3 _startPos;

        private Coroutine _fxCoroutine;
        private WaitForSeconds _fxWaitTime = new(1f);

        private void Awake()
        {
            _fx.Pause();
        }

        public void StartCleaning(Vector3 startPos)
        {
            gameObject.SetActive(true);
            _fx.Play();
            _isCleaning = true;
            _startPos = startPos;
            transform.position = _startPos;
            if (_currentSpiral is null)
            {
                _currentSpiral = Instantiate(_spiralPrefab, _spiralContainer).GetComponent<Rigidbody>();
                _currentSpiral.isKinematic = true;
                _currentSpiral.constraints = RigidbodyConstraints.FreezeAll;
            }
            _currentSpiral.transform.localScale = Vector3.one;
        }

        public void StopCleaning(bool dontDropFx = false)
        {
            _fx.Pause();
            _isCleaning = false;
            if (_currentSpiral is not null && !dontDropFx)
            {
                _currentSpiral.transform.SetParent(null);
                _currentSpiral.isKinematic = false;
                _currentSpiral.constraints = RigidbodyConstraints.None;
                _currentSpiral.AddForce(Vector3.up, ForceMode.Impulse);
                _currentSpiral = null;
            }
            gameObject.SetActive(false);
        }

        public void UpdatePos(Vector3 point)
        {
            if (!_isCleaning)
            {
                StartCleaning(point);
            }
            var scale = Vector3.one * (1 + Vector3.Distance(_startPos, transform.position) * _scaleMult);
            transform.position = point;
            _currentSpiral.transform.localScale = scale;
        }
    }
}
