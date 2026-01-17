// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.CeilingSphere
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath.NPCs
{
  public class CeilingSphere : ModProjectile
  {
    public override string Texture => "Terraria/Images/Projectile_454";

    public override void SetStaticDefaults()
    {
      this.DisplayName.Equals((object) "Phantasmal Sphere");
      Main.projFrames[this.Projectile.type] = 2;
    }

    public override void SetDefaults()
    {
      this.Projectile.width = 46;
      this.Projectile.height = 46;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 360;
    }

    public override bool CanHitPlayer(Terraria.Player target) => target.hurtCooldowns[1] == 0;

    public override void AI()
    {
      if (this.Projectile.alpha > 200)
        this.Projectile.alpha = 200;
      this.Projectile.alpha -= 5;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      this.Projectile.scale = (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue);
      if ((double) this.Projectile.scale >= 1.0)
      {
        float[] localAi = this.Projectile.localAI;
        int index = 0;
        float num = localAi[index] + 1f;
        localAi[index] = num;
        if ((double) num == 20.0)
        {
          this.Projectile.velocity = Vector2.op_Multiply(32f, this.Projectile.DirectionTo(Main.player[(int) this.Projectile.ai[0]].Center));
          this.Projectile.netUpdate = true;
        }
      }
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter < 6)
        return;
      this.Projectile.frameCounter = 0;
      ++this.Projectile.frame;
      if (this.Projectile.frame <= 1)
        return;
      this.Projectile.frame = 0;
    }

    public override Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue)));
    }

    public override bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle r;
      // ISSUE: explicit constructor call
      ((Rectangle) ref r).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(r.Size(), 2f);
      Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(this.Projectile.Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(r), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public override void Kill(int timeleft)
    {
      this.Projectile.position = this.Projectile.Center;
      this.Projectile.width = this.Projectile.height = 208;
      this.Projectile.position.X -= (float) (this.Projectile.width / 2);
      this.Projectile.position.Y -= (float) (this.Projectile.height / 2);
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = Dust.NewDust(this.Projectile.position, this.Projectile.width, this.Projectile.height, 31, Alpha: 100, newColor: new Color(), Scale: 1.5f);
        Main.dust[index2].position = Vector2.op_Addition(Vector2.op_Multiply(new Vector2((float) (this.Projectile.width / 2), 0.0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), this.Projectile.Center);
      }
      for (int index3 = 0; index3 < 30; ++index3)
      {
        int index4 = Dust.NewDust(this.Projectile.position, this.Projectile.width, this.Projectile.height, 229, newColor: new Color(), Scale: 2.5f);
        Main.dust[index4].position = Vector2.op_Addition(Vector2.op_Multiply(new Vector2((float) (this.Projectile.width / 2), 0.0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), this.Projectile.Center);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 1f);
        int index5 = Dust.NewDust(this.Projectile.position, this.Projectile.width, this.Projectile.height, 229, Alpha: 100, newColor: new Color(), Scale: 1.5f);
        Main.dust[index5].position = Vector2.op_Addition(Vector2.op_Multiply(new Vector2((float) (this.Projectile.width / 2), 0.0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), this.Projectile.Center);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 1f);
        Main.dust[index5].noGravity = true;
      }
    }
  }
}
