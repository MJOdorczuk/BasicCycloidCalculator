using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Controls
{
    class SimpleCycloidTensionControl : CycloidTensionControl
    {
        public override Dictionary<string, double> Compute(Dictionary<string, double> parameters, bool isEpicycloid)
        {
            throw new NotImplementedException();
        }

        public override List<string> ShortenClique(List<string> clique)
        {
            throw new NotImplementedException();
        }

        internal override Func<double,int, double> TensionFunctionGenerator(int zw, double M, double dw, double[] delta)
        {
            /*return (phi,j) =>
            {
                double Q;
                double epsilon;
                double R_tzj = 0, delta_Rtzj = 0, y_okj = 0, R_otwj = 0, delta_Rotwj = 0;
                double y_otj = (dw/2 + delta_Rwt) * Math.Sin(phi_tj + delta_phitj); // R_wt =? Rw
                double y_oktj = y_otj - (e + delta_e);
                double Delta = Math.Floor(y_oktj - (R_tzj + delta_Rtzj)) - Math.Floor(y_okj - (R_otwj + delta_Rotwj));
                if(delta>)
                return Q;
            };*/ //TO DO
            throw new NotImplementedException();
        }
    }
}
