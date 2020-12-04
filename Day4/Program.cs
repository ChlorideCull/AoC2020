using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    class Passport
    {
        public short? BirthYear { get; set; }
        public short? IssueYear { get; set; }
        public short? ExpiryYear { get; set; }
        public string Height { get; set; }
        public string HairColorHex { get; set; }
        public string EyeColorHex { get; set; }
        public string PassportId { get; set; }
        public string CountryId { get; set; }

        public Passport() { }
        
        public Passport(string inputData)
        {
            var sets = inputData.Replace('\n', ' ').Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(':'));
            foreach (var set in sets)
            {
                switch (set[0])
                {
                    case "byr":
                        BirthYear = Convert.ToInt16(set[1]);
                        break;
                    case "iyr":
                        IssueYear = Convert.ToInt16(set[1]);
                        break;
                    case "eyr":
                        ExpiryYear = Convert.ToInt16(set[1]);
                        break;
                    case "hgt":
                        Height = set[1];
                        break;
                    case "hcl":
                        HairColorHex = set[1];
                        break;
                    case "ecl":
                        EyeColorHex = set[1];
                        break;
                    case "pid":
                        PassportId = set[1];
                        break;
                    case "cid":
                        CountryId = set[1];
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public bool Validate(bool validateContent)
        {
            // Country ID deliberately skipped
            if (BirthYear == null || IssueYear == null || ExpiryYear == null || Height == null ||
                HairColorHex == null || EyeColorHex == null || PassportId == null)
            {
                return false;
            }

            if (!validateContent)
                return true;
            
            if (BirthYear < 1920 || BirthYear > 2002)
                return false;

            if (IssueYear < 2010 || IssueYear > 2020)
                return false;

            if (ExpiryYear < 2020 || ExpiryYear > 2030)
                return false;

            var heightNum = Convert.ToInt16(Height.Substring(0, Height.Length-2));
            if (Height.EndsWith("in"))
            {
                if (heightNum < 59 || heightNum > 76)
                    return false;
            }
            else if (Height.EndsWith("cm"))
            {
                if (heightNum < 150 || heightNum > 193)
                    return false;
            }
            else
            {
                return false;
            }

            if (!Regex.IsMatch(HairColorHex, @"\A#[\da-f]{6}\z"))
                return false;

            if (!(new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}).Contains(EyeColorHex))
                return false;
            
            if (!Regex.IsMatch(PassportId, @"\A\d{9}\z"))
                return false;

            return true;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Basic data validation, make sure all fields exist, except also backdoor it to not check for country ID.
             * Bonus: actually validate contents.
             */
            var inputLines = File.ReadAllText(args[0]).Split("\n\n");
            var passports = inputLines.Select(x => new Passport(x)).ToList();
            Console.WriteLine(passports.Count(x => x.Validate(false)));
            Console.WriteLine(passports.Count(x => x.Validate(true)));
        }
    }
}