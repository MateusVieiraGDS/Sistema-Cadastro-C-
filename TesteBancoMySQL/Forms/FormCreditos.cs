using System;
using System.Windows.Forms;

namespace TesteBancoMySQL.Forms
{
    public partial class FormCreditos : Form
    {
        System.Media.SoundPlayer player;
        public FormCreditos()
        {
            InitializeComponent();
        }

        private void FormCreditos_Load(object sender, EventArgs e)
        {
            player = new System.Media.SoundPlayer(Properties.Resources.brbr_1_);
            player.PlayLooping();
        }

        private void FormCreditos_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.Stop();
            player.Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"mailto:mateusvieirap010@gmail.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://github.com/MateusVieiraGDS/Sistema-Cadastro-C-");
        }
    }
}
