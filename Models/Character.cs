namespace Models;

/// <summary>
/// Base class for all characters. Holds name and health and handles damage and healing.
/// </summary>
public abstract class Character
{
    /// <summary>
    /// The character's name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Current health. Can only be changed from inside this class
    /// (via TakeDamage and Heal) and always stays between 0 and MaxHealth.
    /// </summary>
    public int Health { get; private set; }

    /// <summary>
    /// The highest health value. Set once in the constructor and never changed.
    /// </summary>
    public int MaxHealth { get; }

    /// <summary>
    /// True while Health is above 0. Calculated on every access.
    /// </summary>
    public bool IsAlive => Health > 0;

    /// <summary>
    /// Creates a character with full health.
    /// Called by subclasses through <c>: base(name, health)</c>.
    /// </summary>
    /// <param name="name">The display name of the character.</param>
    /// <param name="health">The starting and maximum health.</param>
    public Character(string name, int health)
    {
        Name = name;
        Health = health;
        MaxHealth = health;
    }

    /// <summary>
    /// Reduces Health by the given amount. Health never drops below 0.
    /// </summary>
    /// <param name="amount">The damage to apply.</param>
    public void TakeDamage(int amount)
    {
        Health -= amount;

        // Clamp to 0 so Health can never become negative
        if (Health < 0)
        {
            Health = 0;
        }
    }

    /// <summary>
    /// Increases Health by the given amount. Health never exceeds MaxHealth.
    /// </summary>
    /// <param name="amount">The health to restore.</param>
    public void Heal(int amount)
    {
        Health += amount;

        // Clamp to MaxHealth so healing cannot overfill
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    /// <summary>
    /// Attacks the target. Every subclass must decide how much damage it deals.
    /// </summary>
    /// <param name="target">The character that receives the damage.</param>
    /// <param name="random">The random number generator used to roll the damage.</param>
    public abstract void Attack(Character target, Random random);
}
