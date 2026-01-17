// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.GunMagnum
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace ReturnOfEchdeeath.NPCs
{
  public class GunMagnum : GunCelebration
  {
    public override void SetStaticDefaults() => this.DisplayName.Equals((object) "Venus Magnum");

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.NPC.width = 52;
      this.NPC.height = 28;
    }

    public override void Offset(NPC guntera)
    {
      this.NPC.Center = Vector2.op_Addition(guntera.Center, new Vector2(-36f, -42f).RotatedBy((double) guntera.rotation, new Vector2()));
    }
  }
}
