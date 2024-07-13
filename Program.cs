// See https://aka.ms/new-console-template for more information
using System.Data.Common;
using System.Globalization;

Console.WriteLine("Hello, World!");
List<Plant> plants = new List<Plant>()
{
    new Plant()
    {
        Species = "Prayer Plant",
        LightNeeds = 2,
        AskingPrice = 20.00M,
        City = "New York",
        Sold = false,
        AvailableUntil = new DateTime(2024, 08, 05)
    },

    new Plant()
    {
        Species = "Spider Plant",
        LightNeeds = 3,
        AskingPrice = 10.75M,
        City = "Nashville",
        Sold = false,
        AvailableUntil = new DateTime(2024, 09, 24)
    },

    new Plant()
    {
        Species = "Easter Cactus",
        LightNeeds = 4,
        AskingPrice = 15.50M,
        City = "Santa Fe",
        Sold = false,
        AvailableUntil = new DateTime(2024, 11, 24)
    },

    new Plant()
    {
        Species = "Hyacinth",
        LightNeeds = 4,
        AskingPrice = 25.00M,
        City = "Greensboro",
        Sold = true,
        AvailableUntil = new DateTime(2025, 05, 04)
    },

    new Plant()
    {
        Species = "Catnip",
        LightNeeds = 4,
        AskingPrice = 5.99M,
        City = "Chicago",
        Sold = false,
        AvailableUntil = new DateTime(2024, 06, 24)
    }

};

Random random = new Random();
int randomPlant = random.Next(0, plants.Count);

while (plants[randomPlant].Sold == true)
{
    randomPlant = random.Next(0, plants.Count);
}

string greeting = "Welcome to ExtraVert Botanicals!";

Console.WriteLine(greeting);

string choice = null;
while (choice != "0")
{
    Console.WriteLine(@"Choose an option:
0. Exit
1. Display all plants
2. Post a plant to be adopted
3. Adopt a plant
4. Delist a plant
5. Display plant of the day
6. Search for plants by light needs
7. Show stats for listed plants");

    choice = Console.ReadLine();
    if (choice == "0")
    {
        Console.Clear();
        Console.WriteLine("Goodbye!");
    }
    else if (choice == "1")
    {
        Console.Clear();
        ListPlants();
    }
    else if(choice == "2")
    {
        Console.Clear();
        PostPlant();
    }
    else if (choice == "3")
    {
        AdoptPlant();
    }
    else if (choice == "4")
    {
        DelistPlant();
    }
    else if (choice == "5")
    {
        RandomPlant();
    }
    else if (choice == "6")
    {
        SearchByLightNeeds();
    }
    else if (choice == "7")
    {
        GetStats();
    }
    else 
    {
        Console.Clear();
        Console.WriteLine("Please choose a valid choice between 1 - 4!");
    }
}

void ListPlants()
{
    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. A {plants[i].Species} in {plants[i].City} {(plants[i].Sold ? "was sold" : "is available")} for {plants[i].AskingPrice}");
    }
}

