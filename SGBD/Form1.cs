using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleApp;

namespace SGBD
{
    public partial class Form1 : Form
    {
        public const string SERVER_CONNECTION = "(local)\\SQLServer - ";
        SqlRepository server;
        FormLogin formLogin=null;
        MyTreeNode current = null, nodePlaceHolder = null;
        Dictionary<ObjectType, Action> dict, drop_dict;
        public Form1()
        {
            InitializeComponent();
            server = new SqlRepository();
            formLogin = new FormLogin(ref server,load);
            formLogin.Owner = this;
            formLogin.Show();
            initFunctDictionary();
            initDropDictionary();
        }

        private void initDropDictionary()
        {
            drop_dict = new Dictionary<ObjectType, Action>();
            drop_dict[ObjectType.TABLE] = dropTable;
            drop_dict[ObjectType.INDEX] = dropIndex;
            drop_dict[ObjectType.TRIGGER] = dropTrigger;
            drop_dict[ObjectType.STOREDPROCEDURE] = dropStoreProcedure;
            drop_dict[ObjectType.CHECK] = dropCheck;
            drop_dict[ObjectType.FUNCTION] = dropFunction;
            drop_dict[ObjectType.VIEW] = dropView;
            drop_dict[ObjectType.USER] = dropUser;
            drop_dict[ObjectType.LOGIN] = dropLogin;
            drop_dict[ObjectType.DATABASE] = dropDatabase;
            
        }

        private void initFunctDictionary()
        {
            dict = new Dictionary<ObjectType, Action>();
            dict[ObjectType.STOREDPROCEDURES_FOLDER] = setStoreProceduresInTree;
            dict[ObjectType.CHECKS_FOLDER] = setChecksInTree;
            dict[ObjectType.TRIGGERS_FOLDER] = setTriggersInTree;
            dict[ObjectType.INDEXES_FOLDER] = setIndexesInTree;
            dict[ObjectType.TABLES_FOLDER] = setTablesInTree;
            dict[ObjectType.DATABASE_FOLDER] = setDatabasesInTree;
            dict[ObjectType.FUNCTIONS_FOLDER] = setFunctionsInTree;
            dict[ObjectType.VIEWS_FOLDER] = setViewsInTree;
            dict[ObjectType.LOGIN_FOLDER] = setUsersInTree;
            dict[ObjectType.USER_FOLDER] = setUsersInDatabaseTree;
            
        }

        public void load()
        {
            if (server.user != null)
            {
                Userlabel.Text = SERVER_CONNECTION + server.user;
                var securityFolder = new MyTreeNode(ObjectType.SECURIY_FOLDER, "Security",new MyTreeNode[] { new MyTreeNode(ObjectType.LOGIN_FOLDER, "Logins")});
                MyTreeNode [] array = new MyTreeNode[] { new MyTreeNode(ObjectType.DATABASE_FOLDER, "Databases"),
                    securityFolder
                                                        };
                MyTreeNode treeNode = new MyTreeNode(ObjectType.SERVER, "SqlServer", array);
                
                treeView.Nodes.Add(treeNode);
                treeNode.Expand();
            }
        }
        //drops
        public void dropTable()
        {
            server.dropTable(current.name, current.parent.name);
            Messagelabel.Text = "Drop table succesfully.";
        }
        public void dropView()
        {
            server.dropView(current.name, current.parent.name);
            Messagelabel.Text = "Drop view succesfully.";
        }
        public void dropUser()
        {
            server.dropUser(current.name, current.parent.name);
            Messagelabel.Text = "Drop user succesfully.";
        }
        public void dropLogin()
        {
            server.dropLogin(current.name);
            Messagelabel.Text = "Drop login succesfully.";
        }
        
        public void dropIndex()
        {
            server.dropIndex(current.name, current.parent.name, current.parent.parent.name);
            Messagelabel.Text = "Drop index succesfully.";
        }
        public void dropTrigger()
        {
            server.dropTrigger(current.name, current.parent.parent.name);
            Messagelabel.Text = "Drop trigger succesfully.";
        }
        public void dropStoreProcedure()
        {
            server.dropStoreProcedure(current.name, current.parent.name);
            Messagelabel.Text = "Drop store procedure succesfully.";
        }
        public void dropCheck()
        {
            server.dropCheck(current.name, current.parent.name, current.parent.parent.name);
            Messagelabel.Text = "Drop check succesfully.";
        }
        public void dropFunction()
        {
            server.dropFunction(current.name,current.parent.name);
            Messagelabel.Text = "Drop function succesfully.";
        }
        public void dropDatabase()
        {
            server.dropDataBase(current.name);
            Messagelabel.Text = "Drop Database succesfully.";
        }
        
        private void treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            current = (MyTreeNode)treeView.SelectedNode;
            triggerActionOnNode();
        }

