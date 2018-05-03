using System.IO.Abstractions;
using NUnit.Framework;
using NSubstitute;
using m4gi10.Logic;
using m4gi10.Utils;

namespace m4gi10Test.Logic
{
  [TestFixture]
  public class RenamedTrackFileTest
  {
    //---------------------------------------------------------------------------------------------

    [Test]
    [Category("RenamedTrackFile")]
    public void Constructor_GivenTrackFileAndNewName_ShouldReturnBoth()
    {
      // Arrange.
      var fileSystem = Substitute.For<IFileSystem>();
      fileSystem.File.Exists(Arg.Any<string>()).Returns(true);

      var trackFile = new TrackFile(
        "someFilename",
        fileSystem,
        Substitute.For<IFileExtendedPropertyRetriever>());

      const string newFilename = "newFilename";

      // Act.
      var testObject = new RenamedTrackFile(trackFile, newFilename);

      // Assert.
      Assert.AreSame(trackFile, testObject.File);
      Assert.AreEqual(newFilename, testObject.NewFilename);
    }

    //---------------------------------------------------------------------------------------------
  }
}
