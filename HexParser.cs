using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ATmegaSim
{
    public class HexParser
    {
        public static List<byte> FirmFile = new List<byte>();
        private static uint _baseLinearAddress = 0;

        public static void Parse(string path)
        {
            _baseLinearAddress = 0; // Сбрасываем базовый адрес для нового файла
            FirmFile.Clear(); // Очищаем FirmFile перед новым парсингом

            foreach (string temp in File.ReadAllLines(path))
            {
                if (string.IsNullOrEmpty(temp) || !temp.StartsWith(":"))
                {
                    continue;
                }
                string line = temp.TrimStart(new char[] { ':' });

                HexLine hexLine = new HexLine(line);

                switch (hexLine.type)
                {
                    case 0x00: // Data Record
                        uint currentAbsoluteAddress = _baseLinearAddress + (uint)hexLine.address;
                        while (FirmFile.Count <= currentAbsoluteAddress + hexLine.len - 1)
                        {
                            FirmFile.Add(0xFF); // Заполняем FF до нужного размера
                        }
                        for (int i = 0; i < hexLine.len; i++)
                        {
                            FirmFile[(int)(currentAbsoluteAddress + i)] = hexLine.data[i];
                        }
                        break;
                    case 0x01: // End Of File Record
                        return;
                    case 0x04: // Extended Linear Address Record
                        _baseLinearAddress = (uint)((hexLine.data[0] << 8) | hexLine.data[1]) << 16;
                        break;
                }
            }
        }
    }

    internal class HexLine
    {
        public byte len { get; private set; }
        public ushort address { get; private set; } // Изменил на ushort, т.к. address всегда положительный
        public byte type { get; private set; }
        public List<byte> data { get; private set; }

        public HexLine(string str)
        {
            if (str.Length < 10) // Минимальная длина: len (2) + address (4) + type (2) + checksum (2)
            {
                throw new FormatException("Некорректная длина HEX-строки.");
            }

            len = byte.Parse(str.Substring(0, 2), NumberStyles.HexNumber);
            address = ushort.Parse(str.Substring(2, 4), NumberStyles.HexNumber);
            type = byte.Parse(str.Substring(6, 2), NumberStyles.HexNumber);

            data = new List<byte>(len);
            int dataStartIndex = 8;
            for (int i = 0; i < len; i++)
            {
                data.Add(byte.Parse(str.Substring(dataStartIndex + (i * 2), 2), NumberStyles.HexNumber));
            }

            byte checksumFromFile = byte.Parse(str.Substring(dataStartIndex + (len * 2), 2), NumberStyles.HexNumber);
            byte calculatedChecksum = CalculateChecksum();

            if (checksumFromFile != calculatedChecksum)
            {
                throw new InvalidDataException($"Неверная контрольная сумма. Ожидалось 0x{calculatedChecksum:X2}, получено 0x{checksumFromFile:X2}.");
            }
        }

        private byte CalculateChecksum()
        {
            byte sum = len;
            sum += (byte)(address >> 8);
            sum += (byte)(address & 0xFF);
            sum += type;

            foreach (var b in data)
            {
                sum += b;
            }

            return (byte)(~sum + 1);
        }
    }
}