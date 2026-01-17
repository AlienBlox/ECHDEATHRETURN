// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.CeilingOfMoonLordEye
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
  public class CeilingOfMoonLordEye : ModNPC
  {
    public override void SetStaticDefaults()
    {
      this.DisplayName.Equals((object) "Ceiling of Moon Lord");
    }

    public override void SetDefaults()
    {
      this.NPC.width = 74;
      this.NPC.height = 74;
      this.NPC.damage = 125;
      this.NPC.defense = 350;
      this.NPC.lifeMax = 325000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit57);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath11);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.buffImmune[68] = true;
      this.NPC.buffImmune[31] = true;
      this.NPC.aiStyle = -1;
    }

    public override void AI()
    {
      this.NPC.timeLeft = 60;
      int index1 = (int) this.NPC.ai[0];
      if (index1 < 0 || index1 >= 200 || !Main.npc[index1].active || Main.npc[index1].type != ModContent.NPCType<CeilingOfMoonLord>())
      {
        this.NPC.active = false;
      }
      else
      {
        this.NPC.realLife = index1;
        this.NPC.target = Main.npc[this.NPC.realLife].target;
        this.NPC.direction = this.NPC.spriteDirection = (int) this.NPC.ai[1];
        this.NPC.Center = Main.npc[this.NPC.realLife].Center;
        this.NPC.position.X += 115f * this.NPC.ai[1];
        int num1 = 5;
        this.NPC.localAI[1] += 1.5f;
        if ((double) Main.npc[this.NPC.realLife].life < (double) Main.npc[this.NPC.realLife].lifeMax * 0.75)
        {
          ++this.NPC.localAI[1];
          ++num1;
        }
        if ((double) Main.npc[this.NPC.realLife].life < (double) Main.npc[this.NPC.realLife].lifeMax * 0.5)
        {
          ++this.NPC.localAI[1];
          ++num1;
        }
        if ((double) Main.npc[this.NPC.realLife].life < (double) Main.npc[this.NPC.realLife].lifeMax * 0.25)
        {
          ++this.NPC.localAI[1];
          num1 += 2;
        }
        if ((double) Main.npc[this.NPC.realLife].life < (double) Main.npc[this.NPC.realLife].lifeMax * 0.1)
        {
          this.NPC.localAI[1] += 4f;
          num1 += 6;
        }
        if ((double) this.NPC.localAI[2] == 0.0)
        {
          if ((double) this.NPC.localAI[1] > 600.0)
          {
            this.NPC.localAI[1] = 0.0f;
            this.NPC.localAI[2] = 1f;
          }
        }
        else if ((double) this.NPC.localAI[1] > 45.0)
        {
          this.NPC.localAI[1] = 0.0f;
          float[] localAi = this.NPC.localAI;
          int index2 = 2;
          float num2 = localAi[index2] + 1f;
          localAi[index2] = num2;
          if ((double) num2 >= (double) num1)
            this.NPC.localAI[2] = 0.0f;
          if (Main.netMode != 1)
          {
            Vector2 velocity = Vector2.op_Multiply(9f, this.NPC.DirectionTo(Vector2.op_Addition(Main.player[this.NPC.target].Center, Vector2.op_Multiply(Main.player[this.NPC.target].velocity, 15f))));
            Projectile.NewProjectile(Terraria.Entity.GetSource_None(), this.NPC.Center, velocity, 462, this.NPC.damage / 6, 0.0f, Main.myPlayer);
          }
        }
        float[] localAi1 = this.NPC.localAI;
        int index3 = 0;
        float num3 = localAi1[index3] + 1f;
        localAi1[index3] = num3;
        if ((double) num3 >= 300.0)
        {
          if ((double) this.NPC.localAI[0] % 20.0 == 0.0 && Main.netMode != 1)
            Projectile.NewProjectile(Terraria.Entity.GetSource_None(), this.NPC.Center, Vector2.Zero, ModContent.ProjectileType<CeilingSphere>(), this.NPC.damage / 6, 0.0f, Main.myPlayer, (float) this.NPC.target);
          if ((double) this.NPC.localAI[0] <= 420.0)
            return;
          this.NPC.localAI[0] = 0.0f;
        }
        else
        {
          if (Main.npc[this.NPC.realLife].life >= Main.npc[this.NPC.realLife].lifeMax / 2)
            return;
          ++this.NPC.localAI[0];
        }
      }
    }

    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
      return new bool?(false);
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
  }
}
