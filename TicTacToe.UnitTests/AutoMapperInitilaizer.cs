namespace TicTacToe.UnitTests
{
    public static class AutoMapperInitilaizer
    {
        private static bool _initialized = false;

        public static void Initialize()
        {
            if (!_initialized)
            {
                AutoMapperConfig.RegisterMappings();
                _initialized = true;
            }
        }
    }
}
