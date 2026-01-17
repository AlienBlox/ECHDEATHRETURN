// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.PlanteraCurse
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Microsoft.Xna.Framework;
using ReturnOfEchdeeath.NPCs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath
{
  public class PlanteraCurse : ModItem
  {
    public override string Texture => "Terraria/Images/Item_" + (short) 3459.ToString();

    public override void SetStaticDefaults()
    {
      this.DisplayName.Equals((object) "Plantera's Curse");
    }

    public override void SetDefaults()
    {
      this.Item.width = 20;
      this.Item.height = 20;
      this.Item.maxStack = 9999;
      this.Item.value = 10000;
      this.Item.rare = 11;
      this.Item.consumable = true;
      this.Item.useStyle = 4;
    }

    public override void AddRecipes()
    {
      Recipe recipe = this.CreateRecipe();
      recipe.AddIngredient(3459);
      recipe.AddIngredient(3467);
      recipe.AddCondition(Terraria.Condition.NearShimmer);
      recipe.AddTile(412);
      recipe.Register();
    }

    public override bool CanUseItem(Terraria.Player player)
    {
      return !NPC.AnyNPCs(ModContent.NPCType<Guntera>()) && player.ZoneJungle;
    }

    public override bool? UseItem(Terraria.Player player)
    {
      if (player.whoAmI == Main.myPlayer)
      {
        SoundEngine.PlaySound(in SoundID.Roar, new Vector2?(player.position));
        int num = ModContent.NPCType<Guntera>();
        if (Main.netMode != 1)
          NPC.SpawnOnPlayer(player.whoAmI, num);
        else
          NetMessage.SendData(61, number: player.whoAmI, number2: (float) num);
      }
      return new bool?(true);
    }
  }
}
