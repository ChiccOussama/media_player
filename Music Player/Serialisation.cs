using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Music_Player {
    /// <summary>
    /// Classe servant a serialiser les playlists
    /// </summary>
    public class Serialisation {
        Regex re = new Regex("\r\n");

        /// <summary>
        /// Sauvegarde de la liste (Playlist)
        /// </summary>
        /// <param name="listbox"></param>
        /// <param name="filename"></param>
        public void SavePlaylist(ListBox listbox, string filename) {
            try {
                var linesToSave = new List<string>();
                //Ajoute morceau par morceau les elements a sauvegarder
                for (var i = 0; listbox.Items.Count > i; i++) {
                    listbox.SelectedIndex = i;
                    var item = listbox.SelectedItem as ListBoxItem;
                    linesToSave.Add(item.Path);
                }
                WritePlaylist(filename, linesToSave);

                MessageBox.Show("Sauvé");
            //Affiche les messages suivants en cas d'erreur
            } catch (Exception ex) {
                MessageBox.Show($"Impossible d'ouvrir le fichier : {filename} pour le sauvegarder.\nProblème de droit d'accès peut-etre ?", "Probleme sauvegarde fichier", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show($"{ex}");
            }
        }

        /// <summary>
        /// Ouvre le fichier serialisé
        /// </summary>
        /// <param name="listbox"></param>
        public void OpenPlaylist(ListBox listbox) {
            var open = new OpenFileDialog();
            var result = open.ShowDialog();

            if (result == DialogResult.OK) {
                var file = open.FileName;
                try {
                    listbox.Items.Clear();

                    var lines = ReadPlaylist(file);
                    foreach (var line in lines) {
                        SetFilenames(listbox, line);
                    }
                    
                } catch (IOException) {
                    MessageBox.Show("Impossible de charger!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// Rajoute morceaux
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="linesToSave"></param>
        public void WritePlaylist(string filename, List<string> linesToSave) {
            using (var stream = new StreamWriter(filename)) {
                foreach (var line in linesToSave) {
                    if (line.Length > 0) {
                        stream.WriteLine(line);
                    }
                    
                }
                stream.Flush();
            }
        }

        /// <summary>
        /// Lit playlist
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public List<string> ReadPlaylist(string file) {
            var lines = new List<string>();

            using (var reader = new StreamReader(file)) {
                var text = reader.ReadToEnd();
                var newLines = re.Split(text);
                foreach (var item in newLines) {
                    if(item.Length > 0) {
                        lines.Add(item);
                    }
                }
            }

            return lines;
        }

        /// <summary>
        /// Ajoute morceau listbox
        /// </summary>
        /// <param name="listbox"></param>
        /// <param name="path"></param>
        public void SetFilenames(ListBox listbox, string path) {
            var item = new ListBoxItem();
			path.Replace("'", "\\'");
			var nowplaying = Path.GetFileNameWithoutExtension(path);
            item.Text = nowplaying;
            item.Name = nowplaying;
            item.Path = path;

            listbox.Items.Add(item);
        }
    }
}
