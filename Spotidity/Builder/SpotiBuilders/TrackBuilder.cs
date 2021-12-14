using Spotidity.Builder.CommandBuilders;
using Spotidity.Models;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotidity.Builder.SpotiBuilders
{
    public class TrackBuilder : BaseSpotiBuilder
    {
        private List<SimpleTrack> _simpleData;
        private List<FullTrack> _fullData;
        private List<PlaylistTrack<IPlayableItem>> _playData;
        private readonly string _param;
        private BaseCommandBuilder b = new BaseCommandBuilder();

        private List<ISpotiModel> tracks = new List<ISpotiModel>();

        public TrackBuilder(List<SimpleTrack> t)
        {
            _simpleData = t;
            _param = "SIMPLE";
        }

        public TrackBuilder(List<PlaylistTrack<IPlayableItem>> t)
        {
            _playData = t;
            _param = "PLAY";
        }

        public TrackBuilder(List<FullTrack> t)
        {
            _fullData = t;
            _param = "FULL";
        }

        public override void createProps()
        {

            switch (_param)
            {
                case "SIMPLE":
                    break;
                case "PLAY":
                    foreach (PlaylistTrack<IPlayableItem> item in _playData)
                    {
                        if (item.Track is FullTrack track)
                        {
                            tracks.Add(createFullTrack(new SpotiTrack(), track));
                        }
                    }
                    break;
                case "FULL":
                    foreach (FullTrack item in _fullData)
                    {
                        tracks.Add(createFullTrack(new SpotiTrack(), item));
                    }
                    break;

            }
        }

        private SpotiTrack createFullTrack(SpotiTrack track, FullTrack fullTrack)
        {
            track.Id = fullTrack.Id;
            track.Name = fullTrack.Name;
            track.Artists = fullTrack.Artists;
            track.DurationMs = fullTrack.DurationMs;
            TimeSpan ts = TimeSpan.FromMilliseconds(fullTrack.DurationMs);
            track.DurationStr = string.Format("{0:D2}m:{1:D2}s", ts.Minutes, ts.Seconds);
            track.TrackNumber = fullTrack.TrackNumber;
            track.Popularity = fullTrack.Popularity;
            track.Album = fullTrack.Album;
            track.GoToArtistCMD = b.GoToArtist(fullTrack.Id);
            track.GoToAlbumCMD = b.GoToAlbumTracks(fullTrack.Id);
            return track;
        }

        private SpotiTrack createSimpleTrack(SpotiTrack track, SimpleTrack simpleTrack)
        {
            track.Id = simpleTrack.Id;
            track.Name = simpleTrack.Name;
            track.Artists = simpleTrack.Artists;
            track.DurationMs = simpleTrack.DurationMs;
            TimeSpan ts = TimeSpan.FromMilliseconds(simpleTrack.DurationMs);
            track.DurationStr = string.Format("{0:D2}m:{1:D2}s", ts.Minutes, ts.Seconds);
            track.TrackNumber = simpleTrack.TrackNumber;
            track.GoToAlbumCMD = b.GoToAlbumTracks(simpleTrack.Id);
            return track;
        }



        public override List<ISpotiModel> getResult()
        {
            return tracks;
        }
    }
}
