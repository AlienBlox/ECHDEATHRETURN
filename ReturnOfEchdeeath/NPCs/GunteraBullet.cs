// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.GunteraBullet
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath.NPCs
{
  public class GunteraBullet : ModProjectile
  {
    public override void SetStaticDefaults() => this.DisplayName.Equals((object) "Bullet");

    public override void SetDefaults()
    {
      this.Projectile.width = 4;
      this.Projectile.height = 4;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = -1;
      this.Projectile.light = 0.5f;
      this.Projectile.scale = 1.2f;
      this.Projectile.extraUpdates = 1;
      this.CooldownSlot = 1;
    }

    public override void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundStyle style = Main.rand.Next(2) == 0 ? SoundID.Item11 : SoundID.Item40;
        SoundEngine.PlaySound(in style, new Vector2?(this.Projectile.Center));
      }
      float[] ai = this.Projectile.ai;
      int index = 0;
      float num = ai[index] - 1f;
      ai[index] = num;
      if ((double) num < 0.0)
        this.Projectile.tileCollide = true;
      this.Projectile.rotation = this.Projectile.velocity.ToRotation();
    }

    public override void OnHitPlayer(Terraria.Player target, Terraria.Player.HurtInfo hurtInfo)
    {
      target.AddBuff(ModContent.BuffType<Gun>(), 600);
    }
  }
}
