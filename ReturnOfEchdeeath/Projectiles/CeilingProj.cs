// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.Projectiles.CeilingProj
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReturnOfEchdeeath.NPCs;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath.Projectiles
{
  public class CeilingProj : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      this.DisplayName.Equals((object) "Ceiling of Moon Lord");
    }

    public override void SetDefaults()
    {
      this.Projectile.width = 420;
      this.Projectile.height = 190;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.hide = true;
    }

    public override void AI()
    {
      int index = (int) this.Projectile.ai[1];
      if ((double) this.Projectile.ai[1] >= 0.0 && (double) this.Projectile.ai[1] < 200.0 && Main.npc[index].active && Main.npc[index].type == ModContent.NPCType<CeilingOfMoonLord>())
      {
        this.Projectile.Center = Main.npc[index].Center;
        this.Projectile.timeLeft = 2;
      }
      else
        this.Projectile.Kill();
    }

    public override bool? CanDamage() => new bool?(false);

    public override Color? GetAlpha(Color lightColor) => new Color?(Color.White);

    public override void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      if (!this.Projectile.hide)
        return;
      behindNPCs.Add(index);
    }

    public override bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle r;
      // ISSUE: explicit constructor call
      ((Rectangle) ref r).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(r.Size(), 2f);
      for (int index = 0; index < Main.screenWidth; index += 210)
      {
        Vector2 vector2_2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_2).\u002Ector(Main.screenPosition.X + (float) index, this.Projectile.Center.Y - 50f);
        Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(r), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }
  }
}
