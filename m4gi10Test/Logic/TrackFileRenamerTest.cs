using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using m4gi10.Logic;

namespace m4gi10Test.Logic
{
  [TestFixture]
  public class TrackFileRenamerTest
  {
    //---------------------------------------------------------------------------------------------

    [Test]
    [Category("TrackFileRenamer")]
    public void GetRenamedTrackFiles_GivenOneTrackFile_ShouldReturnRenamedTrackFile()
    {
      // Arrange.
      var trackFile = Substitute.For<ITrackFile>();
      trackFile.Artist.Returns("Some Artist");
      trackFile.TrackNumber.Returns(1);

      // Act.
      var renamedFiles = TrackFileRenamer.GetRenamedTrackFiles(new List<ITrackFile> { trackFile });

      // Assert.
      Assert.AreEqual(1, renamedFiles.Count());
      Assert.AreEqual("SomeArtist_1", renamedFiles.First().NewFilename);
    }

    //---------------------------------------------------------------------------------------------
  }
}
