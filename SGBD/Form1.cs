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
        public const string DESIGN_TEXT_LABEL = "Design of table ";
        public const int SQL_TAB_INDEX= 0;
        public const int DESIGN_TAB_INDEX = 1;
        public const int DDL_TAB_INDEX = 2;
        SqlRepository server;
        FormLogin formLogin=null;
        MyTreeNode current = null, nodePlaceHolder = null;
        Dictionary<ObjectType, Action> dict, drop_dict,ddl_dict, create_dict;
        public Form1()
        {
            InitializeComponent();
            server = new SqlRepository();
            formLogin = new FormLogin(ref server,load);
            formLogin.Owner = this;
            formLogin.Show();
            initFunctDictionary();
            initDropDictionary();
            initDDLDictionary();
            initCreateDictionary();
        }

        private void initCreateDictionary()
        {
            create_dict = new Dictionary<ObjectType, Action>();
            create_dict[ObjectType.TABLES_FOLDER] = createTable;
            create_dict[ObjectType.DATABASE_FOLDER] = createDatabase;
            create_dict[ObjectType.TRIGGERS_FOLDER] = createTrigger;
            create_dict[ObjectType.VIEWS_FOLDER] = createView;
            create_dict[ObjectType.INDEXES_FOLDER] = createIndex;
            create_dict[ObjectType.USER_FOLDER] = createUser;
            create_dict[ObjectType.LOGIN_FOLDER] = createLogin;
            create_dict[ObjectType.STOREDPROCEDURES_FOLDER] = createStoreProcedure;
            create_dict[ObjectType.FUNCTIONS_FOLDER] = createFunction;
            create_dict[ObjectType.FOREIGN_KEY_FOLDER] = createForeignKey;
            create_dict[ObjectType.CHECKS_FOLDER] = createCheck;
        }
        private void initDDLDictionary()
        {
            ddl_dict = new Dictionary<ObjectType, Action>();
            ddl_dict[ObjectType.TRIGGER] = DDLTrigger;
            ddl_dict[ObjectType.CHECK] = DDLCheck;
            ddl_dict[ObjectType.INDEX] = DDLIndex;
            ddl_dict[ObjectType.FOREIGN_KEY] = DDLForeignKey;
            ddl_dict[ObjectType.STOREDPROCEDURE] = DDLSP_FN_Views;
            ddl_dict[ObjectType.FUNCTION] = DDLSP_FN_Views;
            ddl_dict[ObjectType.VIEW] = DDLSP_FN_Views;
            ddl_dict[ObjectType.USER] = DDLUser;
            ddl_dict[ObjectType.LOGIN] = DDLLogin;
            ddl_dict[ObjectType.TABLE] = DDLTable;
            ddl_dict[ObjectType.DATABASE] = DDLDataTable;
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
            drop_dict[ObjectType.FOREIGN_KEY] = dropForeignKey;

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
            dict[ObjectType.FOREIGN_KEY_FOLDER] = setForeignKeysInTree;
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
                SQLtextBox.ReadOnly = false;
            }
        }

        //ddls
        private void DDLTrigger()
        {
            DataTable dt = server.getTriggerDDL(current.name, current.parent.parent.name);
            List<string> strings = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                strings.Add(row["Text"].ToString());
            }
            DDLtextBox.Lines = strings.ToArray();
            tabControl1.SelectedIndex = DDL_TAB_INDEX;
        }
        private void DDLSP_FN_Views()
        {
            DataTable dt = server.getSP_FN_Views_DDL(current.name, current.parent.name);
            List<string> strings = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                strings.Add(row["Text"].ToString());
            }
            DDLtextBox.Lines = strings.ToArray();
            tabControl1.SelectedIndex = DDL_TAB_INDEX;
        }
        private void DDLDataTable()
        {
            string dt = server.getDatabaseDDL(current.name);
            DDLtextBox.Text = dt;
            tabControl1.SelectedIndex = DDL_TAB_INDEX;
        }
        private void DDLCheck()
        {
            DataTable dt = server.getCheckDDL(current.name, current.parent.parent.name);
            List<string> strings = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                strings.Add(row["Text"].ToString());
            }
            DDLtextBox.Lines = strings.ToArray();
            tabControl1.SelectedIndex = DDL_TAB_INDEX;
        }
        private void DDLIndex()
        {
            DataTable dt = server.getIndexDDL(current.name, current.parent.parent.name);
            List<string> strings = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                strings.Add(row["Text"].ToString());
            }
            DDLtextBox.Lines = strings.ToArray();
            tabControl1.SelectedIndex = DDL_TAB_INDEX;
        }
        private void DDLForeignKey()
        {
            DataTable dt = server.getForeignKeyDDL(current.name, current.parent.name,current.parent.parent.name);
            List<string> strings = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                strings.Add(row["Text"].ToString());
            }
            DDLtextBox.Lines = strings.ToArray();
            tabControl1.SelectedIndex = DDL_TAB_INDEX;
        }
        private void DDLUser()
        {
            DataTable dt = server.getUserDDL(current.name, current.parent.name);
            List<string> strings = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                strings.Add(row["Text"].ToString());
            }
            DDLtextBox.Lines = strings.ToArray();
            tabControl1.SelectedIndex = DDL_TAB_INDEX;
        }
        private void DDLLogin()
        {
            DataTable dt = server.getLoginDDL(current.name);
            List<string> strings = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                strings.Add(row["Text"].ToString());
            }
            DDLtextBox.Lines = strings.ToArray();
            tabControl1.SelectedIndex = DDL_TAB_INDEX;
        }
        private void DDLTable()
        {
            DataTable dt = server.getTableDDL(current.name, current.parent.name);
            List<string> strings = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                strings.Add(row["Text"].ToString());
            }
            DDLtextBox.Lines = strings.ToArray();
            checkForIndexes(current);
            checkForForeignKeys(current);
            checkForChecks(current);
            tabControl1.SelectedIndex = DDL_TAB_INDEX;
        }

        private void checkForForeignKeys(MyTreeNode current)
        {
            MyTreeNode foreignKeys = current.Nodes[3] as MyTreeNode;
            //listar 
            DataTable dt = server.getForeignKeys(foreignKeys.parent.name, foreignKeys.parent.parent.name);
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.FOREIGN_KEY, row["name"].ToString());
                treeNode.setParent(foreignKeys.parent);
                foreignKeys.Nodes.Add(treeNode);
            }
            //ddl
            List<string> strings = new List<string>();
            foreach (Object o in foreignKeys.Nodes)
            {
                MyTreeNode child = o as MyTreeNode;
                dt = server.getForeignKeyDDL(child.name, child.parent.name, child.parent.parent.name);
                foreach (DataRow row in dt.Rows)
                {
                    strings.Add(row["Text"].ToString());
                }
                strings.Add("\n");
            }
            if (strings.Count > 0)
            {
                var z = new string[strings.Count + DDLtextBox.Lines.Length];
                DDLtextBox.Lines.CopyTo(z, 0);
                strings.ToArray().CopyTo(z, DDLtextBox.Lines.Length);
                DDLtextBox.Lines = z;
            }
        }

        private void checkForIndexes(MyTreeNode current)
        {
            MyTreeNode indexes = current.Nodes[0] as MyTreeNode;

            //listar
            DataTable dt = server.getIndexes(indexes.parent.name, indexes.parent.parent.name);
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.INDEX, row["name"].ToString());
                treeNode.is_primary = Convert.ToInt32(row["is_primary_key"]);
                treeNode.setParent(indexes.parent);
                indexes.Nodes.Add(treeNode);
            }
            //ddl
            List<string> strings = new List<string>();
            foreach (Object o in indexes.Nodes)
            {
                MyTreeNode child = o as MyTreeNode;
                dt = server.getIndexDDL(child.name, child.parent.parent.name);
                foreach (DataRow row in dt.Rows)
                {
                    strings.Add(row["Text"].ToString());
                }
                strings.Add("\n");
            }
            if (strings.Count > 0)
            {
                var z = new string[strings.Count + DDLtextBox.Lines.Length];
                DDLtextBox.Lines.CopyTo(z, 0);
                strings.ToArray().CopyTo(z, DDLtextBox.Lines.Length);
                DDLtextBox.Lines = z;
            }
        }

        private void checkForChecks(MyTreeNode current)
        {
            MyTreeNode checks = current.Nodes[2] as MyTreeNode;
            //listar
            DataTable dt = server.getChecks(checks.parent.name, checks.parent.parent.name);
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.CHECK, row["name"].ToString());
                treeNode.setParent(checks.parent);
                checks.Nodes.Add(treeNode);
            }
            //ddl
            List<string> strings = new List<string>();
            foreach (Object o in checks.Nodes)
            {
                MyTreeNode child = o as MyTreeNode;
                dt = server.getCheckDDL(child.name, child.parent.parent.name);
                foreach (DataRow row in dt.Rows)
                {
                    strings.Add(row["Text"].ToString());
                }
                strings.Add("\n");
            }
            if (strings.Count > 0)
            {
                var z = new string[strings.Count + DDLtextBox.Lines.Length];
                DDLtextBox.Lines.CopyTo(z, 0);
                strings.ToArray().CopyTo(z, DDLtextBox.Lines.Length);
                DDLtextBox.Lines = z;
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
            server.dropIndex(current.name, current.parent.name, current.parent.parent.name, current.is_primary);
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
        public void dropForeignKey()
        {
            server.dropForeignKey(current.name,current.parent.name, current.parent.parent.name);
            Messagelabel.Text = "Drop foreign key succesfully.";
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
                treeNode.setParent(current.parent);
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
                MessageBox.Show(this,ex.Message+"aqui", ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                treeNode.is_primary = Convert.ToInt32(row["is_primary_key"]);
                treeNode.setParent(current.parent);
                current.Nodes.Add(treeNode);
            }
            current.Expand();
        }
        private void setForeignKeysInTree()
        {
            DataTable dt = server.getForeignKeys(current.parent.name, current.parent.parent.name);
            foreach (DataRow row in dt.Rows)
            {
                MyTreeNode treeNode = new MyTreeNode(ObjectType.FOREIGN_KEY, row["name"].ToString());
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
            var f = new MyTreeNode(ObjectType.FOREIGN_KEY_FOLDER, "Foreign keys");
            f.setParent(parent);
            MyTreeNode[] array = new MyTreeNode[]
                {
                indexes,
                triggers,
                checks,
                f,
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
            try
            {
                if (current.type == ObjectType.TABLE)
                {
                    var dt = server.getTableData(current.name, current.parent.name);
                    dataGridView.DataSource = dt;
                    nodePlaceHolder = current;

                    dt = server.getTableDesign(current.name, current.parent.name);
                    DesigndataGridView.DataSource = dt;
                    tabControl1.SelectedIndex = DESIGN_TAB_INDEX;
                    Designlabel.Text = DESIGN_TEXT_LABEL + current.name;
                }
            }catch(Exception ex)
            {
                Messagelabel.Text = "ERROR WHILE SHOWING";
                if (Messagelabel.ForeColor != System.Drawing.Color.Red)
                {
                    Messagelabel.ForeColor = System.Drawing.Color.Red;
                }
                MessageBox.Show(this, ex.ToString(), ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void DDLbutton_Click(object sender, EventArgs e)
        {
            if (current == null)
                return;
            try
            {
                ddl_dict[current.type]();
            }catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Playbutton_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = SQLtextBox.Text;
                DataTable dt = server.executeSQL(sql);
                dataGridView.DataSource = dt;
                Messagelabel.Text = "Query executed successfully";
                if (Messagelabel.ForeColor != System.Drawing.Color.Black)
                {
                    Messagelabel.ForeColor = System.Drawing.Color.Black;
                }
            }
            catch(Exception ex)
            {
                Messagelabel.Text = "SQL ERROR";
                if (Messagelabel.ForeColor != System.Drawing.Color.Red)
                {
                    Messagelabel.ForeColor = System.Drawing.Color.Red;
                }
                MessageBox.Show(this, ex.ToString(), ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Createbutton_Click(object sender, EventArgs e)
        {
            try
            {
                create_dict[current.type]();
            }catch(Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void createTable()
        {
            string s = server.createTableProto(current.parent.name);
            SQLtextBox.Text = s;
        }
        private void createDatabase()
        {
            string s = server.createDataBase();
            SQLtextBox.Text = s;
        }
        private void createTrigger()
        {
            string s = server.createTrigger();
            SQLtextBox.Text = s;
        }
        private void createView()
        {
            string s = server.createView(current.parent.name);
            SQLtextBox.Text = s;
        }
        private void createIndex()
        {
            string s = server.createIndex();
            SQLtextBox.Text = s;
        }
        private void createUser()
        {
            string s = server.createUser(current.parent.name);
            SQLtextBox.Text = s;
        }
        private void createLogin()
        {
            string s = server.createLogin();
            SQLtextBox.Text = s;
        }
        private void createStoreProcedure()
        {
            string s = server.createStoreProcedure(current.parent.name);
            SQLtextBox.Text = s;
        }
        private void createFunction()
        {
            string s = server.createFunction(current.parent.name);
            SQLtextBox.Text = s;
        }
        private void createForeignKey()
        {
            string s = server.createForeignKey(current.parent.name, current.parent.parent.name);
            SQLtextBox.Text = s;
        }
        private void createCheck()
        {
            string s = server.createCheck(current.parent.name, current.parent.parent.name);
            SQLtextBox.Text = s;
        }
        


        private void Updatebutton_Click(object sender, EventArgs e)
        {
            if (nodePlaceHolder != null)
            {
                try
                {
                    server.updateTable(nodePlaceHolder.name, nodePlaceHolder.parent.name, (DataTable)(dataGridView.DataSource));
                    Messagelabel.Text = "Table updated";
                    if (Messagelabel.ForeColor != System.Drawing.Color.Black)
                    {
                        Messagelabel.ForeColor = System.Drawing.Color.Black;
                    }
                }
                catch (Exception ex)
                {
                    Messagelabel.Text = "ERROR WHILE UPDATING";
                    if (Messagelabel.ForeColor != System.Drawing.Color.Red)
                    {
                        Messagelabel.ForeColor = System.Drawing.Color.Red;
                    }
                    MessageBox.Show(this, ex.ToString(), ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                MessageBox.Show(this, ex.ToString() + "aqui3", ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void Disconnectbutton_Click(object sender, EventArgs e)
        {
            server.closeConnection();
            treeView.Nodes.Clear();
        }
    }
}
