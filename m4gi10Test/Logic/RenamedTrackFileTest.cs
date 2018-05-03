using NUnit.Framework;
using NSubstitute;
using m4gi10.Logic;

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
      var trackFile = Substitute.For<ITrackFile>();

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
