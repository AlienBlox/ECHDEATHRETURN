// Decompiled with JetBrains decompiler
// Type: ReturnOfEchdeeath.RandomSystem
// Assembly: ReturnOfEchdeeath, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 43C12AAC-186F-415E-B87B-F1128F18545F
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModReader\ReturnOfEchdeeath\ReturnOfEchdeeath.dll

using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace ReturnOfEchdeeath
{
  public static class RandomSystem
  {
    public static string TooltipRandom()
    {
      for (int index = 0; index < ItemLoader.ItemCount; ++index)
      {
        Item obj = new Item(Main.rand.Next(1, ItemLoader.ItemCount + 1));
        if (obj.accessory && obj.type != 3536 && obj.type != 3537 && obj.type != 3538 && obj.type != 3539 && obj.type != 4054 && obj.type != 4318 && obj.type != 5347 && obj.type != 5113)
        {
          string str = obj.AffixName();
          Random random = new Random();
          string[] strArray = new string[2]
          {
            "Effects of " + str.ToString(),
            "Effects of " + str.ToString()
          };
          return strArray[random.Next(0, strArray.Length)];
        }
      }
      return string.Empty;
    }
  }
}
