// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.Moonified
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath.NPCs
{
  public class Moonified : ModBuff
  {
    public override void SetStaticDefaults()
    {
      this.DisplayName.Equals((object) nameof (Moonified));
      this.Description.Equals((object) "There is a giant moon coming for you, who wouldn't be scared?\nOh, by the way, you can't escape.");
      Main.debuff[this.Type] = true;
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
    }
  }
}
