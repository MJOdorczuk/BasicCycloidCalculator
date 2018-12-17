using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCC.Archivised.Controls
{
    abstract class CycloidControl
    {
        protected List<string> integerParameters, floatParameters, necessaryParameters, resultParameters;
        protected List<List<string>> possibleCliques;
        public List<string> IntegerParameters => new List<string>(integerParameters);
        public List<string> FloatParameters => new List<string>(floatParameters);
        public List<string> NecessaryParameters => new List<string>(floatParameters);
        public List<string> ResultParameters => new List<string>(resultParameters);
        public abstract List<string> ShortenClique(List<string> clique);
        public abstract Dictionary<string,double> Compute(Dictionary<string, double> parameters, bool isEpicycloid);
    }
}
