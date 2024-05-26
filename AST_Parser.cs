using CSharpToCpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Not in use, ai tests

namespace Simply_hard
{
    public class AST_Parser
    {
        private readonly List<Token> tokens;
        private int position;

        public AST_Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            this.position = 0;
        }

        private Token CurrentToken => position < tokens.Count ? tokens[position] : null;
        private Token ConsumeToken() => position < tokens.Count ? tokens[position++] : null;
        private void ExpectToken(TokenType type, string value = null)
        {
            if (CurrentToken?.Type != type || (value != null && CurrentToken?.Value != value))
                throw new Exception($"Expected token type {type} with value '{value}' but found {CurrentToken?.Type} with value '{CurrentToken?.Value}'");
            ConsumeToken();
        }

        public List<AstNode> Parse()
        {
            var nodes = new List<AstNode>();
            while (CurrentToken != null && CurrentToken.Type != TokenType.EndOfFile)
            {
                if (CurrentToken.Type == TokenType.Keyword && CurrentToken.Value == "public")
                {
                    nodes.Add(ParseClassOrMethod());
                }
                else
                {
                    throw new Exception($"Unexpected token {CurrentToken}");
                }
            }
            return nodes;
        }

        private AstNode ParseClassOrMethod()
        {
            ExpectToken(TokenType.Keyword, "public");
            if (CurrentToken?.Value == "class")
            {
                return ParseClass();
            }
            else
            {
                return ParseMethod();
            }
        }

        private ClassNode ParseClass()
        {
            ExpectToken(TokenType.Keyword, "class");
            var name = CurrentToken?.Value;
            ExpectToken(TokenType.Identifier); // class name
            ExpectToken(TokenType.Separator, "{");
            var methods = new List<MethodNode>();
            var fields = new List<FieldNode>();
            while (CurrentToken?.Type != TokenType.Separator || CurrentToken?.Value != "}")
            {
                if (CurrentToken?.Type == TokenType.Keyword && CurrentToken?.Value == "public")
                {
                    ConsumeToken();
                    if (CurrentToken?.Value == "void")
                    {
                        methods.Add(ParseMethod());
                    }
                    else
                    {
                        fields.Add(ParseField());
                    }
                }
                else
                {
                    throw new Exception($"Unexpected token {CurrentToken}");
                }
            }
            ExpectToken(TokenType.Separator, "}");
            return new ClassNode(name, methods, fields);
        }

        private FieldNode ParseField()
        {
            var type = CurrentToken?.Value;
            ExpectToken(TokenType.Keyword); // type
            var name = CurrentToken?.Value;
            ExpectToken(TokenType.Identifier); // field name
            ExpectToken(TokenType.Separator, ";");
            return new FieldNode(name, type);
        }

        private MethodNode ParseMethod()
        {
            var returnType = CurrentToken?.Value;
            ExpectToken(TokenType.Keyword); // return type
            var name = CurrentToken?.Value;
            ExpectToken(TokenType.Identifier); // method name
            ExpectToken(TokenType.Separator, "(");
            var parameters = new List<ParameterNode>();
            while (CurrentToken?.Type != TokenType.Separator || CurrentToken?.Value != ")")
            {
                var paramType = CurrentToken?.Value;
                ExpectToken(TokenType.Keyword); // parameter type
                var paramName = CurrentToken?.Value;
                ExpectToken(TokenType.Identifier); // parameter name
                parameters.Add(new ParameterNode(paramName, paramType));
                if (CurrentToken?.Type == TokenType.Separator && CurrentToken?.Value == ",")
                {
                    ConsumeToken(); // consume ','
                }
            }
            ExpectToken(TokenType.Separator, ")");
            ExpectToken(TokenType.Separator, "{");
            var body = new List<AstNode>();
            while (CurrentToken?.Type != TokenType.Separator || CurrentToken?.Value != "}")
            {
                if (CurrentToken?.Type == TokenType.Identifier)
                {
                    body.Add(ParseStatement());
                }
                else
                {
                    throw new Exception($"Unexpected token {CurrentToken}");
                }
            }
            ExpectToken(TokenType.Separator, "}");
            return new MethodNode(name, parameters, body);
        }

        private AstNode ParseStatement()
        {
            var name = CurrentToken?.Value;
            ExpectToken(TokenType.Identifier); // variable or method name
            if (CurrentToken?.Type == TokenType.Operator && CurrentToken?.Value == "=")
            {
                return ParseVariableAssignment(name);
            }
            else
            {
                return ParseExpression(name);
            }
        }

        private VariableAssignmentNode ParseVariableAssignment(string name)
        {
            ExpectToken(TokenType.Operator, "=");
            var type = CurrentToken?.Value;
            ExpectToken(TokenType.Identifier); // type
            var value = CurrentToken?.Value;
            ExpectToken(TokenType.Identifier); // value
            ExpectToken(TokenType.Separator, ";");
            return new VariableAssignmentNode(name, type, value);
        }

        private ExpressionNode ParseExpression(string name)
        {
            var expression = name;
            while (CurrentToken?.Type != TokenType.Separator || CurrentToken?.Value != ";")
            {
                expression += " " + ConsumeToken()?.Value;
            }
            ExpectToken(TokenType.Separator, ";");
            return new ExpressionNode(expression);
        }
    }
}
