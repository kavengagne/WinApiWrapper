using System.Runtime.InteropServices;


namespace WinApiWrapper.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        internal Enums.InputType type;
        internal InputUnion U;

        internal static int Size
        {
            get { return Marshal.SizeOf(typeof(INPUT)); }
        }
    }
}