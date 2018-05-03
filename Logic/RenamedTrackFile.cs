namespace m4gi10.Logic
{
  internal class RenamedTrackFile
  {
    //---------------------------------------------------------------------------------------------

    public TrackFile File { get; }
    public string NewFilename { get; }

    //---------------------------------------------------------------------------------------------

    public RenamedTrackFile(TrackFile trackFile, string newFilename)
    {
      File = trackFile;
      NewFilename = newFilename;
    }

    //---------------------------------------------------------------------------------------------
  }
}