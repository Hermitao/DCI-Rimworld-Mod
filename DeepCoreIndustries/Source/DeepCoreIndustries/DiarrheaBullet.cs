using RimWorld;
using Verse;

namespace DCI
{
    public class ThingDef_DiarrheaBullet : ThingDef
    {
        public float AddHediffChance = 1.0f;
        public HediffDef HediffToAdd = HediffDefOf.FoodPoisoning;
    }
}
