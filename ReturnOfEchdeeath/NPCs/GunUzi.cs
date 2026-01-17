// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.GunUzi
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace ReturnOfEchdeeath.NPCs
{
  public class GunUzi : GunCelebration
  {
    public override void SetStaticDefaults() => this.DisplayName.Equals((object) "Uzi");

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.NPC.width = 48;
      this.NPC.height = 36;
    }

    public override void Offset(NPC guntera)
    {
      this.NPC.Center = Vector2.op_Addition(guntera.Center, new Vector2(36f, -42f).RotatedBy((double) guntera.rotation, new Vector2()));
    }
  }
}
