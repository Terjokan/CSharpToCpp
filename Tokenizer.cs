using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToCpp
{
    public class Tokenizer
    {
        private string _source;
        private int _position;

        private static readonly Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>
        {
            { "public", TokenType.Keyword },
            { "private", TokenType.Keyword },
            { "new", TokenType.Keyword },
            { "class", TokenType.Keyword },
            { "static", TokenType.Keyword },
            { "void", TokenType.Keyword },
            { "string", TokenType.Keyword },
            { "int", TokenType.Keyword }
        };

        private static readonly HashSet<char> Operators = new HashSet<char> { '=', '+', '-', '*', '/'};
        private static readonly HashSet<char> Seperators = new HashSet<char> { '.', ';', '{', '}', '(', ')',',' };

        public Tokenizer(string source)
        {
            _source = source;
            _position = 0;
        }

        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();

            while (_position < _source.Length)
            {
                char current = _source[_position];

                if (char.IsWhiteSpace(current))
                {
                    _position++;
                }
                else if (char.IsLetter(current))
                {
                    tokens.Add(ReadIdentifierOrKeyword());
                }
                else if (char.IsDigit(current))
                {
                    tokens.Add(ReadNumber());
                }
                else if (current == '"')
                {
                    tokens.Add(ReadString());
                }
                else if (Operators.Contains(current))
                {
                    tokens.Add(new Token(TokenType.Operator, current.ToString()));
                    _position++;
                }
                else if (Seperators.Contains(current))
                {
                    tokens.Add(new Token(TokenType.Separator, current.ToString()));
                    _position++;
                }
                else
                {
                    throw new Exception($"Unexpected character: {current}");
                }
            }

            tokens.Add(new Token(TokenType.EndOfFile, string.Empty));
            return tokens;
        }

        private Token ReadIdentifierOrKeyword()
        {
            int start = _position;

            while (_position < _source.Length && char.IsLetterOrDigit(_source[_position]))
            {
                _position++;
            }

            string value = _source.Substring(start, _position - start);

            if (Keywords.ContainsKey(value))
            {
                return new Token(Keywords[value], value);
            }
            else
            {
                return new Token(TokenType.Identifier, value);
            }
        }

        private Token ReadNumber()
        {
            int start = _position;

            while (_position < _source.Length && char.IsDigit(_source[_position]))
            {
                _position++;
            }

            string value = _source.Substring(start, _position - start);
            return new Token(TokenType.Number, value);
        }

        private Token ReadString()
        {
            _position++; // Skip the opening quote

            int start = _position;

            while (_position < _source.Length && _source[_position] != '"')
            {
                _position++;
            }

            if (_position >= _source.Length)
            {
                throw new Exception("Unterminated string literal");
            }

            string value = _source.Substring(start, _position - start);
            _position++; // Skip the closing quote

            return new Token(TokenType.String, value);
        }
    }
}
