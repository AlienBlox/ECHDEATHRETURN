// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.GunteraTentacle
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
  public class GunteraTentacle : ModNPC
  {
    public override string Texture => "Terraria/Images/NPC_264";

    public override void SetStaticDefaults()
    {
      this.DisplayName.Equals((object) "Guntera's Tentacle");
      Main.npcFrameCount[this.NPC.type] = Main.npcFrameCount[264];
    }

    public override void SetDefaults()
    {
      this.NPC.width = 24;
      this.NPC.height = 24;
      this.NPC.damage = 400;
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
    }

    public override bool CanHitPlayer(Terraria.Player target, ref int cooldownSlot)
    {
      cooldownSlot = 0;
      return true;
    }

    public override void AI()
    {
      this.NPC.timeLeft = 60;
      int index1 = (int) this.NPC.ai[2];
      if (index1 < 0 || index1 >= 200 || !Main.npc[index1].active || Main.npc[index1].type != ModContent.NPCType<GunteraHook>())
      {
        this.NPC.active = false;
      }
      else
      {
        NPC npc = this.NPC;
        npc.position = Vector2.op_Addition(npc.position, Main.npc[index1].velocity);
        this.NPC.damage = this.NPC.defDamage;
        this.NPC.defense = this.NPC.defDefense;
        int index2 = (int) this.NPC.ai[3];
        if (Main.netMode != 1)
        {
          --this.NPC.localAI[0];
          if ((double) this.NPC.localAI[0] <= 0.0)
          {
            this.NPC.localAI[0] = (float) Main.rand.Next(120, 480);
            this.NPC.ai[0] = (float) Main.rand.Next(-100, 101);
            this.NPC.ai[1] = (float) Main.rand.Next(-100, 101);
            this.NPC.netUpdate = true;
          }
        }
        this.NPC.TargetClosest();
        float num1 = 0.2f;
        float num2 = 200f;
        if ((double) Main.npc[index2].life < (double) Main.npc[index2].lifeMax * 0.25)
          num2 += 100f;
        if ((double) Main.npc[index2].life < (double) Main.npc[index2].lifeMax * 0.1)
          num2 += 100f;
        if (Main.expertMode)
        {
          float num3 = (float) (1.0 - (double) this.NPC.life / (double) this.NPC.lifeMax);
          num2 += num3 * 300f;
          num1 += 0.3f;
        }
        float num4 = Main.npc[index1].position.X + (float) (Main.npc[index1].width / 2);
        float num5 = Main.npc[index1].position.Y + (float) (Main.npc[index1].height / 2);
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(num4, num5);
        float num6 = num4 + this.NPC.ai[0];
        double num7 = (double) num5 + (double) this.NPC.ai[1];
        float num8 = num6 - vector2.X;
        double y1 = (double) vector2.Y;
        float num9 = (float) (num7 - y1);
        float num10 = (float) Math.Sqrt((double) num8 * (double) num8 + (double) num9 * (double) num9);
        float num11 = num2 / num10;
        float x = num8 * num11;
        float y2 = num9 * num11;
        if ((double) this.NPC.position.X < (double) num4 + (double) x)
        {
          this.NPC.velocity.X += num1;
          if ((double) this.NPC.velocity.X < 0.0 && (double) x > 0.0)
            this.NPC.velocity.X *= 0.9f;
        }
        else if ((double) this.NPC.position.X > (double) num4 + (double) x)
        {
          this.NPC.velocity.X -= num1;
          if ((double) this.NPC.velocity.X > 0.0 && (double) x < 0.0)
            this.NPC.velocity.X *= 0.9f;
        }
        if ((double) this.NPC.position.Y < (double) num5 + (double) y2)
        {
          this.NPC.velocity.Y += num1;
          if ((double) this.NPC.velocity.Y < 0.0 && (double) y2 > 0.0)
            this.NPC.velocity.Y *= 0.9f;
        }
        else if ((double) this.NPC.position.Y > (double) num5 + (double) y2)
        {
          this.NPC.velocity.Y -= num1;
          if ((double) this.NPC.velocity.Y > 0.0 && (double) y2 < 0.0)
            this.NPC.velocity.Y *= 0.9f;
        }
        if ((double) this.NPC.velocity.X > 8.0)
          this.NPC.velocity.X = 8f;
        if ((double) this.NPC.velocity.X < -8.0)
          this.NPC.velocity.X = -8f;
        if ((double) this.NPC.velocity.Y > 8.0)
          this.NPC.velocity.Y = 8f;
        if ((double) this.NPC.velocity.Y < -8.0)
          this.NPC.velocity.Y = -8f;
        if ((double) x > 0.0)
        {
          this.NPC.spriteDirection = 1;
          this.NPC.rotation = (float) Math.Atan2((double) y2, (double) x);
        }
        if ((double) x >= 0.0)
          return;
        this.NPC.spriteDirection = -1;
        this.NPC.rotation = (float) Math.Atan2((double) y2, (double) x) + 3.14f;
        if (!Main.player[this.NPC.target].ZoneJungle || (double) Main.player[this.NPC.target].position.Y < Main.worldSurface * 16.0 || (double) Main.player[this.NPC.target].position.Y > (double) ((Main.maxTilesY - 200) * 16))
        {
          this.NPC.damage = this.NPC.defDamage * 10;
          this.NPC.defense = this.NPC.defDefense * 10;
        }
        ++this.NPC.localAI[1];
        if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.9 || (double) Main.npc[index1].life < (double) Main.npc[index1].lifeMax * 0.9)
          ++this.NPC.localAI[1];
        ++this.NPC.localAI[1];
        if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.8 || (double) Main.npc[index1].life < (double) Main.npc[index1].lifeMax * 0.8)
          ++this.NPC.localAI[1];
        ++this.NPC.localAI[1];
        if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.7 || (double) Main.npc[index1].life < (double) Main.npc[index1].lifeMax * 0.7)
          ++this.NPC.localAI[1];
        ++this.NPC.localAI[1];
        if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.6 || (double) Main.npc[index1].life < (double) Main.npc[index1].lifeMax * 0.6)
          ++this.NPC.localAI[1];
        ++this.NPC.localAI[1];
        if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.5 || (double) Main.npc[index1].life < (double) Main.npc[index1].lifeMax * 0.5)
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
        if (Main.netMode == 1)
          return;
        float num12 = Main.rand.NextFloat(2f, 20f);
        Projectile.NewProjectile(Terraria.Entity.GetSource_None(), this.NPC.Center, Vector2.op_Multiply(this.NPC.DirectionTo(Main.player[this.NPC.target].Center), num12), ModContent.ProjectileType<GunteraBullet>(), this.NPC.damage / 4, 0.0f, Main.myPlayer, this.NPC.Distance(Main.player[this.NPC.target].Center) / num12);
      }
    }

    public override void FindFrame(int frameHeight)
    {
      ++this.NPC.frameCounter;
      if (this.NPC.frameCounter >= 6.0)
      {
        this.NPC.frame.Y += frameHeight;
        this.NPC.frameCounter = 0.0;
      }
      if (this.NPC.frame.Y < frameHeight * Main.npcFrameCount[this.NPC.type])
        return;
      this.NPC.frame.Y = 0;
    }

    public override void OnHitPlayer(Terraria.Player target, Terraria.Player.HurtInfo hurtInfo)
    {
      target.AddBuff(ModContent.BuffType<Gun>(), 600);
    }

    public override bool CheckActive() => false;

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      if ((double) this.NPC.ai[2] > -1.0 && (double) this.NPC.ai[2] < 200.0 && Main.npc[(int) this.NPC.ai[2]].active && Main.npc[(int) this.NPC.ai[2]].type == ModContent.NPCType<GunteraHook>())
      {
        Texture2D texture2D = TextureAssets.Chain27.Value;
        Vector2 vector2_1 = this.NPC.Center;
        Vector2 center = Main.npc[(int) this.NPC.ai[2]].Center;
        Rectangle? nullable = new Rectangle?();
        Vector2 vector2_2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_2).\u002Ector((float) texture2D.Width * 0.5f, (float) texture2D.Height * 0.5f);
        float height = (float) texture2D.Height;
        Vector2 vector2_3 = Vector2.op_Subtraction(center, vector2_1);
        float num = (float) Math.Atan2((double) vector2_3.Y, (double) vector2_3.X) - 1.57f;
        bool flag = true;
        if (float.IsNaN(vector2_1.X) && float.IsNaN(vector2_1.Y))
          flag = false;
        if (float.IsNaN(vector2_3.X) && float.IsNaN(vector2_3.Y))
          flag = false;
        while (flag)
        {
          if ((double) ((Vector2) ref vector2_3).Length() < (double) height + 1.0)
          {
            flag = false;
          }
          else
          {
            Vector2 vector2_4 = vector2_3;
            ((Vector2) ref vector2_4).Normalize();
            vector2_1 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(vector2_4, height));
            vector2_3 = Vector2.op_Subtraction(center, vector2_1);
            Color alpha = this.NPC.GetAlpha(Lighting.GetColor((int) vector2_1.X / 16, (int) ((double) vector2_1.Y / 16.0)));
            Main.spriteBatch.Draw(texture2D, Vector2.op_Subtraction(vector2_1, Main.screenPosition), nullable, alpha, num, vector2_2, 1f, (SpriteEffects) 0, 0.0f);
          }
        }
      }
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2 = Vector2.op_Division(frame.Size(), 2f);
      Main.spriteBatch.Draw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(this.NPC.Center, Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2, this.NPC.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
