
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Not in use, ai tests

namespace Simply_hard
{
    public abstract class AstNode
    {
    }

    public class ClassNode : AstNode
    {
        public string Name { get; }
        public List<MethodNode> Methods { get; }
        public List<FieldNode> Fields { get; }

        public ClassNode(string name, List<MethodNode> methods, List<FieldNode> fields)
        {
            Name = name;
            Methods = methods;
            Fields = fields;
        }
    }

    public class MethodNode : AstNode
    {
        public string Name { get; }
        public List<ParameterNode> Parameters { get; }
        public List<AstNode> Body { get; }

        public MethodNode(string name, List<ParameterNode> parameters, List<AstNode> body)
        {
            Name = name;
            Parameters = parameters;
            Body = body;
        }
    }

    public class ParameterNode : AstNode
    {
        public string Name { get; }
        public string Type { get; }

        public ParameterNode(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }

    public class FieldNode : AstNode
    {
        public string Name { get; }
        public string Type { get; }

        public FieldNode(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }

    public class VariableAssignmentNode : AstNode
    {
        public string Name { get; }
        public string Type { get; }
        public string Value { get; }

        public VariableAssignmentNode(string name, string type, string value)
        {
            Name = name;
            Type = type;
            Value = value;
        }
    }

    public class ExpressionNode : AstNode
    {
        public string Expression { get; }

        public ExpressionNode(string expression)
        {
            Expression = expression;
        }
    }

}
