﻿using BCC.Archivised.Controls;
using BCC.Menus.Main;
using BCC.Menus.Tension;
using System;
using System.Collections.Generic;

namespace BCC.Archivised
{
    public class ArchivisedModel
    {
        private GeometryMenu geometryMenu;
        private TensionMenu tensionMenu;
        private readonly CycloidGeometryControl geometryControl = new SimpleCycloidGeometryControl();
        private readonly CycloidControl tensionControl;
        

        public ArchivisedModel()
        {
        }

        public List<string> IntegerGeometryParameters => geometryControl.IntegerParameters;
        public List<string> FloatGeometryParameters => geometryControl.FloatParameters;
        public List<string> ResultGeometryParameters => geometryControl.ResultParameters;
        public List<string> IntegerTensionParameters => tensionControl.IntegerParameters;
        public List<string> FloatTensionParameters => tensionControl.FloatParameters;
        public List<string> ResultTensionParameters => tensionControl.ResultParameters;

        public GeometryMenu GeometryMenu { get => geometryMenu; set => geometryMenu = value; }
        public TensionMenu TensionMenu { get => tensionMenu; set => tensionMenu = value; }

        internal void ComputeGeometry(Dictionary<string, double> parameters, bool isEpicycloid)
        {
            Dictionary<string, double> results;
            try
            {
                results = geometryControl.Compute(parameters, isEpicycloid);
            }
            catch (Exception e)
            {
                throw e;
            }
            geometryMenu.ShowResults(results);
        }

        internal Func<double, Tuple<double, double>> Outline => geometryControl.CycloidOutline;
    }
}
