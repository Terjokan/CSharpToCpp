using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToCpp.AbstractLanguageTree
{
    public class Function
    {
        public bool Public { get; }
        public bool Static { get; }
        public string Identifier { get; }

        public string ReturnType { get; }

        public List<Field> Fields = new List<Field>(); //Maby use dictionarys

        public Function(string Identifier, string returnType, bool Public, bool Static)
        {
            this.Identifier = Identifier;
            this.Public = Public;
            this.Static = Static;
            this.ReturnType = returnType;
        }

    }
}
