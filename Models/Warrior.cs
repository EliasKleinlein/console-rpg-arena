namespace Models;

/// <summary>
/// Deals between 10 and 20 damage per attack.
/// </summary>
public class Warrior : Character
{
    /// <summary>
    /// Creates a new Warrior.
    /// </summary>
    /// <param name="name">The display name of the Warrior.</param>
    /// <param name="health">The starting and maximum health.</param>
    public Warrior(string name, int health) : base(name, health)
    {
    }

    /// <summary>
    /// Attacks the target for a random amount of damage and prints the result.
    /// </summary>
    /// <param name="target">The character that receives the damage.</param>
    /// <param name="random">The random number generator used to roll the damage.</param>
    public override void Attack(Character target, Random random)
    {
        // Roll damage between 10 and 20 (Next excludes the upper bound 21)
        int damage = random.Next(10, 21);

        // Apply the damage; TakeDamage keeps Health from dropping below 0
        target.TakeDamage(damage);

        Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
    }
}
