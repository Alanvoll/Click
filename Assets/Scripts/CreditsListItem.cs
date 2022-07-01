using UnityEngine;
using UnityEngine.UI;


public class CreditsListItem : MonoBehaviour
{
    [SerializeField] private Text _titleText;
    [SerializeField] private Text _infoText;

    public void SetData(CreditsData credit)
    {
        _titleText.text = credit.Title;
        foreach (var infoRow in credit.InfoRows)
        {
            _infoText.text += $"{infoRow}\n";
        }
    }
}
