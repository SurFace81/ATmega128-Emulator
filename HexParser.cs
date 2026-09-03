using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ATmegaSim.CPU;

namespace ATmegaSim
{
    public class HexParser
    {
        public static List<byte> FirmFile = new List<byte>();
        private static uint _baseLinearAddress = 0;

        public static void Parse(string path)
        {
            _baseLinearAddress = 0;
            var parsedFirmFile = new List<byte>();
            bool eofFound = false;

            foreach (string temp in File.ReadAllLines(path))
            {
                string record = temp.Trim();
                if (record.Length == 0)
                {
                    continue;
                }
                if (eofFound)
                    throw new InvalidDataException("После записи EOF обнаружены дополнительные записи.");
                if (!record.StartsWith(":"))
                    throw new InvalidDataException("Некорректная HEX-запись.");

                HexLine hexLine = new HexLine(record.Substring(1));

                switch (hexLine.type)
                {
                case 0x00: // Data Record
                        ulong currentAbsoluteAddress = (ulong)_baseLinearAddress + hexLine.address;
                        ulong endAddress = currentAbsoluteAddress + hexLine.len;
                        if (currentAbsoluteAddress > Cpu.FLASH_SIZE || endAddress > Cpu.FLASH_SIZE)
                            throw new InvalidDataException("Адрес записи HEX выходит за пределы FLASH памяти.");

                        while (parsedFirmFile.Count < (int)endAddress)
                        {
                            parsedFirmFile.Add(0xFF);
                        }
                        for (int i = 0; i < hexLine.len; i++)
                        {
                            parsedFirmFile[(int)currentAbsoluteAddress + i] = hexLine.data[i];
                        }
                        break;
                    case 0x01: // End Of File Record
                        if (hexLine.len != 0 || hexLine.address != 0)
                            throw new InvalidDataException("Некорректная запись EOF.");
                        eofFound = true;
                        break;
                    case 0x02: // Extended Segment Address Record
                        if (hexLine.len != 2)
                            throw new InvalidDataException("Некорректная длина записи Extended Segment Address.");
                        _baseLinearAddress = (uint)((hexLine.data[0] << 8) | hexLine.data[1]) << 4;
                        break;
                    case 0x04: // Extended Linear Address Record
                        if (hexLine.len != 2)
                            throw new InvalidDataException("Некорректная длина записи Extended Linear Address.");
                        _baseLinearAddress = (uint)((hexLine.data[0] << 8) | hexLine.data[1]) << 16;
                        break;
                }
            }

            if (!eofFound)
                throw new InvalidDataException("В HEX-файле отсутствует запись EOF.");

            FirmFile = parsedFirmFile;
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
            if (str.Length < 10)
            {
                throw new InvalidDataException("Некорректная длина HEX-строки.");
            }

            try
            {
                len = byte.Parse(str.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                if (str.Length != 10 + len * 2)
                    throw new InvalidDataException("Некорректная длина HEX-строки.");

                address = ushort.Parse(str.Substring(2, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                type = byte.Parse(str.Substring(6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);

                data = new List<byte>(len);
                int dataStartIndex = 8;
                for (int i = 0; i < len; i++)
                {
                    data.Add(byte.Parse(str.Substring(dataStartIndex + (i * 2), 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture));
                }

                byte checksumFromFile = byte.Parse(str.Substring(dataStartIndex + (len * 2), 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                byte calculatedChecksum = CalculateChecksum();

                if (checksumFromFile != calculatedChecksum)
                {
                    throw new InvalidDataException($"Неверная контрольная сумма. Ожидалось 0x{calculatedChecksum:X2}, получено 0x{checksumFromFile:X2}.");
                }
            }
            catch (FormatException ex)
            {
                throw new InvalidDataException("Некорректная HEX-запись.", ex);
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
