using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using TournamentApi.DataAccess;
using TournamentApi.Extensions;

namespace TournamentApi.Extensions
{
    public static class DataHelper
    {
        public static T GetData<T>(string resourceName)
        {
            // Load the file
            var fileData = GetFromResources(resourceName);

            //whole file is the data
            var deserializeObject = JsonConvert.DeserializeObject<T>(fileData);
            return deserializeObject;
        }

        private static string GetFromResources(string resourceName)
        {
            var assembly = typeof(Team).Assembly;
            var stream = assembly.GetManifestResourceStream("TournamentApi." + resourceName);
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasData(GetCities());
    }

    private static IEnumerable<Team> GetCities()
    {
        var list = DataHelper.GetData<List<Team>>("Team.json");

        return list;
    }
}

public static class ShuffleExtensions
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list) 
    {
        var r = new Random((int)DateTime.Now.Ticks);
        var shuffledList = list.Select(x => new { Number = r.Next(), Item = x }).OrderBy(x => x.Number).Select(x => x.Item);
        return shuffledList.ToList();
    }
}
