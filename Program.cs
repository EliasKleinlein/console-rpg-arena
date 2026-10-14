using Models;
// Entry point of the Console RPG Arena.
// Uses top-level statements: the code in this file runs directly at program start,
// without an explicit Main method or Program class.
var random = new Random();
List<Character> playerParty = new()
{
    new Warrior("Brienne", 120),
    new Mage("Merlin", 80)
};
List<Character> enemies = new()
{
    new Rogue("Bibi", 200),
    new Warrior("Tina", 200)
};
bool quitRequested = false;
while (playerParty.Any(character => character.IsAlive) &&
       enemies.Any(character => character.IsAlive))
{
    DisplayStatus(playerParty, enemies);
    foreach (Character active in playerParty.Where(character => character.IsAlive))
    {
        List<Character> livingEnemies =
            enemies.Where(enemy => enemy.IsAlive).ToList();

        if (livingEnemies.Count == 0)
        {
            break;
        }

        int choice;

        do
        {
            choice = ReadMenuChoice(active);

            if (choice == 3)
            {
                DisplayStatus(playerParty, enemies);
            }
        }
        while (choice == 3);

        if (choice == 4)
        {
            quitRequested = true;
            break;
        }
        if (choice == 1)
        {
            Character target = ReadTarget(livingEnemies);
            active.Attack(target, random);
        }
        else if (choice == 2 && active is IHealer healer)
        {
            List<Character> livingAllies =
                playerParty.Where(character => character.IsAlive).ToList();

            Character target = ReadTarget(livingAllies);
            healer.Heal(target);
        }
    }
    if (quitRequested)
    {
        break;
    }
    foreach (Character enemy in enemies.Where(enemy => enemy.IsAlive))
    {
        List<Character> livingPlayers =
            playerParty.Where(character => character.IsAlive).ToList();

        if (livingPlayers.Count == 0)
        {
            break;
        }

        Character target =
            livingPlayers[random.Next(livingPlayers.Count)];

        enemy.Attack(target, random);
    }
}
if (quitRequested)
{
    Console.WriteLine("\nGame ended.");
}
else if (playerParty.Any(character => character.IsAlive))
{
    Console.WriteLine("\nVictory! All enemies have been defeated.");
}
else
{
    Console.WriteLine("\nDefeat! Your party has been defeated.");
}
static void DisplayStatus(
    List<Character> playerParty,
    List<Character> enemies)
{

    Console.WriteLine("Player Party:");

    foreach (Character character in playerParty)
    {
        string marker = character.IsAlive ? "" : " [DEFEATED]";

        Console.WriteLine(
            $"{character.Name} ({character.GetType().Name}): " +
            $"{character.Health}/{character.MaxHealth} HP{marker}");
    }
    Console.WriteLine("Enemies:");

    foreach (Character character in enemies)
    {
        string marker = character.IsAlive ? "" : " [DEFEATED]";

        Console.WriteLine(
            $"{character.Name} ({character.GetType().Name}): " +
            $"{character.Health}/{character.MaxHealth} HP{marker}");
    }
}
static void ShowTurnMenu(Character active)
{
    Console.WriteLine($"\n{active.Name}'s turn:");
    Console.WriteLine("1. Attack");

    if (active is IHealer)
    {
        Console.WriteLine("2. Use ability");
    }

    Console.WriteLine("3. Show status");
    Console.WriteLine("4. Quit");
}

static int ReadMenuChoice(Character active)
{
    while (true)
    {
        ShowTurnMenu(active);
        Console.Write("Choose action: ");

        string? input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input) &&
            int.TryParse(input, out int choice) &&
            choice >= 1 &&
            choice <= 4 &&
            (choice != 2 || active is IHealer))
        {
            return choice;
        }

        Console.WriteLine("Invalid choice. Please select an available option.");
    }
}

static Character ReadTarget(List<Character> targets)
{
    while (true)
    {
        Console.WriteLine("Choose a target:");

        for (int index = 0; index < targets.Count; index++)
        {
            Console.WriteLine(
                $"{index + 1}. {targets[index].Name} " +
                $"({targets[index].Health}/{targets[index].MaxHealth} HP)");
        }

        string? input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input) &&
            int.TryParse(input, out int targetNumber) &&
            targetNumber >= 1 &&
            targetNumber <= targets.Count)
        {
            return targets[targetNumber - 1];
        }

        Console.WriteLine("Invalid target. Please select an available target.");
    }
}
