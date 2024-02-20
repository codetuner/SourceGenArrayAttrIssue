using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceGenArrayAttrIssue
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UseCloneForAttribute : Attribute
    {
        public UseCloneForAttribute(string className, string cloneClassName)
        {
            ClassName = className;
            CloneClassName = cloneClassName;
        }

        public string ClassName { get; }
        public string CloneClassName { get; }
    }
}
