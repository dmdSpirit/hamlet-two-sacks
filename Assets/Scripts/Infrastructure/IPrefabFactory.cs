#nullable enable
using UnityEngine;

namespace HamletTwoSacks.Infrastructure
{
    public interface IPrefabFactory
    {
        T CreateObject<T>() where T : MonoBehaviour;
        T CreateCopyObject<T>(T prefab) where T : MonoBehaviour;
    }
}