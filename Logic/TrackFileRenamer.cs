using System.Collections.Generic;

namespace m4gi10.Logic
{
  internal static class TrackFileRenamer
  {
    //---------------------------------------------------------------------------------------------

    public static IEnumerable<RenamedTrackFile> GetRenamedTrackFiles(IEnumerable<ITrackFile> trackFiles)
    {
      var renamedTrackFiles = new List<RenamedTrackFile>();

      foreach (ITrackFile trackFile in trackFiles)
      {
        var newName = $"{trackFile.Artist.Replace(" ", "")}_{trackFile.TrackNumber}";

        renamedTrackFiles.Add(new RenamedTrackFile(trackFile, newName));
      }

      return renamedTrackFiles;
    }

    //---------------------------------------------------------------------------------------------
  }
}