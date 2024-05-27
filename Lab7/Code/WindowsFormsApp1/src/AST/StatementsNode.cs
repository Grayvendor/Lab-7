using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src.AST
{
    internal class StatementsNode: ExpressionNode
    {
        List<ExpressionNode> codeStrings = new List<ExpressionNode>();
        
        public void addNode(ExpressionNode node)
        {
            this.codeStrings.Add(node);
        }
    }
}
