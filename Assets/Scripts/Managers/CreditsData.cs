using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CreditsData
{
    [SerializeField] private string _title;
    [SerializeField] private List<string> _infoRows;

    public string Title => _title;

    public IEnumerable<string> InfoRows => _infoRows;
}