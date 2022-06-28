using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Ui.Stats
{
    public class StatsView : MonoBehaviour
    {
        [SerializeField] [Required] private StatView _statViewPrefab;
        [SerializeField] [Required] private Transform _parent;

        private readonly Dictionary<Type, StatView> _views = new();

        public StatView GetOrAdd<TStat>()
        {
            var type = typeof(TStat);
            if (_views.TryGetValue(type, out var view)) return view;

            view = Instantiate(_statViewPrefab, _parent);
            view.Appear();
            _views[type] = view;
            return view;
        }
    }
}