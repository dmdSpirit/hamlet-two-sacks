#nullable enable

using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace HamletTwoSacks.Infrastructure
{
    public sealed class SceneLoader
    {
        public async UniTask LoadSceneAdditive(int index)
            => await SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
    }
}