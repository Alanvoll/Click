using UnityEngine;
using UnityEngine.UI;

public class RecordsListItem : MonoBehaviour
{
    [SerializeField] private Text _numberText;
    [SerializeField] private Text _valueText;
    [SerializeField] private Text _dateText;

    [SerializeField] private Color _firstColor;
    [SerializeField] private Color _secondColor;
    [SerializeField] private Color _thirdColor;
    [SerializeField] private Color _defaultColor;

    public void SetNumber(int number)
    {
        _numberText.text = $"{number}";
        switch (number)
        {
            case 1:
                SetTextColor(_firstColor);
                break;
            case 2:
                SetTextColor(_secondColor);
                break;
            case 3:
                SetTextColor(_thirdColor);
                break;
            default:
                SetTextColor(_defaultColor);
                break;
        }
    }

    private void SetTextColor(Color color)
    {
        _numberText.color = color;
        _valueText.color = color;
        _dateText.color = color;
    }

    public void SetData(Record record)
    {
        _valueText.text = $"{record.Score}";
        _dateText.text = $"{record.Date}";
    }
}