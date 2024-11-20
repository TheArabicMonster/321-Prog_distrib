using serial;
using System.Text.Json;
using System.Text.Json.Serialization;

Actor actor = new Actor
{
    BirthDate = DateTime.Today,
    Country = "Switzerland",
    FirstName = "Brad",
    LastName = "Pitt",
    IsAlive = true
};

Character character = new Character
{
    FirstName = "Maya",
    LastName = "Harris",
    Description = "se nourris uniquement de cuisse de poulet",
    PlayedBy = actor
};

Episode episode = new Episode
{
    Title = "The Avatar and the Firelord",
    DurationMinutes = 24,
    SequenceNumber = 153,
    Director = "Tim Burton",
    Synopsis = "Aang et Zuko en apprennent plus sur le passé de la nation du feu, et ce qui a engendré la guerre : le premier grâce à son ancienne réincarnation, l'avatar Roku (présent au moment des faits), et le second grâce à un manuscrit déposé anonymement (qui vient en réalité de son oncle Iroh) devant sa chambre et qui lui suggère d'aller lire un parchemin, écrit par son arrière-grand-père, Sozin, défunt seigneur du feu et meilleur ami de l'avatar Roku.",
    Characters = [character]
};

Saison saison1 = new Saison
{
    Episodes = [episode],
    Characters = [character],
    Actors = [actor]
};



string jsonString = JsonSerializer.Serialize(character);
Console.WriteLine(jsonString);

Console.WriteLine($"Le personnage de {character.FirstName} {character.LastName} est joué par {character.PlayedBy.FirstName} {character.PlayedBy.LastName}");

string filePath = "data.json";
using (StreamWriter writer = new StreamWriter(filePath))
{
    var option = new JsonSerializerOptions { WriteIndented = true };
    var objects = new { actor, character, episode };

    writer.WriteLine(JsonSerializer.Serialize(objects, option));
}

string jsonStringEp = JsonSerializer.Serialize(episode);

Episode stringJsonDeserialized = JsonSerializer.Deserialize<Episode>(jsonStringEp);

Console.ReadLine();