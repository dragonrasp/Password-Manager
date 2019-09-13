using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using Newtonsoft.Json;
using System.ComponentModel;

namespace PassManager
{
    public static class TranslationTools
    {
        public class TranslationPair
        {
            public string Text
            {
                get;
                set;
            }
            public string Tooltip
            {
                get;
                set;
            }
            public TranslationPair()
            {
                Text = "";
                Tooltip = "";
            }

            public TranslationPair(string text, string tooltip)
            {
                Text = text;
                Tooltip = tooltip;
            }
        }

       


        static Control FindByNameControl(Control C, string name)
        {
            if (C.Name == name)
            {
                return C;
            }
            else
            {
                foreach (Control cc in C.Controls)
                {
                    Control res = FindByNameControl(cc, name);
                    if (res != null)
                        return res;
                }
            }
            return null;
        }

        static ToolStripDropDownItem FindByNameDropDownItem(Control C, string name)
        {


            if (C.GetType() == typeof(MenuStrip))
            {
                MenuStrip MS = C as MenuStrip;
                foreach (ToolStripMenuItem MI in MS.Items)
                {
                    for (int i = 0; i < MI.DropDownItems.Count; i++)
                    {
                        if (MI.DropDownItems[i].GetType() != typeof(ToolStripSeparator))
                        {
                            if (MI.DropDownItems[i].Name == name)
                                return (ToolStripDropDownItem)MI.DropDownItems[i];
                        }
                    }
                }
            }
            else
            {
                foreach (Control CC in C.Controls)
                {
                    ToolStripDropDownItem tmp = FindByNameDropDownItem(CC, name);
                    if (tmp != null)
                        return tmp;
                }

            }

            return null;
        }

        static ToolStripMenuItem FindByNameMenuItem(Control C, string name)
        {
            if (C.GetType() == typeof(MenuStrip))
            {
                MenuStrip MS = C as MenuStrip;
                foreach (ToolStripMenuItem TSMI in MS.Items)
                {
                    if (TSMI.Name == name)
                        return TSMI;
                }
            }
            else
            {
                foreach (Control CC in C.Controls)
                {
                    ToolStripMenuItem tmp = FindByNameMenuItem(CC, name);
                    if (tmp != null)
                        return tmp;
                }
            }
            return null;
        }

        static DataGridViewColumn FindByNameColumn(Control C, string name)
        {
            if (C.GetType() == typeof(DataGridView))
            {
                DataGridView DGV = C as DataGridView;
                foreach (DataGridViewColumn Col in DGV.Columns)
                {
                    if (Col.Name == name)
                        return Col;
                }
            }
            else
            {
                foreach (Control CC in C.Controls)
                {
                    DataGridViewColumn tmp = FindByNameColumn(CC, name);
                    if (tmp != null)
                        return tmp;
                }

            }

            return null;
        }

        static void CollectNames(ref Dictionary<string, TranslationPair> D, Control C, ToolTip t)
        {
            if (C.Controls != null)
            {
                foreach (Control CC in C.Controls)
                    CollectNames(ref D, CC, t);
            }
            if (C.GetType() == typeof(DataGridView))
            {
                DataGridView DGV = C as DataGridView;
                for (int i = 0; i < DGV.Columns.Count; i++)
                {
                    if (!D.ContainsKey(DGV.Columns[i].Name))
                    {
                        D.Add(DGV.Columns[i].Name, new TranslationPair(DGV.Columns[i].HeaderText, DGV.Columns[i].ToolTipText));
                    }
                }
            }
            if (C.GetType() == typeof(MenuStrip))
            {
                MenuStrip MS = C as MenuStrip;

                foreach (ToolStripMenuItem MI in MS.Items)
                {
                    if (!D.ContainsKey(MI.Name))
                    {
                        D.Add(MI.Name, new TranslationPair(MI.Text, null));

                        for (int i = 0; i < MI.DropDownItems.Count; i++)
                        {
                            if (MI.DropDownItems[i].GetType() != typeof(ToolStripSeparator))
                            {
                                if (!D.ContainsKey(MI.DropDownItems[i].Name))
                                    D.Add(MI.DropDownItems[i].Name, new TranslationPair(MI.DropDownItems[i].Text, null));
                            }
                        }


                    }
                }
            }
            if (!D.ContainsKey(C.Name))
            {
                D.Add(C.Name, new TranslationPair(C.Text, t?.GetToolTip(C)));
            }
        }



