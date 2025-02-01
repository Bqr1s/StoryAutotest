using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Verse;
using RimWorld;
using RimWorld.Planet;
using HarmonyLib;
using UnityEngine;
using RimWorld;
using Verse;
using LudeonTK;

namespace StoryAutotest
{
	[StaticConstructorOnStartup]
	public class Main
	{
		static Main()
		{
		}
	}

	public static class DebugTools
	{
		[DebugAction("AWOO", null, false, false, false, false, 0, false, actionType = DebugActionType.Action, allowedGameStates = AllowedGameStates.PlayingOnMap)]
		// Using mod name to prevent conflict with this command expected to be added to base game
		private static void AutotestBackstories()
		{
			Map map = Find.CurrentMap;
			if (map == null)
				return;
			Pawn p = map.mapPawns.AllHumanlikeSpawned.First();
			if (p == null)
				return;
			if (p.DevelopmentalStage.Child())
				return;
			IEnumerable<BackstoryDef> kidBackstories = DefDatabase<BackstoryDef>.AllDefs.Where((BackstoryDef bs) => bs.shuffleable && bs.slot == BackstorySlot.Childhood);
			IEnumerable<BackstoryDef> adultBackstories = DefDatabase<BackstoryDef>.AllDefs.Where((BackstoryDef bs) => bs.shuffleable && bs.slot == BackstorySlot.Adulthood);

			foreach (BackstoryDef bs in kidBackstories)
			{
				TestString(bs.FullDescriptionFor(p));
			}
			foreach (BackstoryDef bs in adultBackstories)
			{
				TestString(bs.FullDescriptionFor(p));
			}

			Messages.Message("Test finished", MessageTypeDefOf.NeutralEvent);
		}
		private static void TestString(string s)
		{
			// Anti-optimisation and code removal
			if (s == "unique key")
				Messages.Message("", MessageTypeDefOf.NeutralEvent);
		}
	}
}

