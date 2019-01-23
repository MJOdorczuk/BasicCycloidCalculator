using System;
using System.Collections.Generic;

namespace BCC.Miscs
{
    public static partial class Vocabulary
    {
        public enum Language { POLISH, ENGLISH}
        private static Language language = Language.POLISH;
        private static readonly string NOT_IMPLEMENTED_MESSAGE = "Translation not implemented yet";
        private static readonly List<Action> nameCalls = new List<Action>();

        /// <summary>
        /// Add action to fire when language is changed
        /// </summary>
        /// <param name="action"> Action to fire when language is changed</param>
        public static void AddNameCall(Action action) => nameCalls.Add(action);

        /// <summary>
        /// Call every action set for language changeing event
        /// </summary>
        public static void UpdateAllNames()
        {
            foreach(var call in nameCalls)
            {
                call();
            }
        }

        /// <summary>
        /// Change language to one of predefined languages
        /// </summary>
        /// <param name="language"> Predefined choosable language</param>
        public static void SetLanguage(Language language)
        {
            if(language != Vocabulary.language)
            {
                Vocabulary.language = language;
                Vocabulary.UpdateAllNames();
            }
        }

        /// <summary>
        /// Check currently used language
        /// </summary>
        /// <returns> Currently used predefined language</returns>
        public static Language GetLanguage() => Vocabulary.language;

        internal static string NotImplementedYet()
        {
            return NOT_IMPLEMENTED_MESSAGE;
        }

        internal static string Include()
        {
            switch (language)
            {
                case Language.POLISH:
                    return "Uwzględnij";
                case Language.ENGLISH:
                    return "Include";
            }
            return NOT_IMPLEMENTED_MESSAGE;
        }

        internal static string ToleratedElementNumber()
        {
            switch (language)
            {
                case Language.POLISH:
                    return "Numer elementu tolerowanego";
                case Language.ENGLISH:
                    return "Tolerated element number";
            }
            return NOT_IMPLEMENTED_MESSAGE;
        }

        internal static string ForUpperDeviation()
        {
            switch (language)
            {
                case Vocabulary.Language.POLISH:
                    return "Dla górnej odychłki";
                case Vocabulary.Language.ENGLISH:
                    return "For upper deviation";
            }
            return NOT_IMPLEMENTED_MESSAGE;
        }

        internal static string ForLowerDeviation()
        {
            switch (language)
            {
                case Vocabulary.Language.POLISH:
                    return "Dla dolnej odchyłki";
                case Vocabulary.Language.ENGLISH:
                    return "For lower deviation";
            }
            return NOT_IMPLEMENTED_MESSAGE;
        }
    }
}