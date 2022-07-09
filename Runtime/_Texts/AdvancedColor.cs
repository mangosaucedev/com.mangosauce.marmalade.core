using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.Texts
{
    public class AdvancedColor : Command
    {
        public override CommandType Type => CommandType.AdvColor;

        protected override void ApplyCommandToTargetText(ref string text)
        {
            string colorName = arguments[0];
            Color color = Assets.Get<Asset<Color>>(colorName).Contents;
            string hex = ColorUtility.ToHtmlStringRGB(color);
            text = text.Insert(0, $"<color=#{hex}>");
            text = text.Insert(text.Length, "</color>");
        }
    }
}
