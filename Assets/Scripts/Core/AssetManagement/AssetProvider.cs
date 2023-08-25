#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace dmdspirit.Core.AssetManagement
{
    [UsedImplicitly]
    public sealed class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _loadingHandles = new();

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_completedCache.TryGetValue(assetReference.RuntimeKey.ToString(),
                                            out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T ?? throw new InvalidOperationException();

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);
            handle.Completed += OnHandleOnCompleted;
            AddHandle(assetReference.RuntimeKey.ToString(), handle);
            return await handle.Task;

            void OnHandleOnCompleted(AsyncOperationHandle<T> asyncHandle)
                => _completedCache[assetReference.RuntimeKey.ToString()] = asyncHandle;
        }

        public void CleanUp()
        {
            foreach (List<AsyncOperationHandle>? asyncOperationHandles in _loadingHandles.Values)
            {
                foreach (AsyncOperationHandle asyncOperationHandle in asyncOperationHandles)
                    Addressables.Release(asyncOperationHandle);
            }
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_loadingHandles.TryGetValue(key, out List<AsyncOperationHandle>? resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _loadingHandles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }
    }
}