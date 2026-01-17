// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.CeilingOfMoonLord
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReturnOfEchdeeath.Projectiles;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath.NPCs
{
  [AutoloadBossHead]
  public class CeilingOfMoonLord : ModNPC
  {
    private int ceilingProj = -1;

    public override void SetStaticDefaults()
    {
      this.DisplayName.Equals((object) "Ceiling of Moon Lord");
    }

    public override void SetDefaults()
    {
      this.NPC.width = 46;
      this.NPC.height = 60;
      this.NPC.damage = 125;
      this.NPC.defense = 50;
      this.NPC.lifeMax = 6500000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit57);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath11);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.buffImmune[68] = true;
      this.NPC.buffImmune[31] = true;
      this.NPC.aiStyle = -1;
      this.NPC.value = (float) Item.buyPrice(1, 50);
      this.NPC.boss = true;
      this.NPC.netAlways = true;
    }

    public override void SendExtraAI(BinaryWriter writer) => writer.Write(this.ceilingProj);

    public override void ReceiveExtraAI(BinaryReader reader)
    {
      this.ceilingProj = reader.ReadInt32();
    }

    public override void AI()
    {
      this.NPC.timeLeft = 60;
      if (!this.NPC.HasValidTarget)
        this.NPC.TargetClosest(false);
      if (this.ceilingProj < 0 || this.ceilingProj >= 1000 || !Main.projectile[this.ceilingProj].active || Main.projectile[this.ceilingProj].type != ModContent.ProjectileType<CeilingProj>())
      {
        if (Main.netMode != 1)
          this.ceilingProj = Projectile.NewProjectile(Terraria.Entity.GetSource_None(), this.NPC.Center, Vector2.Zero, ModContent.ProjectileType<CeilingProj>(), 0, 0.0f, Main.myPlayer, ai1: (float) this.NPC.whoAmI);
        this.NPC.netUpdate = true;
      }
      if ((double) this.NPC.localAI[3] == 0.0)
      {
        this.NPC.localAI[3] = 1f;
        if (Main.netMode != 1)
        {
          int number1 = NPC.NewNPC(this.NPC.GetSource_FromAI(), (int) this.NPC.Center.X, (int) this.NPC.Center.Y, ModContent.NPCType<CeilingOfMoonLordEye>(), this.NPC.whoAmI, (float) this.NPC.whoAmI, 1f);
          if (number1 != 200 && Main.netMode == 2)
            NetMessage.SendData(23, number: number1);
          this.NPC.ai[0] = (float) number1;
          int number2 = NPC.NewNPC(this.NPC.GetSource_FromAI(), (int) this.NPC.Center.X, (int) this.NPC.Center.Y, ModContent.NPCType<CeilingOfMoonLordEye>(), this.NPC.whoAmI, (float) this.NPC.whoAmI, -1f);
          if (number2 != 200 && Main.netMode == 2)
            NetMessage.SendData(23, number: number2);
          this.NPC.ai[1] = (float) number2;
          int number3 = NPC.NewNPC(this.NPC.GetSource_FromAI(), (int) this.NPC.Center.X, (int) this.NPC.Center.Y, ModContent.NPCType<CeilingOfMoonLordFace>(), this.NPC.whoAmI, (float) this.NPC.whoAmI);
          if (number3 != 200 && Main.netMode == 2)
            NetMessage.SendData(23, number: number3);
          this.NPC.ai[2] = (float) number3;
        }
        this.NPC.netUpdate = true;
      }
      if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
      {
        Main.LocalPlayer.AddBuff(ModContent.BuffType<Moonified>(), 2);
        if ((double) Main.LocalPlayer.Center.Y < (double) this.NPC.Center.Y - 32.0 || (double) Main.LocalPlayer.Center.Y > (double) this.NPC.Center.Y + 1500.0 || (double) Math.Abs(Main.LocalPlayer.Center.X - this.NPC.Center.X) > 1500.0)
        {
          Vector2 center = this.NPC.Center;
          center.Y += 250f;
          bool flag = (double) Main.LocalPlayer.Center.Y < (double) this.NPC.Center.Y;
          for (int index1 = 0; index1 < 50; ++index1)
          {
            int index2 = Dust.NewDust(Main.player[Main.myPlayer].position, Main.player[Main.myPlayer].width, Main.player[Main.myPlayer].height, 229, newColor: new Color(), Scale: 2.5f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].noLight = true;
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 9f);
          }
          Main.LocalPlayer.Teleport(center);
          NetMessage.SendData(65, number2: (float) Main.LocalPlayer.whoAmI, number3: center.X, number4: center.Y, number5: 1);
          Main.LocalPlayer.velocity = Vector2.Zero;
          if (flag)
          {
            Main.LocalPlayer.Hurt(PlayerDeathReason.ByNPC(this.NPC.whoAmI), this.NPC.damage, 1, dodgeable: false);
            Main.LocalPlayer.immune = false;
            Main.LocalPlayer.immuneTime = 0;
            Main.LocalPlayer.hurtCooldowns[0] = 0;
            Main.LocalPlayer.hurtCooldowns[1] = 0;
          }
          for (int index3 = 0; index3 < 50; ++index3)
          {
            int index4 = Dust.NewDust(Main.player[Main.myPlayer].position, Main.player[Main.myPlayer].width, Main.player[Main.myPlayer].height, 229, newColor: new Color(), Scale: 2.5f);
            Main.dust[index4].noGravity = true;
            Main.dust[index4].noLight = true;
            Dust dust = Main.dust[index4];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 9f);
          }
        }
        if (Main.LocalPlayer.ZoneUnderworldHeight)
          Main.LocalPlayer.KillMe(PlayerDeathReason.ByOther(12), 9999.0, 0);
      }
      if ((double) this.NPC.position.Y > (double) (Main.maxTilesY * 16))
        this.NPC.active = false;
      this.NPC.velocity.Y = 1.5f;
      if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.75)
        this.NPC.velocity.Y += 0.25f;
      if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.5)
        this.NPC.velocity.Y += 0.4f;
      if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.25)
        this.NPC.velocity.Y += 0.5f;
      if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.1)
        this.NPC.velocity.Y += 0.6f;
      if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.66 && Main.expertMode)
        this.NPC.velocity.Y += 0.3f;
      if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.33 && Main.expertMode)
        this.NPC.velocity.Y += 0.3f;
      if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.05 && Main.expertMode)
        this.NPC.velocity.Y += 0.6f;
      if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.035 && Main.expertMode)
        this.NPC.velocity.Y += 0.6f;
      if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.025 && Main.expertMode)
        this.NPC.velocity.Y += 0.6f;
      if (Main.expertMode)
        this.NPC.velocity.Y = (float) ((double) this.NPC.velocity.Y * 1.3500000238418579 + 0.34999999403953552);
      if ((double) Math.Abs(Main.player[this.NPC.target].Center.X - this.NPC.Center.X) > 150.0)
        this.NPC.velocity.X = 2f * this.NPC.velocity.Y * (float) Math.Sign(Main.player[this.NPC.target].Center.X - this.NPC.Center.X);
      if (this.NPC.life < this.NPC.lifeMax / 2)
        ++this.NPC.localAI[0];
      float[] localAi = this.NPC.localAI;
      int index5 = 0;
      float num = localAi[index5] + 1f;
      localAi[index5] = num;
      if ((double) num <= 300.0)
        return;
      this.NPC.localAI[0] = 0.0f;
      if (Main.netMode == 1)
        return;
      for (int index6 = 0; index6 < 6; ++index6)
        Projectile.NewProjectile(Terraria.Entity.GetSource_None(), Vector2.op_Addition(this.NPC.Center, Main.rand.NextVector2Square(0.0f, (float) this.NPC.width)), Vector2.op_Multiply(Vector2.UnitX.RotatedByRandom(Math.PI), 6f), 452, this.NPC.damage / 7, 0.0f, Main.myPlayer);
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = Dust.NewDust(this.NPC.position, this.NPC.width, this.NPC.height, 229, newColor: new Color());
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
      }
    }

    public override bool CheckActive() => false;

    public override Color? GetAlpha(Color drawColor) => new Color?(Color.White);

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = TextureAssets.Npc[this.Type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2 = Vector2.op_Division(frame.Size(), 2f);
      spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(this.NPC.Center, Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2, this.NPC.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
