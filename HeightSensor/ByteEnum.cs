namespace HeightSensor
{

    public static class ByteCommandEnum
    {
        public static readonly byte[] SINGLE_MEASURE = new byte[] { 0x30, 0x02, 0x0D, 0x60 };
    }
    public static class ByteResponseEnum
    {
        public static readonly byte[] SUCCESS = new byte[] { 0xC0, 0x00 };
        public static readonly byte[] COMMAND_ERROR = new byte[] { 0xE0, 0x02, 0x00, 0xE1 };
        public static readonly byte[] ADDRESS_ERROR = new byte[] { 0xE0, 0x02, 0x00, 0xE2 };
        public static readonly byte[] OVERFLOW_ERROR = new byte[] { 0xE0, 0x02, 0x00, 0xE9 };
    }
}
