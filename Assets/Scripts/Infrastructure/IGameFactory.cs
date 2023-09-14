#nullable enable

namespace HamletTwoSacks.Infrastructure
{
    public interface IGameFactory
    {
        public void BindSceneFactory(ScenePrefabFactory scenePrefabFactory);
        public void UnbindSceneFactory(ScenePrefabFactory scenePrefabFactory);
    }
}