using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ISSDisplay;

[Autoload(false)]
public class ISSDisplayContent : InfoDisplay
{

    public int value = 0;
    public bool active = true;
    public override bool Active()
    {
        return active;
    }
    
    public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
    {
        string text = $"Urine Tank Quantity: {value}%";
        return text;
    }
}