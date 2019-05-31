using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using FileHelpers;

namespace cvs_to_sql
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseContext _context = new BaseContext();
            List<CdrModel> CdrRecords = new List<CdrModel>();
            string PathFile = @"C:\Users\velik\Desktop\Master.csv";

            int CountRecords = 0;
            int GoodRecords = 0;
            int BadRecords = 0;

            using (var reader = new StreamReader(PathFile))
            {
                while (!reader.EndOfStream)
                {
                    try
                    {
                        string ReadLine = reader.ReadLine();
                        CdrModel SplitLine = ParseString(ReadLine);
                        CdrRecords.Add(SplitLine);
                        CountRecords++;
                        GoodRecords++;

                        if (SplitLine != null)
                        {
                            _context.CdrModels.Add(SplitLine);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Ошибка записи - {0}", ReadLine);
                            BadRecords++;
                            CountRecords++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        BadRecords++;
                        CountRecords++;
                        Console.WriteLine("String: {0} - {1} / {2}", CountRecords, ex.Source, ex.Message);
                    }
                }
            }

            #region парсер записей CDR Asterisk
            CdrModel ParseString(string s)
            {
                CdrModel CdrRecord = null;
                string[] SplitLine = s.Split(",");
                DateTime date;

                if (SplitLine.Count() == 20)
                {


                    if (SplitLine[12] == "")
                    {
                        date = ParserDateTime(SplitLine[11]);
                    }
                    else
                    {
                        date = ParserDateTime(SplitLine[12]);
                    }

                    CdrRecord = new CdrModel
                    {
                        calldate = ParserDateTime(SplitLine[11]),
                        src = ParserText(SplitLine[1]),
                        dst = ParserText(SplitLine[2]),
                        dcontext = ParserText(SplitLine[3]),
                        clid = ParserText(SplitLine[4]),
                        channel = ParserText(SplitLine[5]),
                        dstchannel = ParserText(SplitLine[6]),
                        lastapp = ParserText(SplitLine[7]),
                        lastdata = string.Format("{0},{1},{2}", ParserText(SplitLine[8]), ParserText(SplitLine[9]), ParserText(SplitLine[10])),
                        start = ParserDateTime(SplitLine[11]),
                        answer = date,
                        end = ParserDateTime(SplitLine[13]),
                        duration = ParseInt(SplitLine[14]),
                        billsec = ParseInt(SplitLine[15]),
                        disposition = ParserText(SplitLine[16]),
                        amaflags = ParseAmaflag(SplitLine[17]),
                        accountcode = "",
                        userfield = "",
                        uniqueid = ParserText(SplitLine[18])
                    };
                    return CdrRecord;
                }
                else if (SplitLine.Count() == 18)
                {
                    if (SplitLine[10] == "")
                    {
                        date = ParserDateTime(SplitLine[9]);
                    }
                    else
                    {
                        date = ParserDateTime(SplitLine[10]);
                    }

                    CdrRecord = new CdrModel
                    {
                        calldate = ParserDateTime(SplitLine[9]),
                        src = ParserText(SplitLine[1]),
                        dst = ParserText(SplitLine[2]),
                        dcontext = ParserText(SplitLine[3]),
                        clid = ParserText(SplitLine[4]),
                        channel = ParserText(SplitLine[5]),
                        dstchannel = ParserText(SplitLine[6]),
                        lastapp = ParserText(SplitLine[7]),
                        lastdata = ParserText(SplitLine[8]),
                        start = ParserDateTime(SplitLine[9]),
                        answer = date,
                        end = ParserDateTime(SplitLine[11]),
                        duration = ParseInt(SplitLine[12]),
                        billsec = ParseInt(SplitLine[13]),
                        disposition = ParserText(SplitLine[14]),
                        amaflags = ParseAmaflag(SplitLine[15]),
                        accountcode = "",
                        userfield = "",
                        uniqueid = ParserText(SplitLine[16])
                    };
                    return CdrRecord;
                }
                else
                {
                    //пропускаем сломанные записи
                    return null;
                }
            }



            string ParserText(string s)
            {
                return s.Trim('"');
            }

            DateTime ParserDateTime(string s)
            {
                string StringTrim = s.Trim('"');
                return DateTime.Parse(StringTrim);
            }

            int ParseInt(string s)
            {
                string StringTrim = s.Trim('"');
                return Int32.Parse(StringTrim);
            }

            int ParseAmaflag(string s)
            {
                string StringTrim = s.Trim('"');
                switch (StringTrim)
                {
                    case "DOCUMENTATION":
                        return 3;
                    default:
                        return 0;
                }
            }

            #endregion

            #region вывод информации о результатах работы
            //_context.CdrModels.AddRange(CdrRecords);
            _context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Количество записей в файле - {0}", CdrRecords.Capacity);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Колличество обработанных - {0}", GoodRecords);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Колличество пропущенных записей - {0}", CdrRecords.Capacity - GoodRecords);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Колличество ошибок обработки - {0}", BadRecords);
            #endregion
        }
    }
}
