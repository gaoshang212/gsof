using System.Windows.Input;

namespace Gsof.Xaml.Extensions
{
    public static class KeyExtensions
    {
        public static bool IsNum(this Key p_key)
        {
            return (p_key > Key.D0 && p_key < Key.D9) || (p_key > Key.NumPad0 && p_key < Key.NumPad9);
        }
    }
}
