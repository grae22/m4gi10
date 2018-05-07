using System;
using System.IO.Abstractions;
using m4gi10.Utils;

namespace m4gi10.Logic
{
  internal class TrackFile : ITrackFile
  {
    //---------------------------------------------------------------------------------------------

    public string Artist { get; private set; } = "";
    public string Album { get; private set; } = "";
    public string TrackName { get; private set; } = "";
    public int TrackNumber { get; private set; }
    public string FileExtension { get; private set; } = "";
    public string Filename { get; }

    private const string FilePropertyAlbumArtist = "System.Music.AlbumArtist";
    private const string FilePropertyArtist = "System.Music.Artist";
    private const string FilePropertyAlbum = "System.Music.Album";
    private const string FilePropertyTrackNumber = "System.Music.TrackNumber";
    private const string FilePropertyTrackTitle = "System.Music.Title";

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
      UpdateAlbum();
      UpdateTrackNumber();
      UpdateTrackName();
      UpdateFileExtension();
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

    private void UpdateAlbum()
    {
      Album = FilePropertyRetriever.GetPropertyValue(FilePropertyAlbum);

      if (!string.IsNullOrWhiteSpace(Album))
      {
        return;
      }

      Album = "Unknown";
    }

    //---------------------------------------------------------------------------------------------

    private void UpdateTrackName()
    {
      TrackName = FilePropertyRetriever.GetPropertyValue(FilePropertyTrackTitle);

      if (!string.IsNullOrWhiteSpace(TrackName))
      {
        return;
      }

      TrackName = "Unknown";
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

    private void UpdateFileExtension()
    {
      FileExtension = FileSystem.Path.GetExtension(Filename).ToLower();
    }

    //---------------------------------------------------------------------------------------------
  }
}