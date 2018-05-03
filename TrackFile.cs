using System;
using System.IO.Abstractions;

namespace m4gi10
{
  internal class TrackFile
  {
    //---------------------------------------------------------------------------------------------

    public string Artist { get; private set; } = "";
    public int TrackNumber { get; private set; }

    private const string FilePropertyAlbumArtist = "System.Music.AlbumArtist";
    private const string FilePropertyArtist = "System.Music.Artist";
    private const string FilePropertyTrackNumber = "System.Music.TrackNumber";

    private string Filename { get; }
    private IFileSystem FileSystem { get; }
    private IFileExtendedPropertyRetriever FilePropertyRetriever { get; }

    //---------------------------------------------------------------------------------------------

    public TrackFile(
      string filename,
      IFileSystem fileSystem,
      IFileExtendedPropertyRetriever filePropertyRetriever)
    {
      Filename = filename;
      FileSystem = fileSystem;
      FilePropertyRetriever = filePropertyRetriever;

      ValidateFileExists();
      UpdateArtist();
      UpdateTrackNumber();
    }

    //---------------------------------------------------------------------------------------------

    private void ValidateFileExists()
    {
      if (FileSystem.File.Exists(Filename))
      {
        return;
      }

      throw new ArgumentException($"File does not exist: \"{Filename}\"");
    }

    //---------------------------------------------------------------------------------------------

    private void UpdateArtist()
    {
      Artist = GetAlbumArtistPropertyValue();

      if (!string.IsNullOrWhiteSpace(Artist))
      {
        return;
      }

      Artist = GetArtistPropertyValue();

      if (!string.IsNullOrWhiteSpace(Artist))
      {
        return;
      }

      Artist = "Unknown";
    }

    //---------------------------------------------------------------------------------------------

    private string GetAlbumArtistPropertyValue()
    {
      return FilePropertyRetriever.GetPropertyValue(FilePropertyAlbumArtist);
    }

    //---------------------------------------------------------------------------------------------

    private string GetArtistPropertyValue()
    {
      return FilePropertyRetriever.GetPropertyValue(FilePropertyArtist);
    }

    //---------------------------------------------------------------------------------------------

    private void UpdateTrackNumber()
    {
      string value = FilePropertyRetriever.GetPropertyValue(FilePropertyTrackNumber);

      if (string.IsNullOrWhiteSpace(value))
      {
        return;
      }

      if (!int.TryParse(value, out int trackNumber))
      {
        return;
      }

      TrackNumber = trackNumber;
    }

    //---------------------------------------------------------------------------------------------
  }
}