void PostPlant()
{
    string userSpecies = null;
    int userLightNeeds = 0;
    decimal userAskingPrice = 0;
    string userCity = null;
    int userZIP = 0;
    DateTime userDate = DateTime.MinValue;

    while (string.IsNullOrEmpty(userSpecies))
    {
        Console.WriteLine("Enter plant species");
        userSpecies = Console.ReadLine();
    };

    while (userLightNeeds == 0)
    {
        Console.WriteLine("What is your plants light needs? 1 being very little light and 5 being direct sunlight");
        string userLightNeedsInput = Console.ReadLine();

        if (!string.IsNullOrEmpty(userLightNeedsInput))
        {
            userLightNeeds = Int32.Parse(userLightNeedsInput);
        }
    };

    while (userAskingPrice == 0)
    {
        Console.WriteLine("How much does your plant cost?");
        string userAskingPriceInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(userAskingPriceInput))
        {
            userAskingPrice = decimal.Parse(userAskingPriceInput);
        }
    };

    while (string.IsNullOrEmpty(userCity))
    {
        Console.WriteLine("What city is your plant currently in?");
        userCity = Console.ReadLine();
    };

    while (userZIP == 0)
    {
        Console.WriteLine("What is the Zipcode where your plant is located?");
        string userZIPInput = Console.ReadLine();

        if (!string.IsNullOrEmpty(userZIPInput))
        {
            userZIP = Int32.Parse(userZIPInput);
        }
    };

    while (userDate == DateTime.MinValue)
    {
        Console.WriteLine("How long would you like your plant listed? Please tell us in the yyyy/mm/dd format!");

        if (DateTime.TryParse(Console.ReadLine(), out userDate))
        {
            Console.WriteLine(userDate);
            break;
        }
        else{
            Console.WriteLine("I'm sorry, that date is invalid. Please enter a new date in the yyyy/mm/dd format.");
        }
    };

    string userAnswer = null;
    while (userAnswer == null)
    {
        Console.WriteLine($"Are you ready to list {userSpecies} in {userCity} for sale for {userAskingPrice}? Yes or No");
        userAnswer = Console.ReadLine();
        if (userAnswer == "no" || userAnswer == "No" || userAnswer == "n" || userAnswer == "N")
        {
            Console.WriteLine("That's too bad! Goodbye!");
        }
        else if (userAnswer == "yes" || userAnswer == "Yes" || userAnswer == "y" || userAnswer == "Y")
        {
            Console.WriteLine($"Great! Your {userSpecies} will be added to our adoption list!");
            plants.Add(new Plant()
            {
                Species = userSpecies,
                LightNeeds = userLightNeeds,
                AskingPrice = userAskingPrice,
                City = userCity,
                ZIP = userZIP,
                Sold = false,
                AvailableUntil = userDate,
            });
        }
        else
        {
            Console.WriteLine("Please provide a yes or no answer. Are you ready to list your plant for sale?");
        }
    }
}

void AdoptPlant()
{
    Console.Clear();
    Console.WriteLine("Here are our available plants. Which would you like to adopt?");
    for (int i = 0; i < plants.Count; i++)
    {
        if (plants[i].Sold == false && plants[i].AvailableUntil > DateTime.Now)
        {
        Console.WriteLine($"{i + 1}. A {PlantDetails(plants[i])}");
        }
    }
    int userChoice = Int32.Parse(Console.ReadLine()) - 1;

    plants[userChoice].Sold = true;

    Console.WriteLine($"Congratulations! You have adopted the {plants[userChoice].Species} for ${plants[userChoice].AskingPrice}!");
}


void DelistPlant()
{
    Console.Clear();
    Console.WriteLine("Which plant would you like to removing from our listings?");

    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. A {PlantDetails(plants[i])}");
    }

    int userChoice = Int32.Parse(Console.ReadLine());
    int plantIndex = userChoice - 1;

    plants.RemoveAt(plantIndex);

    Console.WriteLine($"As you wish! The {plants[plantIndex].Species} in {plants[plantIndex].City} has been removed from our listings.");
}

void RandomPlant()
{
    Console.Clear();
    Console.WriteLine($"Today's plant is the {plants[randomPlant].Species} in {plants[randomPlant].City}. It is is available for ${plants[randomPlant].AskingPrice}");
}

void SearchByLightNeeds()
{
    Console.Clear();
    Console.WriteLine("What is the maximum lighting for the plants you'd like to see? 1 being minimal light and 5 being direct sunlight.");

    int userChoice = Int32.Parse(Console.ReadLine());

    for (int i = 0; i < plants.Count; i++) 
    {
        if (plants[i].LightNeeds <= userChoice)
        {
            Console.WriteLine($"{i + 1}. The {plants[i].Species} in {plants[i].City} has a light need of {plants[i].LightNeeds}");
        }
    }
}

void GetStats ()
{
    Console.Clear();
    Console.WriteLine($"Lowest price plant: {plants.Min(p => p.AskingPrice)}");
    Console.WriteLine($"Number of available plants: {plants.Where(p => p.Sold == false).ToList().Count}");
    Console.WriteLine($"Number of plants with the highest light needs: {plants.Where(p => p.LightNeeds > 4).ToList().Count} ");
    Console.WriteLine($"Average light needs of all listed plants: {plants.Average(p => p.LightNeeds)}");
    Console.WriteLine($"Percentage of adopted plants: {(decimal)(plants.Where(p => p.Sold!).ToList().Count) / plants.Count * 100}%");
}

string PlantDetails(Plant plant)
{
    string plantString = $"{plant.Species} in {plant.City} {(plant.Sold ? "sold" : "available")} for {plant.AskingPrice}";

    return plantString;
}