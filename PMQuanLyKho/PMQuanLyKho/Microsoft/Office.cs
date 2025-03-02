namespace Microsoft
{
    internal class Office
    {
        internal class Interop
        {
            internal class Excel
            {
                internal class Application
                {
                    public Application()
                    {
                    }

                    public object Workbooks { get; internal set; }
                }

                internal class Worksheet
                {
                    public object Cells { get; internal set; }
                }
            }
        }
    }
}