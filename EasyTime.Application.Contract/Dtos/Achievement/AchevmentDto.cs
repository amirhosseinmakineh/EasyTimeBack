namespace EasyTime.Application.Contract.Dtos.Achevment
{
    public record AchievementDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }

    }
}
