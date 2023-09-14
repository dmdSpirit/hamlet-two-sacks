#nullable enable
using UnityEngine;

namespace HamletTwoSacks.Infrastructure
{
    public interface IPrefabFactory
    {
        T CreateObject<T>() where T : MonoBehaviour;
    }
}