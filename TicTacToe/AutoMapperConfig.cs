using TicTacToe.BL.Interfaces;

namespace TicTacToe
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
