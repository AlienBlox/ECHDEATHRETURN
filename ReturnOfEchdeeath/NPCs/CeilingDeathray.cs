// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.CeilingDeathray
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath.NPCs
{
  public class CeilingDeathray : BaseDeathray
  {
    public CeilingDeathray()
      : base(120f, "PhantasmalDeathrayML")
    {
    }

    public override void SetStaticDefaults()
    {
      this.DisplayName.Equals((object) "Phantasmal Deathray");
    }

    public override bool CanHitPlayer(Terraria.Player target) => target.hurtCooldowns[1] == 0;

    public override void AI()
    {
      this.Projectile.hide = false;
      Vector2? nullable = new Vector2?();
      if (this.Projectile.velocity.HasNaNs() || Vector2.op_Equality(this.Projectile.velocity, Vector2.Zero))
        this.Projectile.velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      int index1 = (int) this.Projectile.ai[1];
      if (!Main.npc[index1].active || Main.npc[index1].type != ModContent.NPCType<CeilingOfMoonLordFace>())
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.Center = Main.npc[index1].Center;
        if (this.Projectile.velocity.HasNaNs() || Vector2.op_Equality(this.Projectile.velocity, Vector2.Zero))
          this.Projectile.velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
        double num1 = (double) this.Projectile.localAI[0];
        float num2 = 1.5f;
        ++this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
        {
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * 10f * num2;
          if ((double) this.Projectile.scale > (double) num2)
            this.Projectile.scale = num2;
          float f = this.Projectile.velocity.ToRotation() + this.Projectile.ai[0];
          this.Projectile.rotation = f - 1.57079637f;
          this.Projectile.velocity = f.ToRotationVector2();
          float length = 3f;
          float width = (float) this.Projectile.width;
          Vector2 center = this.Projectile.Center;
          if (nullable.HasValue)
            center = nullable.Value;
          float[] samples = new float[(int) length];
          Collision.LaserScan(center, this.Projectile.velocity, width * this.Projectile.scale, 3000f, samples);
          float num3 = 0.0f;
          for (int index2 = 0; index2 < samples.Length; ++index2)
            num3 += samples[index2];
          this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num3 / length, 0.5f);
          Vector2 Position = Vector2.op_Addition(this.Projectile.Center, Vector2.op_Multiply(this.Projectile.velocity, this.Projectile.localAI[1] - 14f));
          for (int index3 = 0; index3 < 2; ++index3)
          {
            float num4 = this.Projectile.velocity.ToRotation() + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.5707963705062866);
            float num5 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
            Vector2 vector2;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2).\u002Ector((float) Math.Cos((double) num4) * num5, (float) Math.Sin((double) num4) * num5);
            int index4 = Dust.NewDust(Position, 0, 0, 244, vector2.X, vector2.Y, newColor: new Color());
            Main.dust[index4].noGravity = true;
            Main.dust[index4].scale = 1.7f;
          }
          if (Main.rand.Next(5) == 0)
          {
            Vector2 vector2 = Vector2.op_Multiply(Vector2.op_Multiply(this.Projectile.velocity.RotatedBy(1.5707963705062866, new Vector2()), (float) Main.rand.NextDouble() - 0.5f), (float) this.Projectile.width);
            int index5 = Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(Position, vector2), Vector2.op_Multiply(Vector2.One, 4f)), 8, 8, 244, Alpha: 100, newColor: new Color(), Scale: 1.5f);
            Dust dust = Main.dust[index5];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
            Main.dust[index5].velocity.Y = -Math.Abs(Main.dust[index5].velocity.Y);
          }
          DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
        }
      }
    }
  }
}
