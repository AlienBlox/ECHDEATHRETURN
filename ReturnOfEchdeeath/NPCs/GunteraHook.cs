// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.GunteraHook
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
  public class GunteraHook : ModNPC
  {
    public override string Texture => "Terraria/Images/NPC_263";

    public override void SetStaticDefaults()
    {
      this.DisplayName.Equals((object) "Guntera's Hook");
      Main.npcFrameCount[this.NPC.type] = Main.npcFrameCount[263];
    }

    public override void SetDefaults()
    {
      this.NPC.width = 40;
      this.NPC.height = 40;
      this.NPC.damage = 400;
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
      this.NPC.dontTakeDamage = true;
    }

    public override void AI()
    {
      this.NPC.timeLeft = 60;
      int index1 = (int) this.NPC.ai[3];
      if (index1 < 0 || index1 >= 200 || !Main.npc[index1].active || Main.npc[index1].type != ModContent.NPCType<Guntera>())
      {
        this.NPC.active = false;
      }
      else
      {
        this.NPC.target = Main.npc[index1].target;
        if (Main.npc[index1].life < Main.npc[index1].lifeMax / 2 && Main.npc[index1].HasValidTarget && (double) this.NPC.Distance(Vector2.op_Addition(Main.player[Main.npc[index1].target].Center, Vector2.op_Multiply(Main.player[this.NPC.target].velocity, 30f))) > 700.0)
        {
          Vector2 vector2 = Vector2.op_Division(Main.player[Main.npc[index1].target].Center, 16f);
          vector2.X += (float) Main.rand.Next(-40, 41);
          vector2.Y += (float) Main.rand.Next(-40, 41);
          Framing.GetTileSafely((int) vector2.X, (int) vector2.Y);
          this.NPC.localAI[0] = 600f;
          if (Main.netMode != 1)
            this.NPC.netUpdate = true;
          this.NPC.ai[0] = vector2.X;
          this.NPC.ai[1] = vector2.Y;
        }
        NPC npc = this.NPC;
        npc.position = Vector2.op_Addition(npc.position, this.NPC.velocity);
        bool flag1 = false;
        bool flag2 = false;
        this.NPC.damage = this.NPC.defDamage;
        this.NPC.defense = this.NPC.defDefense;
        if (Main.player[Main.npc[index1].target].dead)
          flag2 = true;
        if (((index1 != -1 && !Main.player[Main.npc[index1].target].ZoneJungle || (double) Main.player[Main.npc[index1].target].position.Y < Main.worldSurface * 16.0 ? 1 : ((double) Main.player[Main.npc[index1].target].position.Y > (double) ((Main.maxTilesY - 200) * 16) ? 1 : 0)) | (flag2 ? 1 : 0)) != 0)
        {
          this.NPC.localAI[0] -= 4f;
          flag1 = true;
          this.NPC.damage = this.NPC.defDamage * 10;
          this.NPC.defense = this.NPC.defDefense * 10;
        }
        if (Main.netMode == 1)
        {
          if ((double) this.NPC.ai[0] == 0.0)
            this.NPC.ai[0] = (float) (int) ((double) this.NPC.Center.X / 16.0);
          if ((double) this.NPC.ai[1] == 0.0)
            this.NPC.ai[1] = (float) (int) ((double) this.NPC.Center.X / 16.0);
        }
        if (Main.netMode != 1)
        {
          if ((double) this.NPC.ai[0] == 0.0 || (double) this.NPC.ai[1] == 0.0)
            this.NPC.localAI[0] = 0.0f;
          --this.NPC.localAI[0];
          if (Main.npc[index1].life < Main.npc[index1].lifeMax / 2)
            this.NPC.localAI[0] -= 2f;
          if (Main.npc[index1].life < Main.npc[index1].lifeMax / 4)
            this.NPC.localAI[0] -= 2f;
          if (flag1)
            this.NPC.localAI[0] -= 6f;
          if (!flag2 && (double) this.NPC.localAI[0] <= 0.0 && (double) this.NPC.ai[0] != 0.0)
          {
            for (int index2 = 0; index2 < 200; ++index2)
            {
              if (index2 != this.NPC.whoAmI && Main.npc[index2].active && Main.npc[index2].type == this.NPC.type && ((double) Main.npc[index2].velocity.X != 0.0 || (double) Main.npc[index2].velocity.Y != 0.0))
                this.NPC.localAI[0] = (float) Main.rand.Next(60, 300);
            }
          }
          if ((double) this.NPC.localAI[0] <= 0.0)
          {
            this.NPC.localAI[0] = (float) Main.rand.Next(300, 600);
            bool flag3 = false;
            int num1 = 0;
            while (!flag3 && num1 <= 1000)
            {
              ++num1;
              int num2 = (int) ((double) Main.player[Main.npc[index1].target].Center.X / 16.0);
              int num3 = (int) ((double) Main.player[Main.npc[index1].target].Center.Y / 16.0);
              if ((double) this.NPC.ai[0] == 0.0)
              {
                num2 = (int) (((double) Main.player[Main.npc[index1].target].Center.X + (double) Main.npc[index1].Center.X) / 32.0);
                num3 = (int) (((double) Main.player[Main.npc[index1].target].Center.Y + (double) Main.npc[index1].Center.Y) / 32.0);
              }
              if (flag2)
              {
                num2 = (int) Main.npc[index1].position.X / 16;
                num3 = (int) ((double) Main.npc[index1].position.Y + 400.0) / 16;
              }
              int num4 = 20 + (int) (100.0 * ((double) num1 / 1000.0));
              int i = num2 + Main.rand.Next(-num4, num4 + 1);
              int j = num3 + Main.rand.Next(-num4, num4 + 1);
              if (Main.npc[index1].life < Main.npc[index1].lifeMax / 2 && Main.rand.Next(6) == 0)
              {
                this.NPC.TargetClosest();
                double num5 = (double) Main.player[this.NPC.target].Center.X / 16.0;
                double num6 = (double) Main.player[this.NPC.target].Center.Y / 16.0;
              }
              try
              {
                if (!WorldGen.SolidTile(i, j))
                {
                  if (Main.npc[index1].life >= Main.npc[index1].lifeMax / 2)
                    continue;
                }
                flag3 = true;
                this.NPC.ai[0] = (float) i;
                this.NPC.ai[1] = (float) j;
                this.NPC.netUpdate = true;
              }
              catch
              {
              }
            }
          }
        }
        if ((double) this.NPC.ai[0] <= 0.0 || (double) this.NPC.ai[1] <= 0.0)
          return;
        float num7 = 6f;
        if (Main.npc[index1].life < Main.npc[index1].lifeMax / 2)
          num7 = 8f;
        if (Main.npc[index1].life < Main.npc[index1].lifeMax / 4)
          num7 = 10f;
        if (Main.expertMode)
          ++num7;
        if (Main.expertMode && Main.npc[index1].life < Main.npc[index1].lifeMax / 2)
          ++num7;
        if (flag1)
          num7 *= 2f;
        if (flag2)
          num7 *= 2f;
        Vector2 center1 = this.NPC.Center;
        float num8 = (float) ((double) this.NPC.ai[0] * 16.0 - 8.0) - center1.X;
        float num9 = (float) ((double) this.NPC.ai[1] * 16.0 - 8.0) - center1.Y;
        float num10 = (float) Math.Sqrt((double) num8 * (double) num8 + (double) num9 * (double) num9);
        if ((double) num10 < 12.0 + (double) num7)
        {
          this.NPC.velocity.X = num8;
          this.NPC.velocity.Y = num9;
        }
        else
        {
          float num11 = num7 / num10;
          this.NPC.velocity.X = num8 * num11;
          this.NPC.velocity.Y = num9 * num11;
        }
        Vector2 center2 = this.NPC.Center;
        float x = Main.npc[index1].Center.X - center2.X;
        this.NPC.rotation = (float) Math.Atan2((double) Main.npc[index1].Center.Y - (double) center2.Y, (double) x) - 1.57f;
      }
    }

    public override void FindFrame(int frameHeight)
    {
      if ((double) this.NPC.velocity.X == 0.0 && (double) this.NPC.velocity.Y == 0.0)
        this.NPC.frame.Y = 0;
      else
        this.NPC.frame.Y = frameHeight;
    }

    public override void OnHitPlayer(Terraria.Player target, Terraria.Player.HurtInfo hurtInfo)
    {
      target.AddBuff(ModContent.BuffType<Gun>(), 600);
    }

    public override bool CheckActive() => false;

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      if ((double) this.NPC.ai[3] > -1.0 && (double) this.NPC.ai[3] < 200.0 && Main.npc[(int) this.NPC.ai[3]].active && Main.npc[(int) this.NPC.ai[3]].type == ModContent.NPCType<Guntera>())
      {
        Texture2D texture2D = TextureAssets.Chain26.Value;
        Vector2 vector2_1 = this.NPC.Center;
        Vector2 center = Main.npc[(int) this.NPC.ai[3]].Center;
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
