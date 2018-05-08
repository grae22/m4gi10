﻿using System.Collections.Generic;
using System.IO;
using System.Threading;
using Shell32;

namespace m4gi10.Utils
{
  internal class FileExtendedPropertyRetriever : IFileExtendedPropertyRetriever
  {
    //---------------------------------------------------------------------------------------------

    private Dictionary<string, string> _properties;

    //---------------------------------------------------------------------------------------------

    public FileExtendedPropertyRetriever(string filename)
    {
      var threadStart = new ThreadStart(() => { _properties = GetFileProperties(filename); });
      var thread = new Thread(threadStart);
      thread.SetApartmentState(ApartmentState.STA);
      thread.Start();
      thread.Join(5000);
    }

    //---------------------------------------------------------------------------------------------

    public string GetPropertyValue(string name)
    {
      var nameLower = name.ToLower();

      if (!_properties.ContainsKey(nameLower))
      {
        return string.Empty;
      }

      return _properties[nameLower];
    }

    //---------------------------------------------------------------------------------------------

    private static Dictionary<string, string> GetFileProperties(string filename)
    {
      var properties = new Dictionary<string, string>();
      var arrHeaders = new List<string>();

      var shell = new Shell();

      Folder objFolder = shell.NameSpace(Path.GetDirectoryName(filename));

      for (int i = 0; i < short.MaxValue; i++)
      {
        string header = objFolder.GetDetailsOf(null, i);
        if (string.IsNullOrEmpty(header))
          break;
        arrHeaders.Add(header);
      }

      var item = objFolder.ParseName(Path.GetFileName(filename));

      var indexArtist = arrHeaders.IndexOf("Authors");
      var indexContributingArtists = arrHeaders.IndexOf("Contributing artists");
      var indexAlbum = arrHeaders.IndexOf("Album");
      var indexTrackNumber = arrHeaders.IndexOf("#");
      var indexTrackTitle = arrHeaders.IndexOf("Title");

      //for (int i = 0; i < arrHeaders.Count; i++)
      //{
      //  string propertyName = arrHeaders[i].ToLower();
      //  string propertyValue = objFolder.GetDetailsOf(item, i);

      //  if (properties.ContainsKey(propertyName))
      //  {
      //    continue;
      //  }

      //  properties.Add(propertyName, propertyValue);
      //}

      if (indexArtist > -1)
      {
        properties.Add("system.music.albumartist", objFolder.GetDetailsOf(item, indexArtist));
      }

      if (indexContributingArtists > -1)
      {
        properties.Add("system.music.artist", objFolder.GetDetailsOf(item, indexContributingArtists));
      }

      if (indexAlbum > -1)
      {
        properties.Add("system.music.album", objFolder.GetDetailsOf(item, indexAlbum));
      }

      if (indexTrackNumber > -1)
      {
        properties.Add("system.music.tracknumber", objFolder.GetDetailsOf(item, indexTrackNumber));
      }

      if (indexTrackTitle > -1)
      {
        properties.Add("system.music.title", objFolder.GetDetailsOf(item, indexTrackTitle));
      }

      return properties;
    }

    //---------------------------------------------------------------------------------------------
  }
}
