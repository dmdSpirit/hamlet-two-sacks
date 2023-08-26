#nullable enable
using System;
using UnityEngine;

namespace HamletTwoSacks.Character
{
    public sealed class SpriteFlipper : MonoBehaviour
    {
        
        private const float TOLERANCE = 0.01f;

        [SerializeField]
        private SpriteRenderer _spriteRenderer = null!;

        public void FlipSprite(float speed)
        {
            if (speed == 0)
                return;
            Vector3 currentScale = _spriteRenderer.transform.localScale;
            if (Math.Abs(Mathf.Sign(speed) - Mathf.Sign(currentScale.x)) <= TOLERANCE)
                return;
            currentScale.x *= -1;
            _spriteRenderer.transform.localScale = currentScale;
        }
    }
}