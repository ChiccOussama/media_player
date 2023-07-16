
//Ce document intitulé « mp3_player » issu dans ma chaine youtube
//(codes-sources.) est mis à disposition sous les termes de
//la licence Creative Commons.Vous pouvez copier, modifier des copies de cette
//source, dans les conditions fixées par la licence, tant que cette note
//apparaît clairement.

using System;
using System.Windows.Forms;


namespace Music_Player {
    public partial class interfaceForm : Form {
        MusicPlayer player = new MusicPlayer();
        Serialisation serialisation = new Serialisation();
        Helper helper = new Helper();

        private string filename = "";
        

        public interfaceForm() {
            InitializeComponent();
        }

        /// <summary>
        /// Ajout de morceau
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addMusicBtn_Click(object sender, EventArgs e) {
			helper.addMusic(playlist);
        }

        /// <summary>
        /// joue la musique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playBtn_Click(object sender, EventArgs e) {
            try {
                //item premier de la liste
                if (playlist.SelectedIndex == -1) {
                    if (playlist.Items.Count >= 1) {
                        playlist.SelectedIndex = 0;
                    }
                }

                //active bouttons
                helper.setButtons(false, label1, pauseBtn, stopBtn);
                helper.SetNowPlayingText(playlist, label1);

                //gestion des tags
                var item = playlist.SelectedItem as ListBoxItem;
				TagLib.File tagFile = TagLib.File.Create(item.Path);
				albumLabel.Text = tagFile.Tag.Album;

                //joue le morceau
				player.open(item.Path);
                player.play();
                helper.previousNextEnabled(playlist, nextBtn, previousBtn);
                //en cas d'erreur
            } catch {
                helper.setButtons(true, label1, pauseBtn, stopBtn);
                MessageBox.Show("Veuillez d'abord ouvrir un fichier .mp3!");
            }

            pictureBox2.Image = Properties.Resources.visualizer;
        }

        /// <summary>
        /// stop la musique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopBtn_Click(object sender, EventArgs e) {
            player.stop();
            label1.Visible = false;
            pauseBtn.Enabled = false;
            pictureBox2.Image = Properties.Resources.maxresdefault;
        }

        /// <summary>
        /// Met en pause la musique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseBtn_Click(object sender, EventArgs e) {
            player.pause();
        }

        /// <summary>
        /// Apres morceau liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextBtn_Click(object sender, EventArgs e) {
            try {
                player.stop();
                playlist.SelectedIndex += 1;
                playBtn.PerformClick();
            } catch {
                MessageBox.Show("Plus de chansons dans la liste!");
            }

            picture.Visible = false;
            picture.Image = Properties.Resources.booba;
            bunifuTransition1.ShowSync(picture);
            
        }

        /// <summary>
        /// Avant morceau liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousBtn_Click(object sender, EventArgs e) {
            try {
                player.stop();
                playlist.SelectedIndex -= 1;
                playBtn.PerformClick();
            } catch {
                MessageBox.Show("Plus de chansons dans la liste!");
            }
        }
		
        /// <summary>
        /// openFile puis sauvegarde playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveBtn_Click(object sender, EventArgs e) {
            if (helper.showSaveDialog(filename) != DialogResult.OK) {
                return;
            }

            serialisation.SavePlaylist(playlist, filename);
        }

        /// <summary>
        /// openFile puis charge playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadBtn_Click(object sender, EventArgs e) {
            serialisation.OpenPlaylist(playlist);
        }

        private void Form1_Load(object sender, EventArgs e) {
            //todo
        }

        private void TimerTick(object sender, EventArgs e) {
            //todo
        }
        /// <summary>
        /// double click pour lire morceau
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e) {
            
            stopBtn.PerformClick();
            playBtn.PerformClick();
            pictureBox2.Image = Properties.Resources.visualizer;
        }

        /// <summary>
        /// Efface tous les elements de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearPlaylist_Click(object sender, EventArgs e) {
            var result = MessageBox.Show("Etes-vous surs de vouloir supprimer tous les éléments de cette liste?", "", MessageBoxButtons.OKCancel);
            if(result == DialogResult.OK) {
                player.stop();
                playlist.Items.Clear();
                helper.setButtons(false, label1, pauseBtn, stopBtn);
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            helper.addMusic(playlist);
        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            label1.Text=" ";
            var result = MessageBox.Show("Etes-vous surs de vouloir supprimer tous les éléments de cette liste?", "", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                player.stop();
                playlist.Items.Clear();
                helper.setButtons(false, label1, pauseBtn, stopBtn);
            }
            pictureBox2.Image = Properties.Resources.maxresdefault;
        }

        private void playlist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}