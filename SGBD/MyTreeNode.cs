using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGBD
{
    public class MyTreeNode : TreeNode
    {
        public ObjectType type;
        public string name;
        public int is_primary;
        private MyTreeNode[] array;
        public MyTreeNode parent;

        public MyTreeNode(ObjectType type, string name) :base(name)
        {
            this.type = type;
            this.name = name;
        }

        public MyTreeNode(ObjectType type, string name, MyTreeNode[] array) : base(name,array)
        {
            this.type = type;
            this.name = name;
        }

        public void setParent(MyTreeNode parent)
        {
            this.parent = parent;
        }
    }
}
