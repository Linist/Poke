using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

class PokemonStatsImporter
{

    public static Pokemon Create(int level, string name)
    {

        //read pokedex file and import
        using (var reader = new StreamReader(@"Assets\Pokedex.csv"))
        {
            // Skipping first line
            reader.ReadLine();

            // Reading lines 
            while (!reader.EndOfStream)
            {
                // Store values 
                String pokeLine = reader.ReadLine();
                String[] values = pokeLine.Split(',');

                // finding corresponding pokemon
                if (values[2].ToLower() == name.ToLower())
                {
                    
                    return InstantiatePokemon(level, values);
                }
            }
        }


    }

    static Pokemon InstantiatePokemon(int level, String[] values)
    {
        // Get the element from the pokemon info
        Elements element;
        switch (values[10])
        {
            case "Fire":
                element = Elements.Fire;
                break;
            case "Water":
                element = Elements.Water;
                break;
            case "Grass":
                element = Elements.Grass;
                break;
            case "Electric":
                element = Elements.Electric;
                break;
        }

        // pokemon moves
        List<Move> moves = new List<Move>();
        moves.Add(new Move(values[13]));
        if (values[14] != "")
            moves.Add(new Move(values[14]));

        // always two possible moves
        else
            moves.Add(new Move("Scratch"));

        // create and return the pokemon 
        return new Pokemon(values[2], level, int.Parse(values[4]), int.Parse(values[5]), int.Parse(values[3]), element, moves);
    }

    public static Pokemon CreateRandom()
    {
        // Store pokedex lines ind string array
        String[] lines = File.ReadAllLines(@"Assets\Pokedex.csv");
        // create randome pokemon
        String randomLine = lines[UnityEngine.Random.Range(1, lines.Length)];
        // separate stats
        String[] values = randomLine.Split(',');
        // Initialize and return pokemon
        return InstantiatePokemon(UnityEngine.Random.Range(1, 11), values);
    }
}
