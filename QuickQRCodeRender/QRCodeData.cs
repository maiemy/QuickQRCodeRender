using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QRCoder
{

    using System;
    using System.IO;
    using System.IO.Compression;

    public class QRCodeData : IDisposable
    {
        public List<BitArray> ModuleMatrix { get; set; }

        public QRCodeData(int version)
        {
            this.Version = version;
            var size = ModulesPerSideFromVersion(version);
            this.ModuleMatrix = new List<BitArray>();
            for (var i = 0; i < size; i++)
                this.ModuleMatrix.Add(new BitArray(size));
        }

        public int Version { get; private set; }

        private static int ModulesPerSideFromVersion(int version)
        {
            return 21 + (version - 1) * 4;
        }

        public void Dispose()
        {
            this.ModuleMatrix = null;
            this.Version = 0;

        }

        public enum Compression
        {
            Uncompressed,
            Deflate,
            GZip
        }
    }
}
