using System;
using System.Linq;
using Securify.PropertyStore.Structures;
using System.Collections.Generic;
using System.Text;

namespace Securify.PropertyStore
{
    /// <summary>
    /// The Property Store Binary File Format is a sequence of Serialized Property 
    /// Storage structures. The sequence MUST be terminated by a Serialized Property 
    /// Storage structure that specifies 0x00000000 for the Storage Size field.
    /// </summary>
    public class SerializedPropertyStore : Structure
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SerializedPropertyStore() : base()
        {
            PropertyStorage = new List<SerializedPropertyStorage>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="SerializedPropertyStorage">A sequence of one or more Serialized Property Storage structures</param>
        public SerializedPropertyStore(List<SerializedPropertyStorage> SerializedPropertyStorage) : base()
        {
            this.PropertyStorage = SerializedPropertyStorage;
        }
        #endregion // Constructor

        #region FromByteArray
        /// <summary>
        /// Create a LinkTargetIDList from a given byte array
        /// </summary>
        /// <param name="ba">The byte array</param>
        /// <returns>A LinkTargetIDList object</returns>
        public static SerializedPropertyStore FromByteArray(byte[] ba)
        {
            SerializedPropertyStore PropertyStore = new SerializedPropertyStore();

            if (ba.Length < 8)
            {
                throw new ArgumentException(String.Format("Size of the SerializedPropertyStore is less than 8 ({0})", ba.Length));
            }

            UInt32 StoreSize = BitConverter.ToUInt32(ba, 0);
            if (ba.Length < StoreSize)
            {
                throw new ArgumentException(String.Format("Size of the SerializedPropertyStore is less than {0} ({1})", StoreSize, ba.Length));
            }

            ba = ba.Skip(4).ToArray();
            UInt32 StorageSize = BitConverter.ToUInt32(ba, 0);
            while (StorageSize > 5)
            {
                SerializedPropertyStorage PropertyStorage = SerializedPropertyStorage.FromByteArray(ba);
                PropertyStore.PropertyStorage.Add(PropertyStorage);
                ba = ba.Skip((int)StorageSize).ToArray();
                StorageSize = BitConverter.ToUInt32(ba, 0);
            }

            return PropertyStore;
        }
        #endregion // FromByteArray

        #region StorageSize
        /// <summary>
        /// Store Size (4 bytes): An unsigned integer that specifies the total size, 
        /// in bytes, of this structure, excluding the size of this field.
        /// </summary>
        public virtual UInt32 StoreSize
        {
            get
            {
                UInt32 Size = 8;
                for(int i = 0; i < PropertyStorage.Count; i++)
                {
                    Size += PropertyStorage[i].StorageSize;
                }
                return Size;
            }
        }
        #endregion // StoreSize

        /// <summary>
        /// A sequence of one or more Serialized Property Storage structures
        /// </summary>
        public List<SerializedPropertyStorage> PropertyStorage { get; set; }

        #region GetBytes
        /// <inheritdoc />
        public override byte[] GetBytes()
        {
            int Offset = 4;
            byte[] PropertyStore = new byte[StoreSize];
            Buffer.BlockCopy(BitConverter.GetBytes(StoreSize), 0, PropertyStore, 0, 4);
            for (int i = 0; i < this.PropertyStorage.Count; i++)
            {
                SerializedPropertyStorage PropertyStorage = this.PropertyStorage[i];
                Buffer.BlockCopy(PropertyStorage.GetBytes(), 0, PropertyStore, Offset, (int)PropertyStorage.StorageSize);
                Offset += (int)PropertyStorage.StorageSize;
            }
            return PropertyStore;
        }
        #endregion // GetBytes

        #region ToString
        /// <inheritdoc />
        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.ToString());
            builder.AppendFormat("StoreSize: {0} (0x{0X})", StoreSize);
            builder.AppendLine();
            for (int i = 0; i < PropertyStorage.Count; i++)
            {
                builder.Append(PropertyStorage[i].ToString());
            }
            return builder.ToString();
        }
        #endregion // ToString
    }
}
