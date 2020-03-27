using RimWorld.Planet;
using System;
using Verse;

namespace MoreSteel
{
  internal class MoreSteelWorldComp : WorldComponent
  {
    public MoreSteelWorldComp(World world)
      : base(world)
    {}

    public override void FinalizeInit()
    {
      base.FinalizeInit();

      MoreSteelModSettings.OriginalSteelAmount = DefDatabase<ThingDef>.GetNamed("MineableSteel").building.mineableYield;
      UpdateAllChanges();
    }

    public void UpdateAllChanges()
    {
      DefDatabase<ThingDef>.GetNamed("MineableSteel").building.mineableYield = (int)Math.Floor(MoreSteelModSettings.OriginalSteelAmount * MoreSteelModSettings.multiplyMS);
    }
  }
}
