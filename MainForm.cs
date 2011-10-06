using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace PatchBalance
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            if (files!=null && files.Length > 0)
            {
                WeightPatch(files[0]);
            }
        }

        private void WeightPatch(string fileName)
        {
            var fileInfo = new FileInfo(fileName);

            var contents = File.ReadAllLines(fileInfo.FullName);

            var additions = contents.Where(l => l.StartsWith("+")).Count();
            var deletions = contents.Where(l => l.StartsWith("-")).Count();

            lblWeight.Text = string.Format("Weight is: {0}", additions - deletions);
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
