using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Perfect_Ambience
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog() { Multiselect=true,ValidateNames=true,Filter= "WMV|*.wmv|WAV|*.wav|MP3|*.mp3|MP4|*.mp4|MKV|*.mkv" }) 
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    List<MediaFile> files = new List<MediaFile>();
                    foreach(string fileName in ofd.FileNames)
                    {
                        FileInfo fi = new FileInfo(fileName);
                        files.Add(new MediaFile() { FileName = Path.GetFileNameWithoutExtension(fi.FullName),Path=fi.FullName});
                    }
                    FileList.DataSource = files;
                    FileList.ValueMember = "Path";
                    FileList.DisplayMember = "FileName";
                }
            }
        }

        private void FileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            MediaFile file = FileList.SelectedItem as MediaFile;
            if(file != null)
            {
                axWindowsMediaPlayer.URL = file.Path;
                axWindowsMediaPlayer.Ctlcontrols.play();
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            FileList.ValueMember = "Path";
            FileList.DisplayMember = "FileName";
        }
    }
}
