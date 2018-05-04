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
      trackFile.Album.Returns("Some Album");
      trackFile.TrackNumber.Returns(1);

      // Act.
      var renamedFiles = TrackFileRenamer.GetRenamedTrackFiles(
        new List<ITrackFile> { trackFile },
        99,
        99);

      // Assert.
      Assert.AreEqual(1, renamedFiles.Count());
      Assert.AreEqual("SomeArtist_SomeAlbum_1", renamedFiles.First().NewFilename);
    }

    //---------------------------------------------------------------------------------------------

    [Test]
    [Category("TrackFileRenamer")]
    public void GetRenamedTrackFiles_GivenMultipleArtists_ShouldReturnInOrder()
    {
      // Arrange.
      var trackFile1 = Substitute.For<ITrackFile>();
      trackFile1.Artist.Returns("Some Artist");
      trackFile1.TrackNumber.Returns(1);

      var trackFiles = new List<ITrackFile>
      {
        Substitute.For<ITrackFile>(),
        Substitute.For<ITrackFile>(),
        Substitute.For<ITrackFile>(),
        Substitute.For<ITrackFile>()
      };

      trackFiles[0].Artist.Returns("Some Artist 2");
      trackFiles[1].Artist.Returns("Some Artist 1");
      trackFiles[2].Artist.Returns("Some Artist 1");
      trackFiles[3].Artist.Returns("Some Artist 2");

      trackFiles[0].Album.Returns("Some Album");
      trackFiles[1].Album.Returns("Some Album");
      trackFiles[2].Album.Returns("Some Album");
      trackFiles[3].Album.Returns("Some Album");

      // Act.
      var renamedFiles = TrackFileRenamer.GetRenamedTrackFiles(
        trackFiles,
        99,
        99);
      
      // Assert.
      Assert.AreEqual("SomeArtist1_SomeAlbum_1", renamedFiles.ElementAt(0).NewFilename);
      Assert.AreEqual("SomeArtist1_SomeAlbum_2", renamedFiles.ElementAt(1).NewFilename);
      Assert.AreEqual("SomeArtist2_SomeAlbum_1", renamedFiles.ElementAt(2).NewFilename);
      Assert.AreEqual("SomeArtist2_SomeAlbum_2", renamedFiles.ElementAt(3).NewFilename);
    }

    //---------------------------------------------------------------------------------------------

    [Test]
    [Category("TrackFileRenamer")]
    public void GetRenamedTrackFiles_GivenMultipleArtistsAndAlbums_ShouldReturnInOrderByArtistAndAlbum()
    {
      // Arrange.
      var artist1Album1Track1 = Substitute.For<ITrackFile>();
      var artist1Album1Track2 = Substitute.For<ITrackFile>();
      var artist1Album2Track1 = Substitute.For<ITrackFile>();
      var artist1Album2Track2 = Substitute.For<ITrackFile>();
      var artist2Album1Track1 = Substitute.For<ITrackFile>();
      var artist2Album1Track2 = Substitute.For<ITrackFile>();

      artist1Album1Track1.Artist.Returns("Artist1");
      artist1Album1Track2.Artist.Returns("Artist1");
      artist1Album2Track1.Artist.Returns("Artist1");
      artist1Album2Track2.Artist.Returns("Artist1");
      artist2Album1Track1.Artist.Returns("Artist2");
      artist2Album1Track2.Artist.Returns("Artist2");

      artist1Album1Track1.Album.Returns("1");
      artist1Album1Track2.Album.Returns("1");
      artist1Album2Track1.Album.Returns("2");
      artist1Album2Track2.Album.Returns("2");
      artist2Album1Track1.Album.Returns("3");
      artist2Album1Track2.Album.Returns("3");

      var trackFiles = new List<ITrackFile>
      {
        artist2Album1Track2,
        artist2Album1Track1,
        artist1Album2Track2,
        artist1Album2Track1,
        artist1Album1Track2,
        artist1Album1Track1
      };
      
      // Act.
      var renamedFiles = TrackFileRenamer.GetRenamedTrackFiles(
        trackFiles,
        99,
        99);

      // Assert.
      Assert.AreEqual("Artist1_1_1", renamedFiles.ElementAt(0).NewFilename);
      Assert.AreEqual("Artist1_1_2", renamedFiles.ElementAt(1).NewFilename);
      Assert.AreEqual("Artist1_2_1", renamedFiles.ElementAt(2).NewFilename);
      Assert.AreEqual("Artist1_2_2", renamedFiles.ElementAt(3).NewFilename);
      Assert.AreEqual("Artist2_3_1", renamedFiles.ElementAt(4).NewFilename);
      Assert.AreEqual("Artist2_3_2", renamedFiles.ElementAt(5).NewFilename);
    }

    //---------------------------------------------------------------------------------------------

    [Test]
    [Category("TrackFileRenamer")]
    public void GetRenamedTrackFiles_GivenLongArtistAndAlbumNames_ShouldTruncate()
    {
      // Arrange.
      var artist1Album1Track1 = Substitute.For<ITrackFile>();
      var artist1Album1Track2 = Substitute.For<ITrackFile>();
      var artist1Album2Track1 = Substitute.For<ITrackFile>();
      var artist1Album2Track2 = Substitute.For<ITrackFile>();
      var artist2Album1Track1 = Substitute.For<ITrackFile>();
      var artist2Album1Track2 = Substitute.For<ITrackFile>();

      artist1Album1Track1.Artist.Returns("Some Artist1");
      artist1Album1Track2.Artist.Returns("Some Artist1");
      artist1Album2Track1.Artist.Returns("Some Artist1");
      artist1Album2Track2.Artist.Returns("Some Artist1");
      artist2Album1Track1.Artist.Returns("Some Artist2");
      artist2Album1Track2.Artist.Returns("Some Artist2");

      artist1Album1Track1.Album.Returns("Some Album 1");
      artist1Album1Track2.Album.Returns("Some Album 1");
      artist1Album2Track1.Album.Returns("Some Album 2");
      artist1Album2Track2.Album.Returns("Some Album 2");
      artist2Album1Track1.Album.Returns("Some Album 3");
      artist2Album1Track2.Album.Returns("Some Album 3");

      var trackFiles = new List<ITrackFile>
      {
        artist2Album1Track2,
        artist2Album1Track1,
        artist1Album2Track2,
        artist1Album2Track1,
        artist1Album1Track2,
        artist1Album1Track1
      };

      // Act.
      var renamedFiles = TrackFileRenamer.GetRenamedTrackFiles(trackFiles);

      // Assert.
      Assert.AreEqual("SomeAr_SomeAl_1", renamedFiles.ElementAt(0).NewFilename);
      Assert.AreEqual("SomeAr_SomeAl_2", renamedFiles.ElementAt(1).NewFilename);
      Assert.AreEqual("SomeAr_SomeAl_1", renamedFiles.ElementAt(2).NewFilename);
      Assert.AreEqual("SomeAr_SomeAl_2", renamedFiles.ElementAt(3).NewFilename);
      Assert.AreEqual("SomeAr_SomeAl_1", renamedFiles.ElementAt(4).NewFilename);
      Assert.AreEqual("SomeAr_SomeAl_2", renamedFiles.ElementAt(5).NewFilename);
    }

    //---------------------------------------------------------------------------------------------
  }
}
