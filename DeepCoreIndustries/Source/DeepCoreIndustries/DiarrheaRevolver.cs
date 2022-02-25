using RimWorld;
using Verse;

namespace DCI
{
    public class Projectile_Diarrhea : Bullet
    {
        public ThingDef_DiarrheaBullet Def
        {
            get
            {
                return this.def as ThingDef_DiarrheaBullet;
            }
        }

        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);

            /*
             * Null checking is very important in RimWorld.
             * 99% of errors reported are from NullReferenceExceptions (NREs).
             * Make sure your code checks if things actually exist, before they
             * try to use the code that belongs to said things.
             */
            if (Def != null && hitThing != null && hitThing is Pawn hitPawn) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            {
                var rand = Rand.Value; // This is a random percentage between 0% and 100%
                if (rand <= Def.AddHediffChance) // If the percentage falls under the chance, success!
                {
                    //This checks to see if the character has a heal differential, or hediff on them already.
                    var diarrheaOnPawn = hitPawn?.health?.hediffSet?.GetFirstHediffOfDef(Def.HediffToAdd);
                    var randomSeverity = Rand.Range(0.15f, 0.30f);
                    if (diarrheaOnPawn != null)
                    {
                        //If they already have plague, add a random range to its severity.
                        //If severity reaches 1.0f, or 100%, plague kills the target.
                        diarrheaOnPawn.Severity += randomSeverity;
                        //MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "DCI_DiarrheaBullet_IncreaseMote".Translate(diarrheaOnPawn.Severity), 12f);
                    }
                    else
                    {
                        //These three lines create a new health differential or Hediff,
                        //put them on the character, and increase its severity by a random amount.
                        Hediff hediff = HediffMaker.MakeHediff(Def.HediffToAdd, hitPawn, null);
                        hediff.Severity = randomSeverity;
                        hitPawn.health.AddHediff(hediff, null, null);
                        //MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "DCI_DiarrheaBullet_SuccessMote".Translate(this.launcher.Label, hitPawn.Label), 12f);
                    }
                }
                else //failure!
                {
                    /*
                     * Motes handle all the smaller visual effects in RimWorld.
                     * Dust plumes, symbol bubbles, and text messages floating next to characters.
                     * This mote makes a small text message next to the character.
                     */
                    //MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "DCI_DiarrheaBullet_FailureMote".Translate(Def.AddHediffChance), 12f);
                }
            }
        }
    }
}
