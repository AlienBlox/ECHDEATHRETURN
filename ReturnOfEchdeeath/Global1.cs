// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.Global1
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath
{
  public class Global1 : GlobalItem
  {
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
      if (item.type != ModContent.ItemType<SoulofCreation>())
        return;
      TooltipLine tooltipLine1 = new TooltipLine(this.Mod, "ChangingTextTooltipLine", RandomSystem.TooltipRandom())
      {
        OverrideColor = new Color?(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue))
      };
      tooltips.Add(tooltipLine1);
      TooltipLine tooltipLine2 = new TooltipLine(this.Mod, "ChangingTextTooltipLine2", "'Power of Transcenedent cat entity.'")
      {
        OverrideColor = new Color?(Main.DiscoColor)
      };
      tooltips.Add(tooltipLine2);
    }
  }
}
