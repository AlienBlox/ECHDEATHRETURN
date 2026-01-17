// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.Guntera
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
  [AutoloadBossHead]
  public class Guntera : ModNPC
  {
    public override void SetStaticDefaults()
    {
      this.DisplayName.Equals((object) nameof (Guntera));
      Main.npcFrameCount[this.NPC.type] = 8;
    }

    public override void SetDefaults()
    {
      this.NPC.width = 86;
      this.NPC.height = 86;
      this.NPC.damage = 375;
      this.NPC.defense = 100;
      this.NPC.lifeMax = 1500000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      for (int index = 0; index < this.NPC.buffImmune.Length; ++index)
        this.NPC.buffImmune[index] = true;
      this.NPC.buffImmune[ModContent.BuffType<Gun>()] = false;
      this.NPC.aiStyle = -1;
      this.NPC.value = (float) Item.buyPrice(2);
      this.NPC.boss = true;
      this.NPC.netAlways = true;
    }

    public override void AI()
    {
      this.NPC.timeLeft = 60;
      this.NPC.TargetClosest(false);
      if (this.NPC.HasValidTarget && (double) this.NPC.Distance(Main.player[this.NPC.target].Center) < 6000.0)
      {
        this.NPC.timeLeft = 60;
      }
      else
      {
        this.NPC.active = false;
        this.NPC.life = 0;
        NetMessage.SendData(23, number: this.NPC.whoAmI);
      }
      this.NPC.damage = this.NPC.defDamage;
      this.NPC.defense = this.NPC.defDefense;
      if ((double) this.NPC.localAI[1] == 0.0 && (double) this.NPC.life < (double) this.NPC.lifeMax * 0.40000000596046448)
      {
        this.NPC.localAI[1] = 1f;
        if (Main.netMode != 1)
        {
          for (int ai2 = 0; ai2 < 200; ++ai2)
          {
            if (Main.npc[ai2].active && Main.npc[ai2].type == ModContent.NPCType<GunteraHook>())
            {
              for (int index = 0; index < 3; ++index)
              {
                int number = NPC.NewNPC(this.NPC.GetSource_FromAI(), (int) this.NPC.Center.X, (int) this.NPC.Center.Y, ModContent.NPCType<GunteraTentacle>(), this.NPC.whoAmI, ai2: (float) ai2, ai3: (float) this.NPC.whoAmI);
                if (number != 200 && Main.netMode == 2)
                  NetMessage.SendData(23, number: number);
              }
            }
          }
        }
      }
      if ((double) this.NPC.localAI[2] == 0.0 && this.NPC.life < this.NPC.lifeMax / 2)
      {
        this.NPC.localAI[2] = 1f;
        if (Main.netMode != 1)
        {
          for (int index = 0; index < 6; ++index)
          {
            int number = NPC.NewNPC(this.NPC.GetSource_FromAI(), (int) this.NPC.Center.X, (int) this.NPC.Center.Y, ModContent.NPCType<GunteraHook>(), this.NPC.whoAmI, ai3: (float) this.NPC.whoAmI);
            if (number != 200 && Main.netMode == 2)
              NetMessage.SendData(23, number: number);
          }
        }
      }
      if ((double) this.NPC.localAI[3] == 0.0)
      {
        this.NPC.localAI[3] = 1f;
        if (Main.netMode != 1)
        {
          int number1 = NPC.NewNPC(this.NPC.GetSource_FromAI(), (int) this.NPC.Center.X, (int) this.NPC.Center.Y, ModContent.NPCType<GunCelebration>(), this.NPC.whoAmI, (float) this.NPC.whoAmI);
          if (number1 != 200 && Main.netMode == 2)
            NetMessage.SendData(23, number: number1);
          int number2 = NPC.NewNPC(this.NPC.GetSource_FromAI(), (int) this.NPC.Center.X, (int) this.NPC.Center.Y, ModContent.NPCType<GunMagnum>(), this.NPC.whoAmI, (float) this.NPC.whoAmI, -1f);
          if (number2 != 200 && Main.netMode == 2)
            NetMessage.SendData(23, number: number2);
          int number3 = NPC.NewNPC(this.NPC.GetSource_FromAI(), (int) this.NPC.Center.X, (int) this.NPC.Center.Y, ModContent.NPCType<GunUzi>(), this.NPC.whoAmI, (float) this.NPC.whoAmI, 1f);
          if (number3 != 200 && Main.netMode == 2)
            NetMessage.SendData(23, number: number3);
          int number4 = NPC.NewNPC(this.NPC.GetSource_FromAI(), (int) this.NPC.Center.X, (int) this.NPC.Center.Y, ModContent.NPCType<GunShotgun>(), this.NPC.whoAmI, (float) this.NPC.whoAmI, -1f);
          if (number4 != 200 && Main.netMode == 2)
            NetMessage.SendData(23, number: number4);
          int number5 = NPC.NewNPC(this.NPC.GetSource_FromAI(), (int) this.NPC.Center.X, (int) this.NPC.Center.Y, ModContent.NPCType<GunQuad>(), this.NPC.whoAmI, (float) this.NPC.whoAmI, 1f);
          if (number5 != 200 && Main.netMode == 2)
            NetMessage.SendData(23, number: number5);
          int number6 = NPC.NewNPC(this.NPC.GetSource_FromAI(), (int) this.NPC.Center.X, (int) this.NPC.Center.Y, ModContent.NPCType<GunSniper>(), this.NPC.whoAmI, (float) this.NPC.whoAmI, -1f);
          if (number6 != 200 && Main.netMode == 2)
            NetMessage.SendData(23, number: number6);
          int number7 = NPC.NewNPC(this.NPC.GetSource_FromAI(), (int) this.NPC.Center.X, (int) this.NPC.Center.Y, ModContent.NPCType<GunSniper>(), this.NPC.whoAmI, (float) this.NPC.whoAmI, 1f);
          if (number7 != 200 && Main.netMode == 2)
            NetMessage.SendData(23, number: number7);
          for (int index = 0; index < 5; ++index)
          {
            int number8 = NPC.NewNPC(this.NPC.GetSource_FromAI(), (int) this.NPC.Center.X, (int) this.NPC.Center.Y, ModContent.NPCType<GunteraHook>(), this.NPC.whoAmI, ai3: (float) this.NPC.whoAmI);
            if (number8 != 200 && Main.netMode == 2)
              NetMessage.SendData(23, number: number8);
          }
        }
        this.NPC.netUpdate = true;
      }
      bool flag1 = false;
      bool flag2 = false;
      if (Main.player[this.NPC.target].dead)
      {
        flag1 = true;
        flag2 = true;
      }
      int[] numArray = new int[11];
      float num1 = 0.0f;
      float num2 = 0.0f;
      int index1 = 0;
      for (int index2 = 0; index2 < 200; ++index2)
      {
        if (Main.npc[index2].active && Main.npc[index2].type == ModContent.NPCType<GunteraHook>())
        {
          num1 += Main.npc[index2].Center.X;
          num2 += Main.npc[index2].Center.Y;
          numArray[index1] = index2;
          ++index1;
          if (index1 >= 11)
            break;
        }
      }
      float num3 = num1 / (float) index1;
      float num4 = num2 / (float) index1;
      float num5 = 2.5f;
      float num6 = 0.025f;
      if (this.NPC.life < this.NPC.lifeMax / 2)
      {
        num5 = 5f;
        num6 = 0.05f;
      }
      if (this.NPC.life < this.NPC.lifeMax / 4)
        num5 = 7f;
      if (!Main.player[this.NPC.target].ZoneJungle || (double) Main.player[this.NPC.target].position.Y < Main.worldSurface * 16.0 || (double) Main.player[this.NPC.target].position.Y > (double) ((Main.maxTilesY - 200) * 16))
      {
        flag1 = true;
        num5 += 8f;
        num6 = 0.15f;
        this.NPC.damage = this.NPC.defDamage * 10;
        this.NPC.defense = this.NPC.defDefense * 10;
      }
      if (Main.expertMode)
      {
        num5 = (float) (((double) num5 + 1.0) * 1.1000000238418579);
        num6 = (float) (((double) num6 + 0.0099999997764825821) * 1.1000000238418579);
      }
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector(num3, num4);
      float num7 = Main.player[this.NPC.target].Center.X - vector2.X;
      float num8 = Main.player[this.NPC.target].Center.Y - vector2.Y;
      if (flag2)
      {
        num8 *= -1f;
        num7 *= -1f;
        num5 += 8f;
      }
      float num9 = (float) Math.Sqrt((double) num7 * (double) num7 + (double) num8 * (double) num8);
      int num10 = 500;
      if (flag1)
        num10 += 350;
      if (Main.expertMode)
        num10 += 150;
      if ((double) num9 >= (double) num10)
      {
        float num11 = (float) num10 / num9;
        num7 *= num11;
        num8 *= num11;
      }
      double num12 = (double) num3 + (double) num7;
      float num13 = num4 + num8;
      Vector2 center = this.NPC.Center;
      double x = (double) center.X;
      float num14 = (float) (num12 - x);
      float num15 = num13 - center.Y;
      float num16 = (float) Math.Sqrt((double) num14 * (double) num14 + (double) num15 * (double) num15);
      float num17;
      float num18;
      if ((double) num16 < (double) num5)
      {
        num17 = this.NPC.velocity.X;
        num18 = this.NPC.velocity.Y;
      }
      else
      {
        float num19 = num5 / num16;
        num17 = num14 * num19;
        num18 = num15 * num19;
      }
      if ((double) this.NPC.velocity.X < (double) num17)
      {
        this.NPC.velocity.X += num6;
        if ((double) this.NPC.velocity.X < 0.0 && (double) num17 > 0.0)
          this.NPC.velocity.X += num6 * 2f;
      }
      else if ((double) this.NPC.velocity.X > (double) num17)
      {
        this.NPC.velocity.X -= num6;
        if ((double) this.NPC.velocity.X > 0.0 && (double) num17 < 0.0)
          this.NPC.velocity.X -= num6 * 2f;
      }
      if ((double) this.NPC.velocity.Y < (double) num18)
      {
        this.NPC.velocity.Y += num6;
        if ((double) this.NPC.velocity.Y < 0.0 && (double) num18 > 0.0)
          this.NPC.velocity.Y += num6 * 2f;
      }
      else if ((double) this.NPC.velocity.Y > (double) num18)
      {
        this.NPC.velocity.Y -= num6;
        if ((double) this.NPC.velocity.Y > 0.0 && (double) num18 < 0.0)
          this.NPC.velocity.Y -= num6 * 2f;
      }
      if (this.NPC.life < this.NPC.lifeMax / 2)
      {
        NPC npc = this.NPC;
        npc.position = Vector2.op_Addition(npc.position, this.NPC.velocity);
      }
      this.NPC.rotation = this.NPC.DirectionTo(Main.player[this.NPC.target].Center).ToRotation() + 1.57079637f;
    }

    public override void OnHitPlayer(Terraria.Player target, Terraria.Player.HurtInfo hurtInfo)
    {
      target.AddBuff(ModContent.BuffType<Gun>(), 600);
    }

    public override void FindFrame(int frameHeight)
    {
      if (this.NPC.life < this.NPC.lifeMax / 2)
      {
        if (this.NPC.frame.Y < 4 * frameHeight)
          this.NPC.frame.Y = 4 * frameHeight;
        if (++this.NPC.frameCounter <= 6.0)
          return;
        this.NPC.frameCounter = 0.0;
        this.NPC.frame.Y += frameHeight;
        if (this.NPC.frame.Y < Main.npcFrameCount[this.NPC.type] * frameHeight)
          return;
        this.NPC.frame.Y = 4 * frameHeight;
      }
      else
      {
        if (this.NPC.frame.Y >= 4 * frameHeight)
          this.NPC.frame.Y = 0;
        if (++this.NPC.frameCounter <= 6.0)
          return;
        this.NPC.frameCounter = 0.0;
        this.NPC.frame.Y += frameHeight;
        if (this.NPC.frame.Y < 4 * frameHeight)
          return;
        this.NPC.frame.Y = 0;
      }
    }

    public override bool CheckActive() => false;

    public override void BossHeadRotation(ref float rotation) => rotation = this.NPC.rotation;

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame1 = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(frame1.Size(), 2f);
      Main.spriteBatch.Draw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(this.NPC.Center, Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame1), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2_1, this.NPC.scale, (SpriteEffects) 0, 0.0f);
      for (int index = 0; index < 200; ++index)
      {
        if (Main.npc[index].active && (double) Main.npc[index].ai[0] == (double) this.NPC.whoAmI && (Main.npc[index].type == ModContent.NPCType<GunCelebration>() || Main.npc[index].type == ModContent.NPCType<GunMagnum>() || Main.npc[index].type == ModContent.NPCType<GunQuad>() || Main.npc[index].type == ModContent.NPCType<GunShotgun>() || Main.npc[index].type == ModContent.NPCType<GunSniper>() || Main.npc[index].type == ModContent.NPCType<GunUzi>()))
        {
          Texture2D texture2D2 = TextureAssets.Npc[Main.npc[index].type].Value;
          Rectangle frame2 = Main.npc[index].frame;
          Vector2 vector2_2 = Vector2.op_Division(frame2.Size(), 2f);
          Main.spriteBatch.Draw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Main.npc[index].Center, Main.screenPosition), new Vector2(0.0f, Main.npc[index].gfxOffY)), new Rectangle?(frame2), Main.npc[index].GetAlpha(drawColor), Main.npc[index].rotation, vector2_2, Main.npc[index].scale, this.NPC.spriteDirection > 0 ? (SpriteEffects) 1 : (SpriteEffects) 0, 0.0f);
        }
      }
      return false;
    }
  }
}
