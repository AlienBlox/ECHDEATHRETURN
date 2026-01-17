using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ECHDEATHRETURN.NPCs;

[AutoloadBossHead]
public class Echdeath : ModNPC
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Echdeath");
		Main.npcFrameCount[NPC.type] = 11;
	}

	public override void SetDefaults()
	{
		NPC.width = 60;
		NPC.height = 60;
		NPC.damage = 214748364;
		NPC.defense = 214748364;
		NPC.lifeMax = 214748364;
		NPC.HitSound = SoundID.NPCHit57;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.knockBackResist = 0f;
		NPC.lavaImmune = true;
		NPC.aiStyle = -1;
		NPC.boss = true;
	}

    public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    {
		NPC.damage = 214748364;
		NPC.lifeMax = 214748364;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		cooldownSlot = 1;
		return true;
	}

	public override void AI()
	{
		if (!NPC.HasValidTarget)
		{
			NPC.TargetClosest();
		}
		NPC.damage = NPC.defDamage;
		NPC.defense = NPC.defDefense;
		NPC.ai[0] += 0.05f;
		if (NPC.HasValidTarget)
		{
			Player player = Main.player[NPC.target];
			NPC.direction = (NPC.spriteDirection = ((NPC.Center.X < player.Center.X) ? 1 : (-1)));
			if (NPC.ai[1] == 1f)
			{
				NPC.position += (player.position - player.oldPosition) * 0.25f;
			}
			NPC.velocity = NPC.DirectionTo(player.Center) * NPC.ai[0];
			if (NPC.velocity.Length() > NPC.Distance(player.Center))
			{
				NPC.Center = player.Center;
			}
			if (NPC.timeLeft < 600)
			{
				NPC.timeLeft = 600;
			}
		}
		else if (NPC.timeLeft > 60)
		{
			NPC.timeLeft = 60;
		}
		NPC.scale = 1f + NPC.ai[0] / 4f;
		if (Main.netMode != NetmodeID.MultiplayerClient)
		{
			int num = (int)(40f * NPC.scale);
			if (NPC.ai[1] == 1f)
			{
				for (int i = -num / 2; i <= num / 2; i += 8)
				{
					for (int j = -num / 2; j <= num / 2; j += 8)
					{
						int num2 = (int)(NPC.Center.X + (float)i) / 16;
						int num3 = (int)(NPC.Center.Y + (float)j) / 16;
						if (num2 <= -1 || num2 >= Main.maxTilesX || num3 <= -1 || num3 >= Main.maxTilesY)
						{
							continue;
						}
						Tile tileSafely = Framing.GetTileSafely(num2, num3);
						if (tileSafely.TileType != TileID.Dirt || tileSafely.BlockType != BlockType.Solid || tileSafely.WallType != WallID.None)
						{
							WorldGen.KillTile(num2, num3, fail: false, effectOnly: false, noItem: true);
							WorldGen.KillWall(num2, num3);
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, num2, num3);
							}
						}
					}
				}
			}
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && Main.npc[k].type != NPC.type && NPC.Distance(Main.npc[k].Center) < (float)(num / 2))
				{
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText(":echdeath:", Color.Red);
					}

                    NPC.HitInfo H = new()
                    {
                        Damage = NPC.damage
                    };

                    Main.npc[k].StrikeNPC(H);

					for (int l = 0; l < 100; l++)
					{
						CombatText.NewText(Main.npc[k].Hitbox, Color.Red, Main.rand.Next(NPC.damage), dramatic: true);
					}
				}
			}
		}
		if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && NPC.Hitbox.Intersects(Main.LocalPlayer.Hitbox))
		{
			Main.NewText(":echdeath:", Color.Red);
			Main.LocalPlayer.ResetEffects();
			Main.LocalPlayer.ghost = true;
			Main.LocalPlayer.KillMe(PlayerDeathReason.ByNPC(NPC.whoAmI), NPC.damage, 0);
			for (int m = 0; m < 100; m++)
			{
				CombatText.NewText(Main.LocalPlayer.Hitbox, Color.Red, Main.rand.Next(NPC.damage), dramatic: true);
			}
		}
		if (NPC.ai[1] == 1f)
		{
			if (NPC.localAI[0] == 0f)
			{
				Main.NewText("Echdeath has enraged.", Color.DarkRed);
				NPC.localAI[0] = 1f;
				for (int n = 0; n < NPC.buffImmune.Length; n++)
				{
					NPC.buffImmune[n] = true;
				}
			}
			while (NPC.buffType[0] != 0)
			{
				NPC.DelBuff(0);
			}
			if (NPC.ai[2] == 0f)
			{
				if (NPC.life == NPC.lifeMax)
				{
					NPC.ai[2] = 1f;
				}
				NPC.life = NPC.lifeMax;
			}
		}
		else if (NPC.ai[0] > 30f)
		{
			NPC.ai[0] = 30f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		if ((NPC.frameCounter += 1.0) > (double)(34f - NPC.ai[0]))
		{
			NPC.frameCounter = 0.0;
			NPC.frame.Y += frameHeight;
			if (NPC.frame.Y >= Main.npcFrameCount[NPC.type] * frameHeight)
			{
				NPC.frame.Y = 0;
			}
		}
	}

    public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
        if (target.active && !target.dead && !target.ghost)
        {
            Main.NewText(":echdeath:", Color.Red);
            target.ResetEffects();
            target.ghost = true;
            target.KillMe(PlayerDeathReason.ByNPC(NPC.whoAmI), NPC.damage, 0);
            for (int i = 0; i < 100; i++)
            {
                CombatText.NewText(target.Hitbox, Color.Red, Main.rand.Next(NPC.damage), dramatic: true);
            }
        }
    }

	public override bool CheckDead()
	{
		if (NPC.ai[1] == 1f && NPC.ai[2] == 1f)
		{
			return true;
		}
		NPC.active = true;
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			NPC.life = 1;
		}
		else
		{
			NPC.life = NPC.lifeMax;
			NPC.ai[1] = 1f;
		}
		return false;
	}

	public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
	{
		spriteEffects = ((NPC.spriteDirection < 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Texture2D value = ModContent.Request<Texture2D>(Texture).Value;
		Rectangle frame = NPC.frame;
		Vector2 origin = frame.Size() / 2f;
		//Color newColor = drawColor;
		//newColor = NPC.GetAlpha(newColor);
		SpriteEffects effects = ((NPC.spriteDirection < 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		Main.spriteBatch.Draw(value, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), frame, NPC.GetAlpha(drawColor), NPC.rotation, origin, NPC.scale, effects, 0f);
		return false;
	}
}
