using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Credits Settings", menuName = "GameSettings/CreditsSettings", order = 0)]
    public class CreditsSettings : ScriptableObject
    {
        [SerializeField] private List<CreditsData> _credits;

        public IEnumerable<CreditsData> Credits => _credits;
    }
}