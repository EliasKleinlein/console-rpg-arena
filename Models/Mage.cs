namespace Models;

/// <summary>
/// Deals between 5 and 25 damage per attack.
/// </summary>
public class Mage : Character, IHealer
{
    /// <summary>
    /// Creates a new Mage.
    /// </summary>
    /// <param name="name">The display name of the Mage.</param>
    /// <param name="health">The starting and maximum health.</param>
    public Mage(string name, int health) : base(name, health)
    {
    }

    /// <summary>
    /// Attacks the target for a random amount of damage and prints the result.
    /// </summary>
    /// <param name="target">The character that receives the damage.</param>
    /// <param name="random">The random number generator used to roll the damage.</param>
    public override void Attack(Character target, Random random)
    {
        // Roll damage between 5 and 25 (Next excludes the upper bound 26)
        int damage = random.Next(5, 26);

        // Apply the damage; TakeDamage keeps Health from dropping below 0
        target.TakeDamage(damage);

        Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
    }
    public void Heal(Character target)
    {
        // Heal the target for a fixed amount of 180 health
        int amount = 180;

        // Apply the healing; Heal keeps Health from exceeding MaxHealth
        target.Heal(amount);

        Console.WriteLine($"{Name} heals {target.Name} for {amount} health!");
    }
}
