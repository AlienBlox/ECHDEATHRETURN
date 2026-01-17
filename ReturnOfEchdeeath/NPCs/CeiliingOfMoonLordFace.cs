// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.CeilingOfMoonLordFace
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath.NPCs
{
  public class CeilingOfMoonLordFace : ModNPC
  {
    public override void SetStaticDefaults()
    {
      this.DisplayName.Equals((object) "Ceiling of Moon Lord");
    }

    public override void SetDefaults()
    {
      this.NPC.width = 274;
      this.NPC.height = 122;
      this.NPC.damage = 125;
      this.NPC.defense = 350;
      this.NPC.lifeMax = 325000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit57);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath11);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      for (int index = 0; index < this.NPC.buffImmune.Length; ++index)
        this.NPC.buffImmune[index] = true;
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
        this.NPC.position.Y += 100f;
        if (Main.npc[this.NPC.realLife].life < Main.npc[this.NPC.realLife].lifeMax / 2)
          ++this.NPC.localAI[0];
        float[] localAi = this.NPC.localAI;
        int index2 = 0;
        float num = localAi[index2] + 1f;
        localAi[index2] = num;
        if ((double) num <= 660.0)
          return;
        this.NPC.localAI[0] = 0.0f;
        if (Main.netMode == 1)
          return;
        double x1 = (double) Main.player[this.NPC.target].Center.X;
        double x2 = (double) this.NPC.Center.X;
        Projectile.NewProjectile(this.NPC.GetSource_FromAI(), Vector2.op_Addition(this.NPC.Center, Main.rand.NextVector2Square(0.0f, (float) this.NPC.width)), Vector2.op_Multiply(Vector2.UnitX.RotatedByRandom(Math.PI), 6f), ModContent.ProjectileType<CeilingDeathray>(), this.NPC.damage / 4, 0.0f, Main.myPlayer);
      }
    }

    public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
    {
      projectile.timeLeft = 0;
    }

    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
      return new bool?(false);
    }

    public override bool CheckActive() => false;

    public override Color? GetAlpha(Color drawColor) => new Color?(Color.White);
  }
}