        static ToolTip GetToolTipFromForm(Form f)
        {
            Type formType = f.GetType();
            FieldInfo fieldInfo = formType.GetField("components", BindingFlags.Instance | BindingFlags.NonPublic);
            IContainer parent = (IContainer)fieldInfo.GetValue(f);
            List<ToolTip> ToolTipList = parent?.Components.OfType<ToolTip>().ToList();
            ToolTip t = null;
            if (ToolTipList != null)
            {
                t = ToolTipList[0];
            }
            return t;
        }

        public static void CreateTranslationFile(string filename, bool fulllist)
        {

            Dictionary<string, Dictionary<string, TranslationPair>> forms = new Dictionary<string, Dictionary<string, TranslationPair>>();


            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            for (int i = 0; i < types.Length; i++)
            {
                if (types[i].BaseType == typeof(Form))
                {
                    forms.Add(types[i].Name, new Dictionary<string, TranslationPair>());
                }
            }

            foreach (string formname in forms.Keys)
            {
                Form f = Activator.CreateInstance(Type.GetType("PassManager." + formname)) as Form;



                ToolTip t = GetToolTipFromForm(f);


                Dictionary<string, TranslationPair> collection = forms[formname];
                CollectNames(ref collection, f, t);

                if (!fulllist)
                {
                    for (int i = collection.Keys.Count - 1; i >= 0; i--)
                    {
                        if (collection[collection.Keys.ElementAt(i)].Text == "" && collection[collection.Keys.ElementAt(i)].Tooltip == "")
                            collection.Remove(collection.Keys.ElementAt(i));
                    }
                }
                f.Dispose();
            }






            string towrite = JsonConvert.SerializeObject(forms, Formatting.Indented);


            StreamWriter SW = new StreamWriter(filename);
            SW.Write(towrite);
            SW.Close();
        }

        public static Dictionary<string, Dictionary<string, TranslationPair>> ReadTranslationFile(string filename)
        {
            StreamReader SR = new StreamReader(filename);
            Dictionary<string, Dictionary<string, TranslationPair>> TransDictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, TranslationPair>>>(SR.ReadToEnd());
            SR.Close();
            return TransDictionary;


        }

        public static void ApplyTranslation(Control F, Dictionary<string, TranslationPair> TransDictionary)
        {
            ToolTip t = GetToolTipFromForm((Form)F);
            foreach (string key in TransDictionary.Keys)
            {
                Control c = FindByNameControl(F, key);
                if (c != null)
                {
                    c.Text = TransDictionary[key].Text;
                }
                else
                {
                    ToolStripDropDownItem DDI = FindByNameDropDownItem(F, key);
                    if (DDI != null)
                        DDI.Text = TransDictionary[key].Text;
                    else
                    {
                        DataGridViewColumn DGVC = FindByNameColumn(F, key);
                        if (DGVC != null)
                            DGVC.HeaderText = TransDictionary[key].Text;
                        else
                        {
                            ToolStripMenuItem TSMI = FindByNameMenuItem(F, key);
                            if (TSMI != null)
                                TSMI.Text = TransDictionary[key].Text;

                        }
                    }
                }
                if (t != null && TransDictionary[key].Tooltip != null && c != null)
                    t.SetToolTip(c, TransDictionary[key].Tooltip);
            }
        }
    }
}
