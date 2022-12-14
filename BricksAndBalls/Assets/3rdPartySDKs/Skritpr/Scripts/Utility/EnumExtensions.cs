using System;
namespace Skrptr.Utility
{
    /// <summary>
    /// Exension for Enums in Inspector
    /// </summary>
    public static class EnumExtensions
    {
        public static bool HasFlag(this Enum self, Enum flag)
        {
            if (self.GetType() != flag.GetType())
            {
                throw new ArgumentException("HasFlag : Flag is not of the type of Enum");
            }

            var selfValue = Convert.ToUInt64(self);
            var flagValue = Convert.ToUInt64(flag);

            return (selfValue & flagValue) == flagValue;
        }
    }
}