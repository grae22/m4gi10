using System;
using System.Collections.Generic;
using System.Linq;

namespace m4gi10.Logic
{
  internal static class TrackFileRenamer
  {
    //---------------------------------------------------------------------------------------------

    public static IEnumerable<RenamedTrackFile> GetRenamedTrackFiles(
      IEnumerable<ITrackFile> trackFiles,
      int artistNameMaxLength = 6,
      int albumNameMaxLength = 6)
    {
      var renamedTracks = new List<RenamedTrackFile>();
      var albumsByArtist = GetAlbumsByArtist(trackFiles);

      foreach (var artist in albumsByArtist.Keys)
      {
        AddRenamedAlbumTracksToList(
          artist,
          albumsByArtist[artist],
          trackFiles,
          renamedTracks,
          artistNameMaxLength,
          albumNameMaxLength);
      }

      return renamedTracks.OrderBy(rt => rt.NewFilename);
    }

    //---------------------------------------------------------------------------------------------

    private static IDictionary<string, List<string>> GetAlbumsByArtist(IEnumerable<ITrackFile> tracks)
    {
      var albumsByArtist = new Dictionary<string, List<string>>();

      foreach (var track in tracks)
      {
        var artist = track.Artist;
        var album = track.Album;

        if (!albumsByArtist.ContainsKey(artist))
        {
          albumsByArtist.Add(artist, new List<string>());
        }

        if (albumsByArtist[artist].Contains(album))
        {
          continue;
        }

        albumsByArtist[artist].Add(album);
      }

      return albumsByArtist;
    }

    //---------------------------------------------------------------------------------------------

    private static void AddRenamedAlbumTracksToList(
      string artist,
      List<string> albums,
      IEnumerable<ITrackFile> trackFiles,
      List<RenamedTrackFile> renamedTracks,
      int artistNameMaxLength,
      int albumNameMaxLength)
    {
      foreach (var album in albums)
      {
        var albumTracks =
          trackFiles
            .Where(a =>
              a.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase) &&
              a.Album.Equals(album, StringComparison.OrdinalIgnoreCase))
            .OrderBy(t => t.TrackNumber);

        int trackNumber = 1;

        foreach (var track in albumTracks)
        {
          RenameTrackAndAddToList(
            track,
            trackNumber++,
            renamedTracks,
            artistNameMaxLength,
            albumNameMaxLength);
        }
      }
    }

    //---------------------------------------------------------------------------------------------

    private static void RenameTrackAndAddToList(
      ITrackFile track,
      int trackNumber,
      List<RenamedTrackFile> renamedTracks,
      int artistNameMaxLength,
      int albumNameMaxLength)
    {
      var newArtistName = $"{track.Artist.Replace(" ", "")}";
      var newAlbum = $"{track.Album.Replace(" ", "")}";

      if (newArtistName.Length > artistNameMaxLength)
      {
        newArtistName = newArtistName.Substring(0, artistNameMaxLength);
      }

      if (newAlbum.Length > albumNameMaxLength)
      {
        newAlbum = newAlbum.Substring(0, albumNameMaxLength);
      }

      string newName;

      while (true)
      {
        newName = $"{newArtistName}_{newAlbum}_{trackNumber:00}{track.FileExtension}";

        bool nameAlreadyExists =
          renamedTracks.Exists(rt => rt.NewFilename.Equals(newName, StringComparison.OrdinalIgnoreCase));

        if (nameAlreadyExists)
        {
          trackNumber++;
          continue;
        }

        break;
      }
      
      renamedTracks.Add(new RenamedTrackFile(track, newName));
    }

    //---------------------------------------------------------------------------------------------
  }
}