namespace m4gi10.Logic
{
  internal class RenamedTrackFile
  {
    //---------------------------------------------------------------------------------------------

    public ITrackFile File { get; }
    public string NewFilename { get; }

    //---------------------------------------------------------------------------------------------

    public RenamedTrackFile(ITrackFile trackFile, string newFilename)
    {
      File = trackFile;
      NewFilename = newFilename;
    }

    //---------------------------------------------------------------------------------------------

    public override string ToString()
    {
      return $"{NewFilename}";
    }

    //---------------------------------------------------------------------------------------------
  }
}