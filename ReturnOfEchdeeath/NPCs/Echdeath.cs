// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.NPCs.Echdeath
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath.NPCs
{
  [AutoloadBossHead]
  public class Echdeath : ModNPC
  {
    public override void SetStaticDefaults()
    {
      this.DisplayName.Equals((object) nameof (Echdeath));
      Main.npcFrameCount[this.Type] = 11;
    }

    public override void SetDefaults()
    {
      this.NPC.width = 60;
      this.NPC.height = 60;
      this.NPC.damage = 214748364;
      this.NPC.defense = 214748364;
      this.NPC.lifeMax = 214748364;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit57);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.boss = true;
    }

    public override bool CanHitPlayer(Terraria.Player target, ref int cooldownSlot)
    {
      cooldownSlot = 1;
      return true;
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
      npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulofCreation>()));
    }

    public override void AI()
    {
      if (!this.NPC.HasValidTarget)
        this.NPC.TargetClosest();
      this.NPC.damage = this.NPC.defDamage;
      this.NPC.defense = this.NPC.defDefense;
      this.NPC.ai[0] += 0.05f;
      if (this.NPC.HasValidTarget)
      {
        Terraria.Player player = Main.player[this.NPC.target];
        this.NPC.direction = this.NPC.spriteDirection = (double) this.NPC.Center.X < (double) player.Center.X ? 1 : -1;
        if ((double) this.NPC.ai[1] == 1.0)
        {
          NPC npc = this.NPC;
          npc.position = Vector2.op_Addition(npc.position, Vector2.op_Multiply(Vector2.op_Subtraction(player.position, player.oldPosition), 0.25f));
        }
        this.NPC.velocity = Vector2.op_Multiply(this.NPC.DirectionTo(player.Center), this.NPC.ai[0]);
        if ((double) ((Vector2) ref this.NPC.velocity).Length() > (double) this.NPC.Distance(player.Center))
          this.NPC.Center = player.Center;
        if (this.NPC.timeLeft < 600)
          this.NPC.timeLeft = 600;
      }
      else if (this.NPC.timeLeft > 60)
        this.NPC.timeLeft = 60;
      this.NPC.scale = (float) (1.0 + (double) this.NPC.ai[0] / 4.0);
      if (Main.netMode != 1)
      {
        int num1 = (int) (40.0 * (double) this.NPC.scale);
        if ((double) this.NPC.ai[1] == 1.0)
        {
          for (int index1 = -num1 / 2; index1 <= num1 / 2; index1 += 8)
          {
            for (int index2 = -num1 / 2; index2 <= num1 / 2; index2 += 8)
            {
              int num2 = (int) ((double) this.NPC.Center.X + (double) index1) / 16;
              int num3 = (int) ((double) this.NPC.Center.Y + (double) index2) / 16;
              if (num2 > -1 && num2 < Main.maxTilesX && num3 > -1 && num3 < Main.maxTilesY)
              {
                Tile tileSafely = Framing.GetTileSafely(num2, num3);
                if (tileSafely.TileType == (ushort) 0)
                {
                  int blockType = (int) tileSafely.BlockType;
                }
                WorldGen.KillTile(num2, num3, noItem: true);
                WorldGen.KillWall(num2, num3);
                if (Main.netMode == 2)
                  NetMessage.SendData(17, number2: (float) num2, number3: (float) num3);
              }
            }
          }
        }
        for (int index3 = 0; index3 < 200; ++index3)
        {
          if (Main.npc[index3].active && Main.npc[index3].type != this.NPC.type && (double) this.NPC.Distance(Main.npc[index3].Center) < (double) (num1 / 2))
          {
            if (Main.netMode == 0)
              Main.NewText((object) ":echdeath:", new Color?(Color.Red));
            for (int index4 = 0; index4 < 100; ++index4)
              CombatText.NewText(Main.npc[index3].Hitbox, Color.Red, Main.rand.Next(this.NPC.damage), true);
          }
        }
      }
      if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
      {
        Rectangle hitbox = this.NPC.Hitbox;
        if (((Rectangle) ref hitbox).Intersects(Main.LocalPlayer.Hitbox))
        {
          Main.NewText((object) ":echdeath:", new Color?(Color.Red));
          Main.LocalPlayer.ResetEffects();
          Main.LocalPlayer.ghost = true;
          Main.LocalPlayer.KillMe(PlayerDeathReason.ByNPC(this.NPC.whoAmI), (double) this.NPC.damage, 0);
          for (int index = 0; index < 100; ++index)
            CombatText.NewText(Main.LocalPlayer.Hitbox, Color.Red, Main.rand.Next(this.NPC.damage), true);
        }
      }
      if ((double) this.NPC.ai[1] == 1.0)
      {
        if ((double) this.NPC.localAI[0] == 0.0)
        {
          Main.NewText((object) "Echdeath has enraged.", new Color?(Color.DarkRed));
          this.NPC.localAI[0] = 1f;
          for (int index = 0; index < this.NPC.buffImmune.Length; ++index)
            this.NPC.buffImmune[index] = true;
        }
        while (this.NPC.buffType[0] != 0)
          this.NPC.DelBuff(0);
        if ((double) this.NPC.ai[2] != 0.0)
          return;
        if (this.NPC.life == this.NPC.lifeMax)
          this.NPC.ai[2] = 1f;
        this.NPC.life = this.NPC.lifeMax;
      }
      else
      {
        if ((double) this.NPC.ai[0] <= 30.0)
          return;
        this.NPC.ai[0] = 30f;
      }
    }

    public override void FindFrame(int frameHeight)
    {
      if (++this.NPC.frameCounter <= 34.0 - (double) this.NPC.ai[0])
        return;
      this.NPC.frameCounter = 0.0;
      this.NPC.frame.Y += frameHeight;
      if (this.NPC.frame.Y < Main.npcFrameCount[this.NPC.type] * frameHeight)
        return;
      this.NPC.frame.Y = 0;
    }

    public override void ModifyHitPlayer(Terraria.Player target, ref Terraria.Player.HurtModifiers modifiers)
    {
      if (!target.active || target.dead || target.ghost)
        return;
      Main.NewText((object) ":echdeath:", new Color?(Color.Red));
      target.ResetEffects();
      target.ghost = true;
      target.KillMe(PlayerDeathReason.ByNPC(this.NPC.whoAmI), (double) this.NPC.damage, 0);
      for (int index = 0; index < 100; ++index)
        CombatText.NewText(target.Hitbox, Color.Red, Main.rand.Next(this.NPC.damage), true);
    }

    public override bool CheckDead()
    {
      if ((double) this.NPC.ai[1] == 1.0 && (double) this.NPC.ai[2] == 1.0)
        return true;
      this.NPC.active = true;
      if (Main.netMode == 1)
      {
        this.NPC.life = 1;
      }
      else
      {
        this.NPC.life = this.NPC.lifeMax;
        this.NPC.ai[1] = 1f;
      }
      return false;
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = TextureAssets.Npc[this.Type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2 = Vector2.op_Division(frame.Size(), 2f);
      this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = this.NPC.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(this.NPC.Center, Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
