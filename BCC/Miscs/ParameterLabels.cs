namespace BCC.Miscs
{
    public static partial class Vocabulary
    {
        public struct ParameterLabels
        {
            public struct Geometry
            {
                /// <summary>
                /// Profile type (e.g. Epicycloid/Hipocycloid)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Epicycloid (profile type)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Hipocycloid (profile type)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Teeth quantity (z)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Roll radius (g)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Major diameter (Da)
                /// </summary>
                /// <returns></returns>
                public static string MajorDiameter()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Średnica wierzchołkowa";
                        case Language.ENGLISH:
                            return "Major diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                /// <summary>
                /// Root diameter (Df)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Roll Spacing Diameter (Dg)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Eccentricity (e)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Tooth Height (h)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Tooth height factor (λ)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Pin spacing diameter (Dw) not sure
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Rolling Circle diameter (ρ)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Base diameter (Db)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Brass
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Custom (for not using any predefine)
                /// </summary>
                /// <returns></returns>
                internal static string Custom()
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
                /// <summary>
                /// Bronze
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Cast iron
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Sleeve material
                /// </summary>
                /// <returns></returns>
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

                internal static string IncludeEngineeringTolerances()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Uwzględnić tolerancje";
                        case Language.ENGLISH:
                            return "Include engineering tolerances";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                /// <summary>
                /// Gear material
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Poisson's ratio (ν)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Steel
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Young's modulus (E)
                /// </summary>
                /// <returns></returns>
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
                /// <summary>
                /// Face width (b)
                /// </summary>
                /// <returns></returns>
                internal static string FaceWidth()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Szerokość koła";
                        case Language.ENGLISH:
                            return "Face width";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                /// <summary>
                /// Roll quantity (n)
                /// </summary>
                /// <returns></returns>
                internal static string RollQuantity()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Liczba sworzni";
                        case Language.ENGLISH:
                            return "Roll quantity";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                /// <summary>
                /// Hole radius (R_hole)
                /// </summary>
                /// <returns></returns>
                internal static string HoleRadius()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Promień otworu";
                        case Language.ENGLISH:
                            return "Hole radius";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                /// <summary>
                /// Sleeve radius (R_sleeve)
                /// </summary>
                /// <returns></returns>
                internal static string SleeveRadius()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Promień tulei";
                        case Language.ENGLISH:
                            return "Sleeve radius";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                /// <summary>
                /// Roll spacing radius (R_roll_spacing)
                /// </summary>
                /// <returns></returns>
                internal static string RollSpacingRadius()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Promień rozmieszczenia sworzni";
                        case Language.ENGLISH:
                            return "Roll spacing diameter";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                /// <summary>
                /// Engineering tolerances (fits)
                /// </summary>
                /// <returns></returns>
                internal static string EngineeringTolerances()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Tolerancje wykonania";
                        case Language.ENGLISH:
                            return "Engineering tolerances";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                /// <summary>
                /// Hole spacing radius (R_hole_spacing)
                /// </summary>
                /// <returns></returns>
                internal static string HoleSpacingRadius()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Promień rozmieszczenia otworów";
                        case Language.ENGLISH:
                            return "Hole spacing radius";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                /// <summary>
                /// Sleeve spacing radius (R_sleeve_spacing)
                /// </summary>
                /// <returns></returns>
                internal static string SleeveSpacingRadius()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Promień rozmieszczenia tulei";
                        case Language.ENGLISH:
                            return "Sleeve spacing radius";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                /// <summary>
                /// Hole spacing angle (φ_hole)
                /// </summary>
                /// <returns></returns>
                internal static string HoleSpacingAngle()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Kąt rozmieszczenia otworów";
                        case Language.ENGLISH:
                            return "Hole spacing angle";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                /// <summary>
                /// Hole spacing angle (φ_sleeve)
                /// </summary>
                /// <returns></returns>
                internal static string SleeveSpacingAngle()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Kąt rozmieszczenia tulei";
                        case Language.ENGLISH:
                            return "Sleeve spacing angle";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
            }
            public struct Result
            {
                /// <summary>
                /// Force (F)
                /// </summary>
                /// <returns></returns>
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

                internal static string CarriedForces()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Przenoszone siły";
                        case Language.ENGLISH:
                            return "Carried forces";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string LoosenessDistributionInMechanism()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Rozkład luzów w mechanizmie";
                        case Language.ENGLISH:
                            return "Looseness distribution in mechanism";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                /// <summary>
                /// Momentum (M)
                /// </summary>
                /// <returns></returns>
                internal static string Torque()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Moment";
                        case Language.ENGLISH:
                            return "Torque";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                /// <summary>
                /// Pressure (p)
                /// </summary>
                /// <returns></returns>
                internal static string ContactStress()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Nacisk stykowy";
                        case Language.ENGLISH:
                            return "Contact stress";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
                /// <summary>
                /// Roll number (j)
                /// </summary>
                /// <returns></returns>
                internal static string RollNumber()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Numer rolki";
                        case Language.ENGLISH:
                            return "Roll number";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }

                internal static string ContactStresses()
                {
                    switch (language)
                    {
                        case Language.POLISH:
                            return "Naprężenia stykowe";
                        case Language.ENGLISH:
                            return "Contact stresses";
                    }
                    return NOT_IMPLEMENTED_MESSAGE;
                }
            }
        }
    }
}