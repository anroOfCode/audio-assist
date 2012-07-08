using System;
using System.Windows.Forms;


namespace Fusao.AudioAssist.SoundPlayer.TestHarness
{
    public partial class frmMain : Form
    {
        SoundPlayer _soundPlayer = new SoundPlayer();
        public frmMain()
        {
            InitializeComponent();
            this.Width = 220;

            int numberOfSoundPlayerElements = Enum.GetNames(typeof(SoundType)).Length;
            this.Height = numberOfSoundPlayerElements * 50 + 50;

            int verticalButtonIndex = 0;
            foreach (SoundType st in Enum.GetValues(typeof(SoundType)))
            {
                Button newSoundPlayingButton = new Button()
                {
                    // Some hard-coded values to create buttons of an
                    // approximately correct size.
                    Width = 180,
                    Height = 40,
                    Top = verticalButtonIndex * 50 + 5,
                    Left = 10,
                    Text = "On " + st.ToString(),
                    Parent = this,
                    Tag = st
                };
                newSoundPlayingButton.Click += new EventHandler(b_Click);
                verticalButtonIndex++;
            }
        }

        void b_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            SoundType s = (SoundType)b.Tag;
            _soundPlayer.PlayNoise(s);
        }
    }
}
