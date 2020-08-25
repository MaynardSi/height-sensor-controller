
namespace HeightSensor.Utils
{
    public static class ByteUtils
    {
        //https://stackoverflow.com/questions/4859023/find-an-array-byte-inside-another-array/26880541
        /// <summary>
        /// Locates a byte array from the larger byte array.
        /// </summary>
        /// <param name="haystack">The larger byte array.</param>
        /// <param name="needle">The byte array to locate.</param>
        /// <returns></returns>
        public static int SearchBytesIndex(byte[] haystack, byte[] needle)
        {
            var len = needle.Length;
            var limit = haystack.Length - len;
            for (var i = 0; i <= limit; i++)
            {
                var k = 0;
                for (; k < len; k++)
                {
                    if (needle[k] != haystack[i + k]) break;
                }
                if (k == len) return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns a value indicating whether a specified sub byte array occurs within a byte array.
        /// </summary>
        /// <param name="byteArrayToSearch"></param>
        /// <param name="subByteArray"></param>
        /// <returns></returns>
        public static bool ByteArrayContains(byte[] byteArrayToSearch, byte[] subByteArray)
        {
            return SearchBytesIndex(byteArrayToSearch, subByteArray) != -1;
        }

        /// <summary>
        /// Concatenates two byte arrays.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static byte[] ConcatByteArray(byte[] a, byte[] b)
        {
            byte[] output = new byte[a.Length + b.Length];
            for (int i = 0; i < a.Length; i++)
                output[i] = a[i];
            for (int j = 0; j < b.Length; j++)
                output[a.Length + j] = b[j];
            return output;
        }
    }
}
