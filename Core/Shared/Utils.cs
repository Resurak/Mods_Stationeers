using Core.Config;

namespace Core.Shared
{
    public static class Utils
    {
        public static void AssignPower(this ref float value, PowerRange range, float def = float.MinValue)
        {
            if (range.Valid)
            {
                if (value < range.Min)
                {
                    value = range.Min;
                }
                else if (value > range.Max)
                {
                    value = range.Max;
                }
            }
            else
            {
                if (def != float.MinValue)
                {
                    value = def;
                }
            }
        }
    }
}
