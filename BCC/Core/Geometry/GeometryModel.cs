using BCC.Interface_View.StandardInterface;
using BCC.Interface_View.StandardInterface.Geometry;
using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Core.Geometry
{
    public enum CycloParams
    {
        Z,
        G,
        DA,
        DF,
        E,
        H,
        DG,
        Λ,
        DW,
        Ρ,
        DB,
        EPI
    }

    public enum ErrorTypes
    {
        OUT_OF_RANGE,
        NO_POSSIBLE_CLIQUE,
        INCOMPLETE_CLIQUE,
        CURVATURE_REQUIREMENT,
        TOOTH_CUTTING_REQUIREMENT,
        NEIGHBOURHOOD_REQUIREMENT
    }

    abstract class GeometryModel
    {
        // The view and the control for geometry computation
        private GeometryMenu view = null;

        // Parameters lists
        public abstract List<CycloParams> OptionalParams();
        public abstract List<CycloParams> ObligatoryIntParams();
        public abstract List<CycloParams> ObligatoryFloatParams();
        public abstract List<CycloParams> OutputParams();
        public abstract List<List<CycloParams>> PossibleCliques(params List<CycloParams>[] cliques);

        // Generating and binding a view with the model
        public virtual GeometryMenu GetMenu()
        {
            if (view is null)
            {
                view = new GeometryMenu(this, 360);
            }
            return view;
        }

        // Transferring data to and from the view
        protected Dictionary<CycloParams, object> DownLoadData()
        {
            var ret = new Dictionary<CycloParams, object>();
            foreach(var param in ObligatoryIntParams())
            {
                ret.Add(param, view.Get(param));
            }
            foreach(var param in ObligatoryFloatParams())
            {
                ret.Add(param, view.Get(param));
            }
            foreach(var param in OptionalParams())
            {
                if (view.IsAvailable(param)) ret.Add(param, view.Get(param));
            }
            return ret;
        }
        protected void UpLoadData(Dictionary<CycloParams, object> data)
        {
            foreach(var param in data)
            {
                view.Set(param.Key, param.Value);
            }
        }

        // Requirements checkers with actions
        protected abstract bool IsPositiveValue(CycloParams param, double value);
        protected abstract bool IsCliquePossible(List<CycloParams> clique);
        protected abstract bool IsCurvatureRequirementMet(Dictionary<CycloParams, double> vals, bool epi);
        protected abstract bool IsToothCuttingRequirementMet(Dictionary<CycloParams, double> vals, bool epi);
        protected abstract bool IsNeighbourhoodRequirementMet(Dictionary<CycloParams, double> vals, bool epi);

        // Overall requirements checker
        protected bool AreRequirementsMet(Dictionary<CycloParams, double> vals, bool epi)
        {
            var ret = true;
            foreach(var val in vals)
            {
                ret = true && IsPositiveValue(val.Key, val.Value);
            }
            ret = true && IsCliquePossible(vals.Keys.Intersect(OptionalParams()).ToList());
            ret = true && IsCurvatureRequirementMet(vals, epi);
            ret = true && IsToothCuttingRequirementMet(vals, epi);
            ret = true && IsNeighbourhoodRequirementMet(vals, epi);
            return ret;
        }

        // Name calls for parameters
        public Func<string> CallName(CycloParams param)
        {
            switch (param)
            {
                case CycloParams.Z:
                    return () => Vocabulary.TeethQuantity();
                case CycloParams.G:
                    return () => Vocabulary.RollDiameter();
                case CycloParams.DA:
                    return () => Vocabulary.MajorDiameter();
                case CycloParams.DF:
                    return () => Vocabulary.RootDiameter();
                case CycloParams.E:
                    return () => Vocabulary.Eccentricity();
                case CycloParams.H:
                    return () => Vocabulary.ToothHeight();
                case CycloParams.DG:
                    return () => Vocabulary.RollSpacingDiameter();
                case CycloParams.Λ:
                    return () => Vocabulary.ToothHeightFactor();
                case CycloParams.DW:
                    return () => Vocabulary.PinSpacingDiameter();
                case CycloParams.Ρ:
                    return () => Vocabulary.RollingCircleDiameter();
                case CycloParams.DB:
                    return () => Vocabulary.BaseDiameter();
                case CycloParams.EPI:
                    return () => Vocabulary.ProfileType();
                default:
                    return () => Vocabulary.NotImplementedYet();
            }
        }
    }
}