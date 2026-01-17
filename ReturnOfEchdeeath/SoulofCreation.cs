// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.SoulofCreation
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath
{
  public class SoulofCreation : ModItem
  {
    public override string Texture => "Terraria/Images/Item_" + (short) 3456.ToString();

    public override void SetStaticDefaults()
    {
      this.DisplayName.Equals((object) "Soul of Creation");
    }

    public override void SetDefaults()
    {
      this.Item.width = 20;
      this.Item.height = 20;
      this.Item.maxStack = 1;
      this.Item.value = 1000000;
      this.Item.rare = -12;
    }

    public override void AddRecipes()
    {
      Recipe recipe = this.CreateRecipe();
      recipe.AddIngredient(3456, 100);
      recipe.AddIngredient(3467, 1000);
      recipe.AddCondition(Terraria.Condition.NearShimmer);
      recipe.AddTile(412);
      recipe.Register();
    }
  }
}
