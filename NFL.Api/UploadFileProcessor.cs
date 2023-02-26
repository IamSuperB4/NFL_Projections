using CsvHelper;
using NFL.Dto;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NFLWebsite
{

    /// <summary>
    /// Class to read and process CSV files uploaded from the Admin Page
    /// </summary>
    public class UploadFileProcessor
    {
        /// <summary>
        /// Read CSV file uploaded from the Admin Page
        /// </summary>
        /// <param name="stream">IO Stream from file upload</param>
        /// <returns>List of GamesCsvRecords pulled from file</returns>
        public List<GameCsvRecord> LoadGames(Stream stream)
        {
            int lineNumber = 0;
            TextReader reader = new StreamReader(stream);
            List<GameCsvRecord> uploadData = new();

            using CsvReader csv = new(reader, CultureInfo.InvariantCulture);

            IEnumerable<GameCsvRecord> entries = csv.GetRecords<GameCsvRecord>();
            IEnumerator<GameCsvRecord> iterator = entries.GetEnumerator();

            // read all Records found in CSV
            while (true)
            {
                GameCsvRecord? row = null;

                // move to next line
                try
                {
                    lineNumber++;

                    if (iterator.MoveNext() == false)
                    {
                        break; // reached end of file
                    }

                    row = new GameCsvRecord();
                }
                // error moving onto next row, skip the row
                //  - add blank record with line number and error message
                catch (Exception ex)
                {
                    row = new GameCsvRecord();

                    row.LineNumber = lineNumber;
                    row.ActivityLog.Add($"Error on Line {lineNumber}: {ex.Message}");

                    uploadData.Add(row);

                    continue;
                }

                row = iterator.Current;
                row.LineNumber = lineNumber;

                // Clean values
                //row = CleanGamesCsvRecord(row);

                uploadData.Add(row);
            }

            // return
            return uploadData;
        }



        /// <summary>
        /// Clean values in GamesCsvRecord
        ///     - Remove non-ascii characters
        /// </summary>
        /// <param name="row">GamesCsvRecord</param>
        /// <returns>A clean GamesCsvRecord</returns>
        private static GameCsvRecord CleanGamesCsvRecord(GameCsvRecord row)
        {
            return row;
        }


        /// <summary>
        /// Read CSV file uploaded from the Admin Page
        /// </summary>
        /// <param name="stream">IO Stream from file upload</param>
        /// <returns>List of GamesCsvRecords pulled from file</returns>
        public List<TeamCsvRecord> LoadTeams(Stream stream)
        {
            int lineNumber = 0;
            TextReader reader = new StreamReader(stream);
            List<TeamCsvRecord> uploadData = new();

            using CsvReader csv = new(reader, CultureInfo.InvariantCulture);

            IEnumerable<TeamCsvRecord> entries = csv.GetRecords<TeamCsvRecord>();
            IEnumerator<TeamCsvRecord> iterator = entries.GetEnumerator();

            // read all Records found in CSV
            while (true)
            {
                TeamCsvRecord? row = null;

                // move to next line
                try
                {
                    lineNumber++;

                    if (iterator.MoveNext() == false)
                    {
                        break; // reached end of file
                    }

                    row = new TeamCsvRecord();
                }
                // error moving onto next row, skip the row
                //  - add blank record with line number and error message
                catch (Exception ex)
                {
                    row = new TeamCsvRecord();

                    row.LineNumber = lineNumber;
                    row.ActivityLog.Add($"Error on Line {lineNumber}: {ex.Message}");

                    uploadData.Add(row);

                    continue;
                }

                row = iterator.Current;
                row.LineNumber = lineNumber;

                // Clean values
                //row = CleanGamesCsvRecord(row);

                uploadData.Add(row);
            }

            // return
            return uploadData;
        }



        /// <summary>
        /// Clean values in GamesCsvRecord
        ///     - Remove non-ascii characters
        /// </summary>
        /// <param name="row">GamesCsvRecord</param>
        /// <returns>A clean GamesCsvRecord</returns>
        private static TeamCsvRecord CleanTeamsCsvRecord(TeamCsvRecord row)
        {
            return row;
        }
    }
}
