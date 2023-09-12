#nullable enable
using System;
using UnityEngine;

namespace HamletTwoSacks.Buildings.Config
{
    [Serializable]
    public class BuildingTier
    {
        public int Cost;
        public bool IsActive;
        public Sprite Image = null!;
        public Vector2 ImageSize;
        public Vector2 ImageOffset;
    }
}