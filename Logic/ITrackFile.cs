namespace m4gi10.Logic
{
  internal interface ITrackFile
  {
    //---------------------------------------------------------------------------------------------

    string Artist { get; }
    string Album { get; }
    string TrackName { get; }
    int TrackNumber { get; }
    string FileExtension { get; }
    string Filename { get; }

    //---------------------------------------------------------------------------------------------
  }
}
