// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.BaseDeathray
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
  public abstract class BaseDeathray : ModProjectile
  {
    protected readonly float maxTime;
    protected readonly string texture;

    protected BaseDeathray(float maxTime, string texture)
    {
      this.maxTime = maxTime;
      this.texture = texture;
    }

    public override void SetDefaults()
    {
      this.Projectile.width = 48;
      this.Projectile.height = 48;
      this.Projectile.hostile = true;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 600;
      this.CooldownSlot = 1;
      this.Projectile.hide = true;
    }

    public override void PostAI() => this.Projectile.hide = false;

    public override bool PreDraw(ref Color lightColor)
    {
      if (Vector2.op_Equality(this.Projectile.velocity, Vector2.Zero))
        return false;
      Texture2D tex1 = TextureAssets.Projectile[this.Type].Value;
      Texture2D texture2D1 = ModContent.Request<Texture2D>("ReturnOfEchdeeath/NPCs/CeilingDeathray").Value;
      Texture2D tex2 = ModContent.Request<Texture2D>("ReturnOfEchdeeath/NPCs/CeilingDeathray").Value;
      float num1 = this.Projectile.localAI[1];
      Color color1 = Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), 0.95f);
      SpriteBatch spriteBatch1 = Main.spriteBatch;
      Texture2D texture2D2 = tex1;
      Vector2 vector2_1 = Vector2.op_Subtraction(this.Projectile.Center, Main.screenPosition);
      Texture2D texture2D3 = texture2D2;
      Vector2 vector2_2 = vector2_1;
      Rectangle? nullable1 = new Rectangle?();
      Color color2 = color1;
      double rotation1 = (double) this.Projectile.rotation;
      Vector2 vector2_3 = Vector2.op_Division(tex1.Size(), 2f);
      double scale1 = (double) this.Projectile.scale;
      spriteBatch1.Draw(texture2D3, vector2_2, nullable1, color2, (float) rotation1, vector2_3, (float) scale1, (SpriteEffects) 0, 0.0f);
      float num2 = num1 - (float) (tex1.Height / 2 + tex2.Height) * this.Projectile.scale;
      Vector2 vector2_4 = Vector2.op_Addition(this.Projectile.Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(this.Projectile.velocity, this.Projectile.scale), (float) tex1.Height), 2f));
      if ((double) num2 > 0.0)
      {
        float num3 = 0.0f;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, 16 * (this.Projectile.timeLeft / 3 % 5), texture2D1.Width, 16);
        while ((double) num3 + 1.0 < (double) num2)
        {
          if ((double) num2 - (double) num3 < (double) rectangle.Height)
            rectangle.Height = (int) ((double) num2 - (double) num3);
          Main.spriteBatch.Draw(texture2D1, Vector2.op_Subtraction(vector2_4, Main.screenPosition), new Rectangle?(rectangle), color1, this.Projectile.rotation, new Vector2((float) (rectangle.Width / 2), 0.0f), this.Projectile.scale, (SpriteEffects) 0, 0.0f);
          num3 += (float) rectangle.Height * this.Projectile.scale;
          vector2_4 = Vector2.op_Addition(vector2_4, Vector2.op_Multiply(Vector2.op_Multiply(this.Projectile.velocity, (float) rectangle.Height), this.Projectile.scale));
          rectangle.Y += 16;
          if (rectangle.Y + rectangle.Height > texture2D1.Height)
            rectangle.Y = 0;
        }
      }
      SpriteBatch spriteBatch2 = Main.spriteBatch;
      Texture2D texture2D4 = tex2;
      Vector2 vector2_5 = Vector2.op_Subtraction(vector2_4, Main.screenPosition);
      Texture2D texture2D5 = texture2D4;
      Vector2 vector2_6 = vector2_5;
      Rectangle? nullable2 = new Rectangle?();
      Color color3 = color1;
      double rotation2 = (double) this.Projectile.rotation;
      Vector2 vector2_7 = tex2.Frame().Top();
      double scale2 = (double) this.Projectile.scale;
      spriteBatch2.Draw(texture2D5, vector2_6, nullable2, color3, (float) rotation2, vector2_7, (float) scale2, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      float collisionPoint = 0.0f;
      return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), this.Projectile.Center, Vector2.op_Addition(this.Projectile.Center, Vector2.op_Multiply(this.Projectile.velocity, this.Projectile.localAI[1])), 22f * this.Projectile.scale, ref collisionPoint) ? new bool?(true) : new bool?(false);
    }
  }
}
