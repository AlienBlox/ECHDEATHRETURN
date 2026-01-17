// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.GunCelebration
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath.NPCs
{
  public class GunCelebration : ModNPC
  {
    public override void SetStaticDefaults() => this.DisplayName.Equals((object) "Celebration Mk2");

    public override void SetDefaults()
    {
      this.NPC.width = 78;
      this.NPC.height = 28;
      this.NPC.damage = 500;
      this.NPC.defense = 100;
      this.NPC.lifeMax = 1500000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit4);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath14);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      for (int index = 0; index < this.NPC.buffImmune.Length; ++index)
        this.NPC.buffImmune[index] = true;
      this.NPC.buffImmune[ModContent.BuffType<Gun>()] = false;
      this.NPC.aiStyle = -1;
    }

    public virtual void Offset(NPC guntera)
    {
      this.NPC.Center = Vector2.op_Addition(guntera.Center, new Vector2(0.0f, -64f).RotatedBy((double) guntera.rotation, new Vector2()));
    }

    public override void AI()
    {
      this.NPC.timeLeft = 60;
      if ((double) this.NPC.localAI[3] == 0.0)
        this.NPC.localAI[3] = Main.rand.NextFloat(4f);
      int index = (int) this.NPC.ai[0];
      if (index < 0 || index >= 200 || !Main.npc[index].active || Main.npc[index].type != ModContent.NPCType<Guntera>())
      {
        this.NPC.active = false;
      }
      else
      {
        this.NPC.TargetClosest(false);
        this.NPC.direction = this.NPC.spriteDirection = (double) this.NPC.ai[0] < 0.0 ? -1 : 1;
        this.Offset(Main.npc[index]);
        float rotation = this.NPC.DirectionTo(Main.player[this.NPC.target].Center).ToRotation();
        float num1 = 3.14159274f * this.NPC.localAI[3];
        float num2 = (float) (3.1415927410125732 * ((double) this.NPC.localAI[3] + 2.0));
        if ((double) rotation > (double) num2)
          rotation -= 6.28318548f;
        if ((double) rotation < (double) num1)
          rotation += 6.28318548f;
        this.NPC.rotation += (float) Math.Sign(rotation - this.NPC.rotation) * MathHelper.ToRadians(1f);
        if ((double) this.NPC.rotation > (double) num2)
          this.NPC.rotation -= 6.28318548f;
        if ((double) this.NPC.rotation < (double) num1)
          this.NPC.rotation += 6.28318548f;
        this.NPC.damage = this.NPC.defDamage;
        this.NPC.defense = this.NPC.defDefense;
        ++this.NPC.localAI[1];
        if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.9 || (double) Main.npc[index].life < (double) Main.npc[index].lifeMax * 0.9)
          ++this.NPC.localAI[1];
        ++this.NPC.localAI[1];
        if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.8 || (double) Main.npc[index].life < (double) Main.npc[index].lifeMax * 0.8)
          ++this.NPC.localAI[1];
        ++this.NPC.localAI[1];
        if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.7 || (double) Main.npc[index].life < (double) Main.npc[index].lifeMax * 0.7)
          ++this.NPC.localAI[1];
        ++this.NPC.localAI[1];
        if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.6 || (double) Main.npc[index].life < (double) Main.npc[index].lifeMax * 0.6)
          ++this.NPC.localAI[1];
        ++this.NPC.localAI[1];
        if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.5 || (double) Main.npc[index].life < (double) Main.npc[index].lifeMax * 0.5)
          ++this.NPC.localAI[1];
        if (!Main.player[this.NPC.target].ZoneJungle || (double) Main.player[this.NPC.target].position.Y < Main.worldSurface * 16.0 || (double) Main.player[this.NPC.target].position.Y > (double) ((Main.maxTilesY - 200) * 16))
        {
          this.NPC.localAI[1] += 3f;
          this.NPC.damage = this.NPC.defDamage * 10;
          this.NPC.defense = this.NPC.defDefense * 10;
        }
        if ((double) this.NPC.localAI[1] <= 80.0)
          return;
        this.NPC.localAI[1] = (float) Main.rand.Next(-20, 20);
        float num3 = (float) (1.0 - (double) this.NPC.life / (double) this.NPC.lifeMax);
        if ((double) num3 < 1.0 - (double) Main.npc[index].life / (double) Main.npc[index].lifeMax)
          num3 = (float) (1.0 - (double) Main.npc[index].life / (double) Main.npc[index].lifeMax);
        float num4 = num3 * 12f + 3f;
        if (Main.netMode == 1)
          return;
        Projectile.NewProjectile(Terraria.Entity.GetSource_None(), this.NPC.Center, Vector2.op_Multiply(this.NPC.rotation.ToRotationVector2(), num4), ModContent.ProjectileType<GunteraBullet>(), this.NPC.damage / 4, 0.0f, Main.myPlayer, this.NPC.Distance(Main.player[this.NPC.target].Center) / num4);
      }
    }

    public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
    {
      projectile.timeLeft = 0;
    }

    public override void OnHitPlayer(Terraria.Player target, Terraria.Player.HurtInfo hurtInfo)
    {
      target.AddBuff(ModContent.BuffType<Gun>(), 600);
    }

    public override bool CheckActive() => false;

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2 = Vector2.op_Division(frame.Size(), 2f);
      Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(this.NPC.Center, Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2, this.NPC.scale, this.NPC.spriteDirection > 0 ? (SpriteEffects) 1 : (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
