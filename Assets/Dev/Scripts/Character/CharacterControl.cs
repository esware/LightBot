using System;
using System.Collections;
using Dev.Scripts.Tiles;
using DG.Tweening;
using UnityEngine;

namespace Dev.Scripts.Character
{
    public enum AnimationTransition
    {
        Idle,Walk,Jump,Push,Finish
    }
    public enum Direction { FORWARD, RIGHT, BACKWARD, LEFT };
    
    public class CharacterControl : MonoBehaviour
    {

        [Header("Elements")] 
        [SerializeField] private float moveSpeed;
        private Animator _animator;
        public Position currentPosition;
        
        [SerializeField]
        internal Direction currentDirection = Direction.FORWARD;

        #region Initial Methods
        private void Start()
        {
            Init();
        }

        #region Events Control

        private void OnEnable()
        {
            SignUpEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
        
        private void SignUpEvents()
        {
            GameEvents.OnLevelCompleted += () => PlayAnim(AnimationTransition.Finish, 0.1f);
        }
        private void UnsubscribeEvents()
        {
            GameEvents.OnLevelCompleted -= ()=>PlayAnim(AnimationTransition.Finish, 0.1f);
        }

        #endregion
        

        private void Init()
        {
            _animator = GetComponentInChildren<Animator>();
            currentPosition = new Position(0,0);
            transform.position = Vector3.zero;
        }
        

        #endregion

        #region State Methods
        

        public void PlayAnim(AnimationTransition transition,float animTransSpeed)
        {
            if(_animator == null || _animator.IsInTransition(0)) {return;}
            _animator.CrossFade(transition.ToString(), animTransSpeed);
        }
        

        #endregion

        #region Movement Methods

        internal IEnumerator Walk(Position nextPosition, Vector3 platformWorldPosition)
        {
            PlayAnim(AnimationTransition.Walk,0.1f);
            Tweener tweener = transform.DOMove(platformWorldPosition, 1.3f)
                .SetUpdate(UpdateType.Fixed) 
                .OnUpdate(() => {
                    Vector3 newPosition = Vector3.Lerp(transform.position, platformWorldPosition, Time.fixedDeltaTime / 1.3f);
                    transform.position = newPosition;
                })
                .OnComplete(() => {
                    transform.position = platformWorldPosition;
                    currentPosition = nextPosition;
                });
            
            yield return tweener.WaitForCompletion();
        }
        internal IEnumerator TurnRight()
        {
            PlayAnim(AnimationTransition.Idle,0.01f);
            Vector3 targetAngles = transform.eulerAngles + new Vector3(0, 90, 0);

            Tweener tweener = transform.DORotate(targetAngles, 1f)
                .SetUpdate(UpdateType.Fixed) 
                .OnUpdate(() => {
                    Vector3 newAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, Time.fixedDeltaTime / 1f);
                    transform.rotation = Quaternion.Euler(newAngles);
                })
                .OnComplete(() => {
                    transform.eulerAngles = targetAngles;
                    currentDirection = (Direction)((int)(currentDirection + 1) % 4);
                });
            yield return tweener.WaitForCompletion();
        }
        internal IEnumerator TurnLeft()
        {
            PlayAnim(AnimationTransition.Idle,0.01f);
            Vector3 targetAngles = transform.eulerAngles - new Vector3(0, 90, 0);
            if (targetAngles.y < 0)
                targetAngles += Vector3.up * 360;

            Tweener tweener = transform.DORotate(targetAngles, 1f)
                .SetUpdate(UpdateType.Fixed) 
                .OnUpdate(() => {
                    Vector3 newAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, Time.fixedDeltaTime / 1f);
                    transform.rotation = Quaternion.Euler(newAngles);
                })
                .OnComplete(() => {
                    transform.eulerAngles = targetAngles;
                    currentDirection = (Direction)((int)(currentDirection + 3) % 4);
                });

            yield return tweener.WaitForCompletion();
        }
        internal IEnumerator Switch(TargetTile targetTile)
        {
            PlayAnim(AnimationTransition.Push,0.03f);
            
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length);
            targetTile.Switch();
            yield return new WaitForFixedUpdate();
        }
        internal IEnumerator Jump(Position nextPosition, Vector3 lastPlatformBlockPosition)
        {
            PlayAnim(AnimationTransition.Jump,0.01f);
            var duration = _animator.GetCurrentAnimatorClipInfo(0).Length;

            Tweener tweener = transform.DOMove(lastPlatformBlockPosition, duration)
                .SetUpdate(UpdateType.Fixed)
                .OnUpdate(() => {
                    Vector3 newPosition = Vector3.Lerp(transform.position, lastPlatformBlockPosition, Time.fixedDeltaTime / duration);
                    transform.position = newPosition;
                })
                .OnComplete(() => {
                    transform.position = lastPlatformBlockPosition;
                    currentPosition = nextPosition;
                });
            yield return tweener.WaitForCompletion();
        }

        #endregion
        public void Reset()
        {
            currentPosition = new Position(0, 0);
            transform.position= Vector3.zero;
            transform.rotation=  Quaternion.identity;
        }
    }
}