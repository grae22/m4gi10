using System;
using System.IO.Abstractions;
using NUnit.Framework;
using m4gi10;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace m4gi10Test
{
  [TestFixture]
  public class TrackFileTest
  {
    //---------------------------------------------------------------------------------------------

    [Test]
    [Category("TrackFile")]
    public void Constructor_GivenFilenameThatDoesntExist_ShouldThrowException()
    {
      // Arrange.
      const string invalidFilename = "someFileThatDoesntExist.txt";

      var fileSystem = Substitute.For<IFileSystem>();
      fileSystem.File.Exists(invalidFilename).Returns(false);

      var filePropertyRetriever = Substitute.For<IFileExtendedPropertyRetriever>();

      // Act & Assert.
      Assert.Throws<ArgumentException>(
        () =>
          new TrackFile(invalidFilename, fileSystem, filePropertyRetriever));
    }

    //---------------------------------------------------------------------------------------------

    [Test]
    [Category("TrackFile")]
    public void Constructor_GivenFileWithoutArtistAttrib_ShouldReturnUnknownForArtist()
    {
      // Arrange.
      const string validFilename = "someFile.txt";

      var fileSystem = Substitute.For<IFileSystem>();
      fileSystem.File.Exists(validFilename).Returns(true);

      var filePropertyRetriever = Substitute.For<IFileExtendedPropertyRetriever>();
      filePropertyRetriever.GetPropertyValue("System.Music.Artist").Returns("");

      // Act.
      var testObject = new TrackFile(validFilename, fileSystem, filePropertyRetriever);

      // Assert.
      Assert.AreEqual("Unknown", testObject.Artist);
    }

    //---------------------------------------------------------------------------------------------

    [Test]
    [Category("TrackFile")]
    public void Constructor_GivenFileWithArtistAttrib_ShouldReturnValueForArtist()
    {
      // Arrange.
      const string validFilename = "someFile.txt";

      var fileSystem = Substitute.For<IFileSystem>();
      fileSystem.File.Exists(validFilename).Returns(true);

      var filePropertyRetriever = Substitute.For<IFileExtendedPropertyRetriever>();
      filePropertyRetriever.GetPropertyValue("System.Music.Artist").Returns("SomeArtist");

      // Act.
      var testObject = new TrackFile(validFilename, fileSystem, filePropertyRetriever);

      // Assert.
      Assert.AreEqual("SomeArtist", testObject.Artist);
    }

    //---------------------------------------------------------------------------------------------

    [Test]
    [Category("TrackFile")]
    public void Constructor_GivenFileWithoutArtistAndWithAlbumArtistAttrib_ShouldReturnAlbumArtistValueForArtist()
    {
      // Arrange.
      const string validFilename = "someFile.txt";

      var fileSystem = Substitute.For<IFileSystem>();
      fileSystem.File.Exists(validFilename).Returns(true);

      var filePropertyRetriever = Substitute.For<IFileExtendedPropertyRetriever>();
      filePropertyRetriever.GetPropertyValue("System.Music.Artist").Returns("SomeArtist");
      filePropertyRetriever.GetPropertyValue("System.Music.AlbumArtist").Returns("SomeOtherArtist");

      // Act.
      var testObject = new TrackFile(validFilename, fileSystem, filePropertyRetriever);

      // Assert.
      Assert.AreEqual("SomeOtherArtist", testObject.Artist);
    }

    //---------------------------------------------------------------------------------------------

    [Test]
    [Category("TrackFile")]
    public void Constructor_GivenFileWithoutTrackNumberAttrib_ShouldReturnZero()
    {
      // Arrange.
      const string validFilename = "someFile.txt";

      var fileSystem = Substitute.For<IFileSystem>();
      fileSystem.File.Exists(validFilename).Returns(true);

      var filePropertyRetriever = Substitute.For<IFileExtendedPropertyRetriever>();
      filePropertyRetriever.GetPropertyValue("System.Music.TrackNumber").Returns("");

      // Act.
      var testObject = new TrackFile(validFilename, fileSystem, filePropertyRetriever);

      // Assert.
      Assert.AreEqual(0, testObject.TrackNumber);
    }

    //---------------------------------------------------------------------------------------------

    [Test]
    [Category("TrackFile")]
    public void Constructor_GivenFileWithTrackNumberAttrib_ShouldReturnValue()
    {
      // Arrange.
      const string validFilename = "someFile.txt";

      var fileSystem = Substitute.For<IFileSystem>();
      fileSystem.File.Exists(validFilename).Returns(true);

      var filePropertyRetriever = Substitute.For<IFileExtendedPropertyRetriever>();
      filePropertyRetriever.GetPropertyValue("System.Music.TrackNumber").Returns("10");

      // Act.
      var testObject = new TrackFile(validFilename, fileSystem, filePropertyRetriever);

      // Assert.
      Assert.AreEqual(10, testObject.TrackNumber);
    }

    //---------------------------------------------------------------------------------------------
  }
}
