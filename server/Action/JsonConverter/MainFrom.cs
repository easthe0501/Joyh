using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Action.Model;
using System.IO;
using Action.Utility;

namespace JsonConverter
{
    public partial class MainFrom : Form
    {
        public MainFrom()
        {
            InitializeComponent();
        }

        private void txtJson_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (files.Length > 0)
                {
                    var file = files[0];
                    if (file.ToLower().EndsWith(".json"))
                    {
                        e.Effect = DragDropEffects.All;
                        return;
                    }
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void txtJson_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (files.Length > 0)
                LoadDocument(files[0]);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            BuildJson();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BuildJson();
            SaveDocument(this.Text);
        }

        private void LoadDocument(string file)
        {
            this.Text = file;
            txtJson.Text = File.ReadAllText(file);

            var type = TypeHelper.GetType("Action.Model", "Action.Model."
                + new FileInfo(file).Name.Replace(".json", ""));
            pgObject.SelectedObject = JsonHelper.FromJson(type, txtJson.Text);
        }

        private void BuildJson()
        {
            txtJson.Text = JsonHelper.ToJson(pgObject.SelectedObject);
        }

        private void SaveDocument(string file)
        {
            File.WriteAllText(file, txtJson.Text);
        }
    }
}
