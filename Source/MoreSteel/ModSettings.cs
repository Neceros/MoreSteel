using System;
using UnityEngine;
using Verse;

namespace MoreSteel
{
  public class MoreSteelModSettings : ModSettings
  {
    public static float multiplyMineableSteel => multiplyMS;
    public static int OriginalSteelAmount;

    // Internal reference only. DO NOT call outside of this class.
    public static float multiplyMS = 1.5f;
    // End warning

    public override void ExposeData()
    {
      base.ExposeData();
      Scribe_Values.Look(ref multiplyMS, "MSAmountToMultiplySteel");
    }
  }

  public class MoreSteelMod : Mod
  {
    MoreSteelModSettings settings;
    public MoreSteelMod(ModContentPack con) : base(con)
    {
      this.settings = GetSettings<MoreSteelModSettings>();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
      Listing_Standard listing = new Listing_Standard();
      listing.Begin(inRect);

      if (MoreSteelModSettings.OriginalSteelAmount >= 1)
      {
        listing.Label("MSRestartWarning".Translate());
        listing.Gap(36);
        listing.Label("MSMultiplyAmountLabel".Translate() + ": [" + MoreSteelModSettings.OriginalSteelAmount.ToString() + " x " + (MoreSteelModSettings.multiplyMS * 100).ToString() + "%] = " + (MoreSteelModSettings.OriginalSteelAmount * MoreSteelModSettings.multiplyMS).ToString() + " " + "MSMultiplyAmountEndLabel".Translate());
        MoreSteelModSettings.multiplyMS = RoundToNearestHalf(listing.Slider(MoreSteelModSettings.multiplyMS, 0f, 20f));
        if (MoreSteelModSettings.OriginalSteelAmount * MoreSteelModSettings.multiplyMS >= 75)
          listing.Label("MSStackWarning".Translate());
      }
      else
      {
        listing.Label("MSOnlyInGame".Translate());
      }

      listing.End();
      base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
      return "MSTitle".Translate();
    }

    private float RoundToNearestHalf(float val)
    { 
      return (float)Math.Round(val * 2, MidpointRounding.AwayFromZero) / 2;
    }
  }
}
