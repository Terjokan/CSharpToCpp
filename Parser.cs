using CSharpToCpp.AbstractLanguageTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToCpp
{
    internal class Parser
    {
        private Token[] _tokens;
        private int _position;

        private bool _Public; //Will be needed to adjust caus of internal and protected
        private bool _Static;

        public List<Class> ClassList = new List<Class>();


        private Class? _CurrentClass;
        private Function? _CurrentFunction;

        public Parser(Token[] token)
        {
            _tokens = token;
            _position = 0;
        }

        public void Parse()
        {
            while (_position < _tokens.Length)
            {
                Token token = _tokens[_position];

                if(token.Type == TokenType.Keyword)
                {
                    ParseKeyword(token.Value);
                }
                else if(token.Type == TokenType.Operator)
                {
                    ParseOperatior();
                }
            }
        }

        private void ParseOperatior()
        {
            if (_tokens[_position].Value == "}")
            {
                if(_CurrentFunction != null)
                {
                    _CurrentFunction = null;
                } 
                else if( _CurrentClass != null)
                {
                    _CurrentClass = null;
                }

                _position++;
                EndOfInstruction();
            }
        }

        private void ParseKeyword(string keyword) // Always check Keyword Dictionary in Tokenizer.cs
        {
            if (keyword == "public")
            {
                _Public = true;
            }
            else if (keyword == "private")
            {
                _Public = false;
            }
            else if (keyword == "static")
            {
                _Static = true;
            }
            else if (keyword == "class")
            {
                ParseClass();
            }
            else if(keyword == "void")
            {
                _position++;
                ParseMethod();
            }

        }

        private void ParseClass() // Creates the class and opens it
        {
            if (_tokens[_position].Value != "class") //should never happen
            {
                Error("ParseClass: InternalError, Parsing an unexisting class");
                return;
            }

            if (_position + 2 < _tokens.Length)
                Error("ParseClass: Unexpected end of line after keyword 'class'");
            if (_tokens[_position + 1].Type != TokenType.Identifier)
                Error("ParseClass: Cant get identifier after 'class' keyword, type :" + _tokens[_position + 1].Type);

            string Identifier = _tokens[_position + 1].Value;

            

            Class Class = new Class(Identifier, _Public, _Static);

            ClassList.Add(Class);

            if(_tokens[_position + 2].Type == TokenType.Operator && _tokens[_position + 2].Value == "{") //Later Check for abstraction
            {
                _CurrentClass = Class;
                
                _position += 3; //sets pos after {
                EndOfInstruction();
                return;
            }

            //maby aditional steps for abstraction
        }

        private string ParseType(int position)
        {
            string result = "";
            while (_tokens[position + 1].Value == ".")
            {
                result += _tokens[position].Value;
                result += ".";
                position += 2;
            }
            result += _tokens[position].Value;
            return result;
        } // Maby need it
        private string ParseTypeReversed(int position)
        {
            string result = "";
            while (_tokens[position - 1].Value == ".")
            {
                result += _tokens[position].Value;
                result += ".";
                position -= 2;
            }
            result += _tokens[position].Value;
            return result;
        } // Maby need it

        private void ParseMethod()
        {

        }
        private void ParseField()
        {

        }

        private void EndOfInstruction() //Gets allways called by a ; or at start of a class
        {
            _Static = false;
            if (_CurrentClass != null && _CurrentClass.Static)
                _Static = true;

            if (_CurrentFunction != null)
                _Static = false;

            _Public = false;// standart is private
        }

        private void Error(string Message)
        {
            Console.WriteLine("[Parser] " + Message);
        }
    }
}
