#nullable enable

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace dmdspirit.Core.UI
{
    public class ElementsList : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _elements = null!;

        [SerializeField]
        private GameObject _elementPrefab = null!;

        [SerializeField]
        private Transform _root = null!;

        public List<T> GetElements<T>(int count) where T : Component
        {
            List<GameObject> elements = GetElements(count);
            var result = new List<T>();
            foreach (GameObject element in elements)
            {
                var typedElement = element.GetComponent<T>();
                Assert.IsNotNull(typedElement);
                result.Add(element.GetComponent<T>());
            }

            return result;
        }

        public List<GameObject> GetElements(int count)
        {
            var result = new List<GameObject>();
            if (_elements.Count < count)
                CreateElements(count - _elements.Count);

            for (var i = 0; i < _elements.Count; i++)
            {
                bool isActive = i < count;
                _elements[i].gameObject.SetActive(isActive);
                if (isActive)
                    result.Add(_elements[i]);
            }

            return result;
        }

        public void HideElements()
        {
            foreach (GameObject element in _elements)
                element.gameObject.SetActive(false);
        }

        private void CreateElements(int count)
        {
            for (var i = 0; i < count; i++)
            {
                GameObject? element = Instantiate(_elementPrefab, _root);
                _elements.Add(element);
            }
        }
    }
}