using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToCpp.AbstractLanguageTree
{
    public class Field
    {
        public bool Public { get; }
        public string Identifier { get; }
        public string Type { get; }

        public Field(bool Public, string identifier, string type)
        {
            this.Public = Public;
            this.Identifier = identifier;
            Type = type;
        }
    }
}