        //listar
        private void setUsersInTree()
        {
            DataTable dt = server.getUsers();
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.LOGIN, row["name"].ToString());
                current.Nodes.Add(treeNode);
            }
            current.Expand();
        }
        private void setUsersInDatabaseTree()
        {
            DataTable dt = server.getUsersDatabase(current.parent.name);
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.USER, row["UserName"].ToString());
                treeNode.setParent(current.parent);
                current.Nodes.Add(treeNode);
            }
            current.Expand();
        }
        private void triggerActionOnNode()
        {
            if (current == null)
                throw new TreeNodeSelectionException("No node has been selected");
            try
            {
                current.Nodes.Clear();
                dict[current.type]();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void setViewsInTree()
        {
            DataTable dt = server.getViews(current.parent.name);
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.VIEW, row["Schema_Name"].ToString()+"."+row["name"].ToString());
                treeNode.setParent(current.parent);
                current.Nodes.Add(treeNode);
            }
            current.Expand();
        }
        private void setFunctionsInTree()
        {
            DataTable dt = server.getFunctions(current.parent.name);
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.FUNCTION, row["Schema_Name"].ToString()+"."+row["name"].ToString());
                treeNode.setParent(current.parent);
                current.Nodes.Add(treeNode);
            }
            current.Expand();
        }
        private void setDatabasesInTree()
        {
            DataTable dt = server.getDatabases();
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.DATABASE, row["Name"].ToString());
                treeNode.Nodes.AddRange(setDataBaseTree(treeNode));
                current.Nodes.Add(treeNode);
            }
            current.Expand();
        }

        private void setTablesInTree()
        {
            DataTable dt = server.getTables(current.parent.name);
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.TABLE, row["Name"].ToString());
                treeNode.setParent(current.parent);
                treeNode.Nodes.AddRange(setTableTree(treeNode));
                current.Nodes.Add(treeNode);
            }
            current.Expand();
        }

        private void setIndexesInTree()
        {
            DataTable dt = server.getIndexes(current.parent.name, current.parent.parent.name);
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.INDEX, row["name"].ToString());
                treeNode.setParent(current.parent);
                current.Nodes.Add(treeNode);
            }
            current.Expand();
        }

        private void setTriggersInTree()
        {
            DataTable dt = server.getTriggers(current.parent.name, current.parent.parent.name);
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.TRIGGER, row["name"].ToString());
                treeNode.setParent(current.parent);
                current.Nodes.Add(treeNode);
            }
            current.Expand();
        }

        private void setChecksInTree()
        {
            DataTable dt = server.getChecks(current.parent.name, current.parent.parent.name);
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.CHECK, row["name"].ToString());
                treeNode.setParent(current.parent);
                current.Nodes.Add(treeNode);
            }
            current.Expand();
        }

        private void setStoreProceduresInTree()
        {
            DataTable dt = server.getStoredProcedures(current.parent.name);
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.STOREDPROCEDURE, row["Schema_Name"].ToString() + "." +row["name"].ToString());
                treeNode.setParent(current.parent);
                current.Nodes.Add(treeNode);
            }
            current.Expand();
        }

        private MyTreeNode[] setTableTree(MyTreeNode parent)
        {
            var triggers = new MyTreeNode(ObjectType.TRIGGERS_FOLDER, "Triggers");
            triggers.setParent(parent);
            var checks = new MyTreeNode(ObjectType.CHECKS_FOLDER, "Checks");
            checks.setParent(parent);
            var indexes = new MyTreeNode(ObjectType.INDEXES_FOLDER, "Indexes");
            indexes.setParent(parent);
            MyTreeNode[] array = new MyTreeNode[]
                {
                indexes,
                triggers,
                checks,
                };
            return array;
        }


        private MyTreeNode[] setDataBaseTree(MyTreeNode parent)
        {
            var tables = new MyTreeNode(ObjectType.TABLES_FOLDER, "Tables");
            tables.setParent(parent);
            var sp = new MyTreeNode(ObjectType.STOREDPROCEDURES_FOLDER, "Stored Procedures");
            sp.setParent(parent);
            var functions = new MyTreeNode(ObjectType.FUNCTIONS_FOLDER, "Functions");
            functions.setParent(parent);
            var views = new MyTreeNode(ObjectType.VIEWS_FOLDER, "Views");
            views.setParent(parent);
            var users = new MyTreeNode(ObjectType.USER_FOLDER, "Users");
            users.setParent(parent);
            MyTreeNode[] array = new MyTreeNode[]
            {
                tables,
                sp,
                functions,
                views,
                users
            };
            return array;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            server.closeConnection();
            Application.Exit();
        }

        private void Selectbutton_Click(object sender, EventArgs e)
        {
            if (current.type == ObjectType.TABLE) {
                var dt = server.getTableData(current.name, current.parent.name);
                dataGridView.DataSource = dt;
                nodePlaceHolder = current;
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            current = (MyTreeNode)treeView.SelectedNode;
        }

        private void NewConnectionbutton_Click(object sender, EventArgs e)
        {
            server.closeConnection();
            treeView.Nodes.Clear();
            formLogin = new FormLogin(ref server, load);
            formLogin.Owner = this;
            formLogin.Show();
        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            if (nodePlaceHolder != null)
            {
                server.updateTable(nodePlaceHolder.name, nodePlaceHolder.parent.name, (DataTable)(dataGridView.DataSource));
                Messagelabel.Text = "Table updated";
            }
        }

        private void Dropbutton_Click(object sender, EventArgs e)
        {
            if (current == null)
                return;

            try
            {
                drop_dict[current.type]();
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void Disconnectbutton_Click(object sender, EventArgs e)
        {
            server.closeConnection();
            treeView.Nodes.Clear();
        }

        
    }
}
