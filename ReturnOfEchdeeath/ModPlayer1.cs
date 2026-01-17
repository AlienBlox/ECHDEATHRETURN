// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.ModPlayer1
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath
{
  public class ModPlayer1 : ModPlayer
  {
    public static int ceilingIndex = -1;

    public override void PreUpdate()
    {
      if (!this.Player.HasItem(ModContent.ItemType<SoulofCreation>()))
        return;
      this.Player.ghost = false;
      this.Player.statLife = this.Player.statLifeMax2;
      for (int setDefaultsToType = 0; setDefaultsToType < ItemLoader.ItemCount / 2; ++setDefaultsToType)
      {
        Item currentItem = new Item(setDefaultsToType);
        if (currentItem.accessory && currentItem.type != 3536 && currentItem.type != 3537 && currentItem.type != 3538 && currentItem.type != 3539 && currentItem.type != 4054 && currentItem.type != 4318 && currentItem.type != 5347 && currentItem.type != 5113)
          this.Player.ApplyEquipFunctional(currentItem, true);
      }
    }

    public override void PostUpdate()
    {
      if (!this.Player.HasItem(ModContent.ItemType<SoulofCreation>()))
        return;
      this.Player.ghost = false;
      this.Player.statLife = this.Player.statLifeMax2;
      for (int setDefaultsToType = 0; setDefaultsToType < ItemLoader.ItemCount; ++setDefaultsToType)
      {
        Item currentItem = new Item(setDefaultsToType);
        if (currentItem.accessory && currentItem.type != 3536 && currentItem.type != 3537 && currentItem.type != 3538 && currentItem.type != 3539 && currentItem.type != 4054 && currentItem.type != 4318 && currentItem.type != 5347 && currentItem.type != 5113)
          this.Player.ApplyEquipFunctional(currentItem, true);
      }
    }
  }
}
