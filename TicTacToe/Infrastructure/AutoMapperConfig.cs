using TicTacToe.BL.GameManager.Interfaces;
namespace TicTacToe.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(t =>
            t.AddProfiles(
                typeof(AutoMapperConfig).Assembly,
                typeof(IGameManager).Assembly));
        }
    }
}
