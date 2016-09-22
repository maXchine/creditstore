namespace Bobby.Libs.Config
{
    using System.Collections.Generic;
    using System.IO;

    public static class SteamExtender
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <exception cref="System.OutOfMemoryException"></exception>
        /// <returns></returns>
        public static IEnumerable<byte> ReadAllBytes(this Stream stream)
        {
            if (stream.CanRead)
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int) stream.Length);
                return bytes;
            }

            var byteArry = new List<byte>();
            int b;
            do
            {
                b = stream.ReadByte();
                byteArry.Add((byte) b);
            } while (b != -1);

            return byteArry;
        }
    }
}
