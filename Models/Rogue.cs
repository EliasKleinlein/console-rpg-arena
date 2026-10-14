namespace Models;

/// <summary>
/// Deals between 8 and 16 damage per attack.
/// </summary>
public class Rogue : Character
{
    /// <summary>
    /// Creates a new Rogue.
    /// </summary>
    /// <param name="name">The display name of the Rogue.</param>
    /// <param name="health">The starting and maximum health.</param>
    public Rogue(string name, int health) : base(name, health)
    {
    }

    /// <summary>
    /// Attacks the target for a random amount of damage and prints the result.
    /// </summary>
    /// <param name="target">The character that receives the damage.</param>
    /// <param name="random">The random number generator used to roll the damage.</param>
    public override void Attack(Character target, Random random)
    {
        // Roll damage between 8 and 16 (Next excludes the upper bound 17)
        int damage = random.Next(8, 17);

        // Apply the damage; TakeDamage keeps Health from dropping below 0
        target.TakeDamage(damage);

        Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
    }
}
