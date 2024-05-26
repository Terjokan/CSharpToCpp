using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToCpp.AbstractLanguageTree
{
    public class Class
    {
        public bool Public { get; }
        public bool Static { get; }
        public string Identifier { get; }

        public List<Field> Fields = new List<Field>(); //Maby use dictionarys
        public List<Function> Functions = new List<Function>();
        

        public Class(string identifier, bool Public, bool Static)
        {
            this.Identifier = identifier;
            this.Public = Public;
            this.Static = Static;
        }

    }
}
