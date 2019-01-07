using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Miscs
{
    public static class Vocabulary
    {
        public enum Language { POLISH, ENGLISH}
        private static Language language = Language.POLISH;
        private static readonly string NOT_IMPLEMENTED_MESSAGE = "Translation not implemented yet";
        private static readonly List<Action> nameCalls = new List<Action>();

        public static void AddNameCall(Action action) => nameCalls.Add(action);
        public static void UpdateAllNames()
        {
            foreach(var call in nameCalls)
            {
                call();
            }
        }

        public static void SetLanguage(Language language)
        {
            Vocabulary.language = language;
            Vocabulary.UpdateAllNames();
        }
        public static Language GetLanguage() => Vocabulary.language;

        internal static string NotImplementedYet()
        {
            return NOT_IMPLEMENTED_MESSAGE;
        }

        public struct TabPagesNames
        {
            public static string Geometry()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Geometria";
                    case Language.ENGLISH:
                        return "Geometry";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
            public static string Dimensioning()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Wymiarowanie";
                    case Language.ENGLISH:
                        return "Dimensioning";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }

            internal static string Results()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Wyniki";
                    case Language.ENGLISH:
                        return "Results";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
        }
        public struct ParameterLabels
        {
            public struct Geometry
            {
                public static string ProfileType()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Rodzaj zarysu";
                        case Language.ENGLISH:
                            return "Profile type";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string Epicycloid()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Epicykloidalny";
                        case Language.ENGLISH:
                            return "Epicycloid";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string Hipocycloid()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Hipocykloidalny";
                        case Language.ENGLISH:
                            return "Hipocycloid";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string TeethQuantity()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Liczba zębów";
                        case Language.ENGLISH:
                            return "Teeth quantity";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string RollRadius()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Promień rolki";
                        case Language.ENGLISH:
                            return "Roll radius";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string MajorDiameter()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Średnica wierzchołkowa";
                        case Language.ENGLISH:
                            return "Majord diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string RootDiameter()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Średnica koła stóp";
                        case Language.ENGLISH:
                            return "Root diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string RollSpacingDiameter()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Średnica rozmieszczenia rolek";
                        case Language.ENGLISH:
                            return "Roll spacing diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string Eccentricity()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Mimośród";
                        case Language.ENGLISH:
                            return "Eccentricity";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string ToothHeight()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Wysokość zęba";
                        case Language.ENGLISH:
                            return "Tooth height";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string ToothHeightFactor()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Współczynnik wysokości zęba";
                        case Language.ENGLISH:
                            return "Tooth height factor";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string PinSpacingDiameter()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Średnica rozmieszczenia sworzni";
                        case Language.ENGLISH:
                            return "Pin spacing diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string RollingCircleDiameter()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Średnica okręgu toczącego";
                        case Language.ENGLISH:
                            return "Rolling circle diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                public static string BaseDiameter()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Średnica koła zasadniczego";
                        case Language.ENGLISH:
                            return "Base diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
            }
            public struct Dimensioning
            {
                internal static string Brass()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Mosiądz";
                        case Language.ENGLISH:
                            return "Brass";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static object Custom()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Niestandardowy";
                        case Language.ENGLISH:
                            return "Custom";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string Bronze()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Brąz";
                        case Language.ENGLISH:
                            return "Bronze";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string CastIron()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Żeliwo";
                        case Language.ENGLISH:
                            return "Cast Iron";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string SleeveMaterial()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Materiał tulei";
                        case Language.ENGLISH:
                            return "Sleeve material";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string GearMaterial()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Materiał koła";
                        case Language.ENGLISH:
                            return "Gear Material";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string PoissonsRatio()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Współczynnik Poissona";
                        case Language.ENGLISH:
                            return "Poisson\'s Ratio";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string Steel()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Stal";
                        case Language.ENGLISH:
                            return "Steel";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string YoungsModulus()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Moduł Younga";
                        case Language.ENGLISH:
                            return "Young\'s Modulus";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string FaceWidth()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Szerokość Koła";
                        case Language.ENGLISH:
                            return "Face Width";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string RollQuantity()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Liczba Sworzni";
                        case Language.ENGLISH:
                            return "Roll Quantity";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string HoleRadius()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Promień Otworu";
                        case Language.ENGLISH:
                            return "Hole Radius";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string SleeveRadius()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Promień Tulei";
                        case Language.ENGLISH:
                            return "Sleeve Radius";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string RollSpacingRadius()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Promień Rozmieszczenia Sworzni";
                        case Language.ENGLISH:
                            return "Roll Spacing Diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string EngineeringTolerances()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Tolerancje wykonania";
                        case Language.ENGLISH:
                            return "Engineering Tolerances";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string HoleSpacingRadius()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Promień Rozmieszczenia Otworów";
                        case Language.ENGLISH:
                            return "Hole Spacing Radius";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string SleeveSpacingRadius()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Promień Rozmieszczenia Tulei";
                        case Language.ENGLISH:
                            return "Sleeve Spacing Radius";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string HoleSpacingAngle()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Kąt Rozmieszczenia Otworów";
                        case Language.ENGLISH:
                            return "Hole Spacing Angle";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string SleeveSpacingAngle()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Kąt Rozmieszczenia Tulei";
                        case Language.ENGLISH:
                            return "Sleeve Spacing Angle";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
            }
            public struct Result
            {
                internal static string Force()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Siła";
                        case Language.ENGLISH:
                            return "Force";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string Momentum()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Moment";
                        case Language.ENGLISH:
                            return "Momentum";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string Pressure()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Nacisk";
                        case Language.ENGLISH:
                            return "Pressure";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string RollNumber()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Numer Rolki";
                        case Language.ENGLISH:
                            return "Roll Number";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
            }
        }
        public struct BubbleMessages
        {
            public struct Geometry
            {
                public static string TheCliqueIsImproper()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Ta klika jest niepoprawna";
                        case Language.ENGLISH:
                            return "The clique is improper";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string PossibleCliquesAre()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Możliwymi klikami są";
                        case Language.ENGLISH:
                            return "Possible cliques are:";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
            }
        }
        public struct ISOBar
        {
            internal static string Edit()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Edycja";
                    case Language.ENGLISH:
                        return "Edit";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }

            internal static string Exit()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Zamknij";
                    case Language.ENGLISH:
                        return "Exit";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }

            internal static string File()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Plik";
                    case Language.ENGLISH:
                        return "File";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }

            internal static string Help()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Pomoc";
                    case Language.ENGLISH:
                        return "Help";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }

            internal static string New()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Nowy";
                    case Language.ENGLISH:
                        return "New";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }

            internal static string Open()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Otwórz";
                    case Language.ENGLISH:
                        return "Open";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }

            internal static string Save()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Zapisz";
                    case Language.ENGLISH:
                        return "Save";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }

            internal static string View()
            {
                switch (language)
                {
                    case Language.POLISH:
                        return "Widok";
                    case Language.ENGLISH:
                        return "View";
                }
                return NOT_IMPLEMENTED_MESSAGE;
            }
        }
    }
}