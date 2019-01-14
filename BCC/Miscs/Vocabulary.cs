using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}