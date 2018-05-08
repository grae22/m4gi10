using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading;
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
      var writeToConsole = new WriteToConsoleDelegate(WriteToConsole);

#if true
      var filesToRename = new ConcurrentStack<string>(files);

      BackgroundWorker[] backgroundWorkers = new BackgroundWorker[8];
      for (var i = 0; i < backgroundWorkers.Length; i++)
      {
        backgroundWorkers[i] = new BackgroundWorker
        {
          WorkerSupportsCancellation = true
        };
      }

      var tracksListLock = new object();

      while (!filesToRename.IsEmpty)
      {
        for (var i = 0; i < backgroundWorkers.Length; i++)
        {
          if (backgroundWorkers[i].IsBusy && !backgroundWorkers[i].CancellationPending)
          {
            continue;
          }

          backgroundWorkers[i] = new BackgroundWorker
          {
            WorkerSupportsCancellation = true
          };

          var worker = backgroundWorkers[i];

          worker.DoWork += (sender1, e1) =>
          {
            var filename = string.Empty;

            try
            {
              if (!filesToRename.TryPop(out filename))
              {
                return;
              }

              var filePropertyRetriever = new FileExtendedPropertyRetriever(filename);

              lock (tracksListLock)
              {
                tracks.Add(new TrackFile(filename, fileSystem, filePropertyRetriever));
              }
            }
            catch (Exception ex)
            {
              writeToConsole.Invoke($"{filename} : {ex.Message}");
              Console.WriteLine(ex);
            }
          };

          worker.RunWorkerAsync();
        }

        Application.DoEvents();
      }

      while (backgroundWorkers.Any(bw => bw.IsBusy))
      {
        Thread.Sleep(1000);
        Application.DoEvents();
      }
#else
      foreach (var filename in files)
      {
        var filePropertyRetriever = new FileExtendedPropertyRetriever(filename);

        tracks.Add(new TrackFile(filename, fileSystem, filePropertyRetriever));
      }
#endif
      var renamedTracks = TrackFileRenamer.GetRenamedTrackFiles(tracks);

      uiFiles.Items.Clear();

      foreach (var track in renamedTracks)
      {
        uiFiles.Items.Add(track);
      }

      writeToConsole.Invoke("Scanning complete.");
    }

    //---------------------------------------------------------------------------------------------

    private void uiGo_Click(object sender, EventArgs e)
    {
      if (!Directory.Exists(uiOutputFolder.Text))
      {
        Directory.CreateDirectory(uiOutputFolder.Text);
      }

      var writeToConsole = new WriteToConsoleDelegate(WriteToConsole);

      foreach (RenamedTrackFile track in uiFiles.CheckedItems)
      {
        var newFilename = $@"{uiOutputFolder.Text}\{track.NewFilename}";

        if (File.Exists(newFilename))
        {
          writeToConsole.Invoke($"File already exists : {newFilename}");
          continue;
        }

        File.Copy(track.File.Filename, $"{newFilename}");
      }

      writeToConsole.Invoke("Copy complete.");
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

    private delegate void WriteToConsoleDelegate(string message);

    private void WriteToConsole(string message)
    {
      uiConsole.AppendText($"{message}{Environment.NewLine}");
    }

    //---------------------------------------------------------------------------------------------
  }
}
