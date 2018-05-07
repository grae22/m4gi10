using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Windows.Forms;
using m4gi10.Logic;
using m4gi10.Utils;

namespace m4gi10.Ui
{
  public partial class MainForm : Form
  {
    //---------------------------------------------------------------------------------------------

    public MainForm()
    {
      InitializeComponent();
    }

    //---------------------------------------------------------------------------------------------

    private void uiScanMusicFolder_Click(object sender, EventArgs e)
    {
      var files = Directory.GetFiles(uiMusicFolder.Text, "*.mp3", SearchOption.AllDirectories);
      var tracks = new List<ITrackFile>();
      var fileSystem = new FileSystem();

      foreach (var filename in files)
      {
        var filePropertyRetriever = new FileExtendedPropertyRetriever(filename);

        tracks.Add(new TrackFile(filename, fileSystem, filePropertyRetriever));
      }

      var renamedTracks = TrackFileRenamer.GetRenamedTrackFiles(tracks);

      uiFiles.Items.Clear();

      foreach (var track in renamedTracks)
      {
        uiFiles.Items.Add(track);
      }
    }

    //---------------------------------------------------------------------------------------------

    private void uiGo_Click(object sender, EventArgs e)
    {
      if (!Directory.Exists(uiOutputFolder.Text))
      {
        Directory.CreateDirectory(uiOutputFolder.Text);
      }

      foreach (RenamedTrackFile track in uiFiles.CheckedItems)
      {
        File.Copy(
          track.File.Filename,
          $@"{uiOutputFolder.Text}\{track.NewFilename}");
      }
    }

    //---------------------------------------------------------------------------------------------

    private void uiClearSelection_Click(object sender, EventArgs e)
    {
      foreach (int i in uiFiles.CheckedIndices)
      {
        uiFiles.SetItemCheckState(i, CheckState.Unchecked);
      }
    }

    //---------------------------------------------------------------------------------------------
  }
}
