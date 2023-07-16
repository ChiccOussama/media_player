using System.IO;
using System.Windows.Forms;

namespace Music_Player {
    public class Helper {
        //obj serialisation
		Serialisation serialisation = new Serialisation();

        /// <summary>
        /// Affiche nom morceau
        /// </summary>
        /// <param name="listbox"></param>
        /// <param name="label"></param>
		public void SetNowPlayingText(ListBox listbox, Label label) {
            var playing = Path.GetFileName(listbox.SelectedItem.ToString());
            label.Text = $"{ playing }";
        }

        /// <summary>
        /// Active retour et suivant
        /// </summary>
        /// <param name="listbox"></param>
        /// <param name="nextBtn"></param>
        /// <param name="previousBtn"></param>
        public void previousNextEnabled(ListBox listbox, Button nextBtn, Button previousBtn) {
            if (listbox.SelectedIndex == (listbox.Items.Count - 1)) {
                nextBtn.Enabled = false;
                previousBtn.Enabled = true;
            } else if (listbox.SelectedIndex == 0) {
                previousBtn.Enabled = false;
                nextBtn.Enabled = true;
            } else {
                nextBtn.Enabled = true;
                previousBtn.Enabled = true;
            }
        }


        /// <summary>
        /// Active boutton
        /// </summary>
        /// <param name="enabled"></param>
        /// <param name="label"></param>
        /// <param name="pauseBtn"></param>
        /// <param name="stopBtn"></param>
        public void setButtons(bool enabled, Label label, Button pauseBtn, Button stopBtn) {
            if (enabled) {
                label.Visible = false;
                pauseBtn.Enabled = false;
                stopBtn.Enabled = false;
            } else {
                label.Visible = true;
                pauseBtn.Enabled = true;
                stopBtn.Enabled = true;
            }
        }

        /// <summary>
        /// Ajout un ou plusieurs morceaux a la liste
        /// </summary>
        /// <param name="playlist"></param>
		public void addMusic(ListBox playlist) {
			var dlg = new OpenFileDialog();
			dlg.Filter = "Music (*.mp3) | *.mp3";
			dlg.Multiselect = true;
			var result = dlg.ShowDialog();

			if (result == DialogResult.OK) {
				try {
					foreach (var file in dlg.FileNames) {
						serialisation.SetFilenames(playlist, file);
					}
				} catch {
					MessageBox.Show("Could not add file");
				}
			}
		}

        /// <summary>
        /// Boite de dialogue : sauvegarde playlist
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
		public DialogResult showSaveDialog(string filename) {
			var dialog = new SaveFileDialog();
			dialog.Filter = "Fichier data (*.dat, *.play) | *.dat, .play";
			var result = dialog.ShowDialog();

			if (result == DialogResult.OK) {
				filename = dialog.FileName;
			}

			return result;
		}
	}
}
