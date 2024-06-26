﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToCpp
{
    public enum TokenType
    {
        Keyword,
        Identifier,
        Number,
        String,
        Operator,
        Separator,
        EndOfFile
    }

    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"Type: {Type}, Value: '{Value}'";
        }
    }
}
