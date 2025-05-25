namespace HauntedHouse;

public class Ghost : MovingSprite
{
    public int healthPoints { get; private set; } // Очки здоровья призрака
    private float attackCooldown = 1.5f; // Кулдаун между выстрелами
    private float attackCooldownLeft = 0f;
    private const float AttackRange = 300f; // Дальность атаки призрака
    private const float MinDistance = 150f; // Минимальная дистанция между призраком и игроком
    private const int ExperienceOrbsOnDeath = 1; // Количество орбов опыта при смерти
    private const int ScoreOnDeath = 100; // Очки за убийство призрака
    private float _poisonEffectTimer = 0f;
    private const float PoisonEffectDuration = 0.3f; // Длительность эффекта отравления
    private bool _isPoisoned = false;

    public Ghost(Texture2D texture, Vector2 position) : base(texture, position)
    {
        Speed = 100 * DifficultySettings.GetGhostSpeedMultiplier(); // Скорость меняется от модификатора в зависимости от уровня сложности
        healthPoints = DifficultySettings.GetGhostHealthPoints(); // Здоровье призрака меняется от модификатора в зависимости от уровня сложности
    }

    public void TakeDamage(int damage) // Метод получения урона призраком
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            ScoreManager.AddScore(ScoreOnDeath);
            // Создаёт сферу с опытом (после смерти призрака)
            Vector2 offset = new(
                (float)(Random.Shared.NextDouble() - 0.5) * 20,
                (float)(Random.Shared.NextDouble() - 0.5) * 20
            );
            ExperienceManager.AddExperience(Position + offset);
        }
    }

    public void TakePoisonDamage(int damage) // Метод получения урона от яда
    {
        TakeDamage(damage);
        _isPoisoned = true;
        _poisonEffectTimer = PoisonEffectDuration;
    }

    public void Update(Player player)
    {
        // Сбрасывает таймер эффекта отравления
        if (_isPoisoned)
        {
            _poisonEffectTimer -= Globals.TotalSeconds;
            if (_poisonEffectTimer <= 0)
            {
                _isPoisoned = false;
            }
        }

        var toPlayer = player.Position - Position;
        float distanceToPlayer = toPlayer.Length();

        // Обновляет кулдаун атаки
        if (attackCooldownLeft > 0)
        {
            attackCooldownLeft -= Globals.TotalSeconds;
        }

        // Обработка стрельбы призраком
        if (distanceToPlayer <= AttackRange && attackCooldownLeft <= 0)
        {
            var direction = Vector2.Normalize(toPlayer);
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            
            // Создание снаряда
            ProjectileData projectileData = new()
            {
                Position = Position,
                Rotation = rotation,
                Lifespan = 1.5f,
                Speed = 400,
                Damage = 1,
                IsEnemyProjectile = true
            };

            ProjectileManager.AddProjectile(projectileData);
            attackCooldownLeft = attackCooldown;
        }

        // Движение к игроку (до минимальной дистанции)
        if (distanceToPlayer > MinDistance)
        {
            var direction = Vector2.Normalize(toPlayer);
            Position += direction * Speed * Globals.TotalSeconds;
        }
        // Призрак держится на минимальной дистанции от игрока
        else if (distanceToPlayer < MinDistance - 20)
        {
            var direction = Vector2.Normalize(toPlayer);
            Position -= direction * Speed * Globals.TotalSeconds;
        }
    }

    public override void Draw()
    {
        Color tint = _isPoisoned ? new Color(0.5f, 1f, 0.5f, 1f) : Color.White; // Окрашивает призрака в зелёный при получении урона от яда
        Globals.SpriteBatch.Draw(
            Texture,
            Position,
            null,
            tint,
            Rotation,
            new Vector2(Texture.Width / 2f, Texture.Height / 2f),
            2f,
            SpriteEffects.None,
            0f
        );
    }
}