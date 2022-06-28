using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.Combo
{
    public class PlayerComboView : MonoBehaviour
    {
        [SerializeField] [Required] private TMP_Text _text;
        [SerializeField] [Required] private string _format = "Combo x{0:0}";

        public void DisplayCombo(int combo)
        {
            _text.SetText(_format, combo);
        }
    }
}